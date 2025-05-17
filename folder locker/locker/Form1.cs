using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace locker
{
    public partial class Form1 : Form
    {

        string lockerPath = ".\\Locker";
        string privatePath = ".\\private";
        string password = "123";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(privatePath)) {
                this.Hide();
                DialogResult ris=MessageBox.Show("Are you sure you want to lock the folder?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (ris == DialogResult.Yes) {
                    lockFolder();
                }
                Application.Exit();
            }
            else if(Directory.Exists(lockerPath))
            {
                this.Show();
            }
            else
            {
                Directory.CreateDirectory(privatePath);
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == password)
            {
                this.Hide();
                UnlockFolder();
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Invalid Password", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lockFolder()
        {
            Directory.Move(privatePath, lockerPath);
            DirectoryInfo dirInfo = new DirectoryInfo(lockerPath);
            dirInfo.Attributes |= FileAttributes.Hidden | FileAttributes.System;
        }

        private void UnlockFolder()
        {
            Directory.Move(lockerPath, privatePath);
            DirectoryInfo dirInfo = new DirectoryInfo(privatePath);
            dirInfo.Attributes &= ~(FileAttributes.Hidden | FileAttributes.System);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                button1.PerformClick();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinkedListApp;
using System.Runtime.InteropServices;

namespace ALG_LAB3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FreeConsole();

        Tree234<string> tree234 = new Tree234<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            tree234.Add(textBox.Text);
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            if (AllocConsole())
            {
                tree234.PrintTree();
                Console.ReadLine();
                FreeConsole();
            }
        }
    }
}
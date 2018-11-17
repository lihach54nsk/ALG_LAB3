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
using System.Diagnostics;

namespace ALG_LAB3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //chart1.Series[0]
        }

        /* [DllImport("kernel32.dll", SetLastError = true)]
         [return: MarshalAs(UnmanagedType.Bool)]
         private static extern bool AllocConsole();

         [DllImport("kernel32.dll", SetLastError = true)]
         [return: MarshalAs(UnmanagedType.Bool)]
         private static extern bool FreeConsole();*/

        Tree234<string> tree234 = new Tree234<string>();

        double count = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            tree234.Add(textBox.Text);
            stopwatch.Stop();
            var time = stopwatch.ElapsedTicks;
            count++;
            chart1.Series[0].Points.Add(Convert.ToDouble(time));
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            var value = tree234.ToString();
        }
    }
}
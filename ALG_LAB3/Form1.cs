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

            
        }

        /* [DllImport("kernel32.dll", SetLastError = true)]
         [return: MarshalAs(UnmanagedType.Bool)]
         private static extern bool AllocConsole();

         [DllImport("kernel32.dll", SetLastError = true)]
         [return: MarshalAs(UnmanagedType.Bool)]
         private static extern bool FreeConsole();*/

        Tree234<int> tree234 = new Tree234<int>();

        double count = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            tree234.Add(Convert.ToInt32(textBox.Text));
            stopwatch.Stop();
            var time = stopwatch.ElapsedTicks;
            count++;
            chart1.Series[0].Points.Add(Convert.ToDouble(time));
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            dataGridView.ColumnCount = 1;
            dataGridView.RowCount = 10001;
            var value = tree234.ToString();
            for (int i = 0; i < 10000; i++)
            {
                dataGridView.Rows[i + 1].Cells[0].Value = value.Split(' ')[i];
            }
        }

        private void trashButton_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < 10000; i++)
            {
                stopwatch.Start();
                tree234.Add(i);
                stopwatch.Stop();
                var time = stopwatch.ElapsedTicks;
                stopwatch.Reset();
                chart1.Series[0].Points.Add(Convert.ToDouble(time));
            }
        }
    }
}
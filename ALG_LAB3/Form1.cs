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
            var list = tree234.GetLinkedList();

            if (list.Count == 0) return;

            dataGridView.ColumnCount = 1;
            dataGridView.RowCount = list.Count;

            var counter = 0;

            foreach (var item in list)
            { 
                dataGridView.Rows[counter++].Cells[0].Value = item.ToString();
            }
        }

        private void trashButton_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < 1000000; i++)
            {
                stopwatch.Start();
                tree234.Add(i);
                stopwatch.Stop();
                var time = stopwatch.ElapsedTicks;
                stopwatch.Reset();
                //if (time > 5000) continue;
                chart1.Series[0].Points.Add(Convert.ToDouble(time));
            }
        }
    }
}
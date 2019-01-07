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
        private readonly Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            tree234 = new Tree234<int>();
            tree234.Add(191);
            tree234.Add(12);
            tree234.Add(45);
            tree234.Add(14);
            tree234.Add(193);
            tree234.Add(75);
            tree234.Add(56);
            tree234.Add(5);
            tree234.Remove(12);
            tree234.Remove(191);
            tree234.Remove(45);
            tree234.Remove(14);
            tree234.Remove(193);
            tree234.Remove(75);
            tree234.Remove(56);
            tree234.Remove(5);
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
            for (int i = 0; i < 50; i++)
            {
                long sum = 0;
                for (int j = 0; j < 200000; j++)
                {
                    stopwatch.Start();
                    tree234.Add(random.Next());
                    stopwatch.Stop();
                    sum += stopwatch.ElapsedTicks;
                    stopwatch.Reset();
                }
                var midTime = (double)sum / 10000.0;

                chart1.Series[0].Points.Add(midTime);
            }
        }
    }
}
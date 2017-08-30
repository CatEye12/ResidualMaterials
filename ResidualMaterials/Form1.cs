using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResidualMaterials
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            dt = new MyDtTable();
            comboBox1.SelectedIndex = 0;
            dt.Load_Data(residualType);
            dataGridView1.DataSource = dt.MakingDataList(residualType);
        }

        MyDtTable dt;

        /// <summary>
        /// false  "Тело вращения"
        /// true   "Плоское"
        /// </summary>
        bool residualType;



        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dt.PushingDataInTable(residualType, textBox4, textBox5, textBox3);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                residualType = false;
                label6.Visible = false;
                label4.Text = "Диаметр";
                textBox1.Visible = false;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                residualType = true;
                label6.Visible = true;
                label4.Text = "Ширина листа/полосы";
                textBox1.Visible = true;
            }
            dt.data = dt.MakingDataList(residualType);
            dataGridView1.DataSource = dt.data;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dt.CutOut(dataGridView1, residualType, textBox1, textBox2);
            dataGridView1.DataSource = dt.data;
        }
    }
}
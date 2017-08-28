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
            dataGridView1.DataSource = dt.Load_Data();
        }
        MyDtTable dt;

        /// <summary>
        /// false  "Тело вращения"
        /// true   "Плоское"
        /// </summary>
        bool residualType = false;



        private void button1_Click(object sender, EventArgs e)
        {
            if (residualType == false)
            {
                var list = dt.ConvertInputDataToList(textBox8.Text, textBox9.Text);
                dataGridView1.DataSource = dt.PushingDataInTable(list);
                MessageBox.Show("Telo vrascheniya");
            }
            else
            {
                var list = dt.ConvertInputDataToList(textBox3.Text, textBox4.Text, textBox5.Text);
                dataGridView1.DataSource = dt.PushingDataInTable(list);
                MessageBox.Show("Ploskoe");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                residualType = false;
                panel1.Visible = true;
                label6.Visible = false;
                textBox1.Visible = false;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                residualType = true;
                panel1.Visible = false;
                label6.Visible = true;
                textBox1.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dt.CutOut(dataGridView1, residualType, textBox1, textBox2);
        }
    }
}

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
        CuttingTheWorkpiece cut = new CuttingTheWorkpiece();
        bool flag = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (flag == false)
            {
                var list = dt.ConvertTo(textBox3.Text);
                dataGridView1.DataSource = dt.PushingDataInTable(list);
                MessageBox.Show("Telo vrascheniya");
            }
            else
            {
                var list = dt.ConvertTo(textBox3.Text, textBox4.Text, textBox5.Text);
                dataGridView1.DataSource = dt.PushingDataInTable(list);
                MessageBox.Show("Ploskoe");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                flag = false;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                flag = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && dataGridView1.SelectedRows != null)
            {
                dt.GetItemToCut(dataGridView1);
            }
            else { MessageBox.Show("Заполните параметры заготовки и выберите остаток!"); }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }
    }
}

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

        /// <summary>
        /// false  "Тело вращения"
        /// true   "Плоское"
        /// </summary>
        bool residualType = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (residualType == false)
            {
                var list = dt.ConvertTo(textBox4.Text, textBox9.Text);
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
                residualType = false;
                panel1.Visible = true;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                residualType = true;
                panel1.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool res = false;

                if (residualType == true)
                {
                    if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && dataGridView1.SelectedRows != null)

                    { res = dt.CheckingWorkpieceLessThanResidual(dataGridView1, textBox1, textBox2);}
 
                    else { MessageBox.Show("Заполните параметры заготовки и выберите остаток!"); }

                }
                else
                {
                    if (!string.IsNullOrEmpty(textBox6.Text) && dataGridView1.SelectedRows != null)

                    { res = dt.CheckingWorkpieceLessThanResidual(dataGridView1, textBox6 ); }

                    else { MessageBox.Show("Заполните параметры заготовки и выберите остаток!"); }

                }
                
                if (res == true)
                {
                    //вырезаем заготовку
                    MessageBox.Show("Заготовка будет вырезана)");
                }
                else { MessageBox.Show("Невозможно вырезать заготовку. Параметры заготовки больше параметров остатка!"); }
 
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }
    }
}

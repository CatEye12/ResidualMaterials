using System;
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
            dt.Load_Data(dt.residualType);
            dataGridView.DataSource = dt.FillDgv(dt.residualType);
        }

        MyDtTable dt;

        /// <summary>
        /// false  "Тело вращения"
        /// true   "Плоское"
        /// </summary>
        
        private void Create_Click(object sender, EventArgs e)
        {
            dt.PushingDataInTable(dt.residualType, textBox4, textBox5, textBox3);

            dataGridView.DataSource = dt.FillDgv(dt.residualType);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt.ManagingUserInterface(comboBox1, label6, label3, label4, textBox1, textBox3);

            dataGridView.DataSource = dt.FillDgv(dt.residualType);
        }
        
        private void CutOut_Click(object sender, EventArgs e)
        {
            dt.CutOut(dataGridView, dt.residualType, textBox1, textBox2);
        }
    }
}
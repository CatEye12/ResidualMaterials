using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ResidualMaterials
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            dt = new MyDtTable();
            usInter = new UserInterface();
            comboBox1.SelectedIndex = 0;
            dt.Load_Data(MyDtTable.residualType);

            b = new Balance();

            //dataGridView.DataSource = dt.data;///dt.FillDgv();
        }

        MyDtTable dt;
        Balance b; 
        UserInterface usInter;
        
        private void Create_Click(object sender, EventArgs e)
        {
            SuperPuper();
            return;
            usInter.CheckIfFieldsAreFilled(txtWidthDim, txtLength, txtH);
            usInter.ConvTextToDouble(txtWidthDim, txtLength, txtH);
            dt.PushingDataInTable();

            dataGridView.DataSource = dt.FillDgv();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            usInter.CheckType(comboBox1);
            usInter.ManagingUserInterface(lblWidthWP, lblH, lblWidthDim, txtWidthWP, txtH);

            //dataGridView.DataSource = dt.FillDgv();
        }
        
        private void CutOut_Click(object sender, EventArgs e)
        {
            usInter.CheckIfFieldsAreFilled(dataGridView, txtWidthWP, txtLengthWP);
            usInter.ConvTxtToDouble(txtWidthWP, txtLengthWP);

            dt.CutOut(dataGridView, MyDtTable.residualType);
        }

        private static void AllowUserInputOnlyNumbers(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        
        #region only numbers input
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            AllowUserInputOnlyNumbers(sender, e);
        }
        private void txtWidthDim_KeyPress(object sender, KeyPressEventArgs e)
        {
            AllowUserInputOnlyNumbers(sender, e);
        }

        private void txtLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            AllowUserInputOnlyNumbers(sender, e);
        }

        private void txtH_KeyPress(object sender, KeyPressEventArgs e)
        {
            AllowUserInputOnlyNumbers(sender, e);
        }

        private void txtWidthWP_KeyPress(object sender, KeyPressEventArgs e)
        {
            AllowUserInputOnlyNumbers(sender, e);
        }
        #endregion

        private void SuperPuper()
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.AutoSize = true;
          
            DataGridViewTextBoxColumn columnBal = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn columnType = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn columnDim = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn columnLenth = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn columnW = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn columnH = new DataGridViewTextBoxColumn();

            columnBal.Name = "Баланс";
            columnType.Name = "Тип";
            columnDim.Name = "Диаметр";
            columnLenth.Name = "Длина";
            columnW.Name = "Ширина";
            columnH.Name = "Высота";

            dataGridView.Columns.Add(columnBal);
            dataGridView.Columns.Add(columnType);
            dataGridView.Columns.Add(columnDim);
            dataGridView.Columns.Add(columnLenth);
            dataGridView.Columns.Add(columnW);
            dataGridView.Columns.Add(columnH);

            var bindingList = new BindingList<Balance>(dt.dataList);
            var source = new BindingSource(bindingList, null);

            dataGridView.Columns[0].DataPropertyName = "BalanceId";
            dataGridView.Columns[1].DataPropertyName = "Type";
            dataGridView.Columns[2].DataPropertyName = "Dim";
            dataGridView.Columns[3].DataPropertyName = "Length";
            dataGridView.Columns[4].DataPropertyName = "W";
            dataGridView.Columns[5].DataPropertyName = "H";
            dataGridView.DataSource = source;


            //Balance currentObject = (Balance)dataGridView.CurrentRow.DataBoundItem;

            //MessageBox.Show(currentObject.BalanceID.ToString());

            //return;
        }
    }
}
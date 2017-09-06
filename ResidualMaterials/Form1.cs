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

            SuperPuper();
        }    

        MyDtTable dt;
        UserInterface usInter;
        

        private void Create_Click(object sender, EventArgs e)
        {        
            usInter.CheckIfFieldsAreFilled(txtName, txtWidthDim, txtLength, txtH);
            usInter.ConvTextToDecimal(txtName, txtWidthDim, txtLength, txtH);
            dt.PushingDataInTable();

            SuperPuper();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            usInter.CheckType(comboBox1);
            usInter.ManagingUserInterface(lblWidthWP, lblH, lblWidthDim, txtWidthWP, txtH);
            
            SuperPuper();
        }
        
        private void CutOut_Click(object sender, EventArgs e)
        {
            usInter.CheckIfFieldsAreFilled(dataGridView, txtWidthWP, txtLengthWP);
            usInter.ConvTxtToDecimal(txtWidthWP, txtLengthWP);
            dt.CutOut();

            SuperPuper();
        }

        private static void AllowUserInputOnlyNumbers(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                        (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }
        
        #region only numbers input //////////
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
            dataGridView.Columns.Clear();      
            dt.dataListToView = dt.MakingDataList();   

            dataGridView.AutoGenerateColumns = false;
            dataGridView.AutoSize = true;
            
            string [] columnName = null;
            
            if (MyDtTable.residualType == false)
            {
                columnName = new string[] {"№", "Диаметр", "Длина", "Версия" };
            }
            else { columnName = new string[] { "№", "Длина", "Ширина", "Высота", "Версия" }; }
                        

            DataGridViewColumn[] column_array = new DataGridViewColumn[columnName.Length];
            for (int cnt = 0; cnt < columnName.Length; cnt++)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.Name = columnName[cnt];
                column_array[cnt] = col;
            }
            dataGridView.Columns.AddRange(column_array);

            var bindingList = new BindingList<Balance>(dt.dataListToView);
            var source = new BindingSource(bindingList, null);
           

            dataGridView.Columns["№"].DataPropertyName = "Name";
            dataGridView.Columns["Версия"].DataPropertyName = "Version";
            if (MyDtTable.residualType == false)
            {
                dataGridView.Columns["Диаметр"].DataPropertyName = "Dim";
                dataGridView.Columns["Длина"].DataPropertyName = "Length";
            }
            else
            {
                dataGridView.Columns["Длина"].DataPropertyName = "Length";
                dataGridView.Columns["Ширина"].DataPropertyName = "W";
                dataGridView.Columns["Высота"].DataPropertyName = "H";
            }
            dataGridView.DataSource = source;
        }

        public void SuperPuper2()
        {
            dataGridView2.Columns.Clear();
            dt.dataListToView = dt.GetItemsofTheSameVersion();
            
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.AutoSize = true;
            string[] columnName = null;

            if (MyDtTable.residualType == false)
            {
                columnName = new string[] {  "№", "Диаметр", "Длина", "Версия"};
            }
            else { columnName = new string[] { "№","Длина", "Ширина", "Высота", "Версия"}; }

            DataGridViewColumn[] column_array = new DataGridViewColumn[columnName.Length];
            for (int cnt = 0; cnt < columnName.Length; cnt++)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.Name = columnName[cnt];
                column_array[cnt] = col;
            }
            dataGridView2.Columns.AddRange(column_array);

            var bindingList = new BindingList<Balance>(dt.dataListToView);
            var source = new BindingSource(bindingList, null);
            dataGridView2.Columns["№"].DataPropertyName = "Name";
            dataGridView2.Columns["Версия"].DataPropertyName = "Version";
            if (MyDtTable.residualType == false)
            {
                dataGridView2.Columns["Диаметр"].DataPropertyName = "Dim";
                dataGridView2.Columns["Длина"].DataPropertyName = "Length";                
            }
            else
            {
                dataGridView2.Columns["Длина"].DataPropertyName = "Length";
                dataGridView2.Columns["Ширина"].DataPropertyName = "W";
                dataGridView2.Columns["Высота"].DataPropertyName = "H";
            }

            dataGridView2.DataSource = source;
            usInter.AddParametersOfDeletedWP(dt,dataGridView2, "Длина вырезаной заготовки2");
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MyDtTable.itemToCutFrom = usInter.GetSelectedBalance(dataGridView);
            
            SuperPuper2();
        }
    } 
}
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

            usInter.SuperPuper(dataGridView, dt);
        }    

        MyDtTable dt;
        UserInterface usInter;
        

        private void Create_Click(object sender, EventArgs e)
        {        
            usInter.CheckIfFieldsAreFilled(txtName, txtWidthDim, txtLength, txtH);
            usInter.ConvTextToDecimal(txtName, txtWidthDim, txtLength, txtH);
            dt.PushingDataInTable();

            usInter.SuperPuper(dataGridView, dt);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            usInter.ClearTxtBoxes(txtName, txtWidthDim, txtLength, txtH, txtLengthWP, txtWidthWP);
            usInter.CheckType(comboBox1);
            usInter.ManagingUserInterface(lblWidthWP, lblH, lblWidthDim, txtWidthWP, txtH);
            
            usInter.SuperPuper(dataGridView, dt);
            dataGridView2.Columns.Clear();
        }
        
        private void CutOut_Click(object sender, EventArgs e)
        {
            usInter.CheckIfFieldsAreFilled(dataGridView, txtWidthWP, txtLengthWP);
            usInter.ConvTxtToDecimal(txtWidthWP, txtLengthWP);
            dt.CutOut();

            usInter.SuperPuper(dataGridView, dt);
            usInter.SuperPuper2(dataGridView2, dt, usInter);
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

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MyDtTable.itemToCutFrom = usInter.GetSelectedBalance(dataGridView);            
            usInter.SuperPuper2(dataGridView2, dt, usInter);
            usInter.FillTxtBoxes(txtName, txtWidthDim, txtLength, txtH);
        }

        private void CancelDeletingButton_Click(object sender, EventArgs e)
        {
            dt.CancelCuttingWP();
            usInter.SuperPuper(dataGridView, dt);
            usInter.SuperPuper2(dataGridView2, dt, usInter);
        }

        private void DeleteResidualButton_Click(object sender, EventArgs e)
        {
            dt.DeleteResidualMaterial();
            usInter.SuperPuper(dataGridView, dt);
            usInter.SuperPuper2(dataGridView2, dt, usInter);
        }

        private void EditMaterialButton_Click(object sender, EventArgs e)
        {
            usInter.CheckIfFieldsAreFilled(txtName, txtWidthDim, txtLength, txtH);
            usInter.ConvTextToDecimal(txtName, txtWidthDim, txtLength, txtH);
            dt.EditResidual();

            usInter.SuperPuper(dataGridView, dt);
            usInter.SuperPuper2(dataGridView2, dt, usInter);
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            MyDtTable.itemToCutFrom = usInter.GetSelectedBalance(dataGridView);
        }

        private void checkWPFormBtn_Click(object sender, EventArgs e)
        {            
            usInter.CheckIfFieldsAreFilled(dataGridView,txtWidthWP, txtLengthWP);
            usInter.ConvTxtToDecimal(txtWidthWP, txtLengthWP);
            Form2 form2 = new Form2();
            form2.Show();          
        
        }
    } 
}
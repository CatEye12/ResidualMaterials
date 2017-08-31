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
            usInter = new UserInterface();
            comboBox1.SelectedIndex = 0;
            dt.Load_Data(MyDtTable.residualType);

            //dataGridView.DataSource = dt.data;///dt.FillDgv();


        }

        MyDtTable dt;
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

            dataGridView.DataSource = dt.FillDgv();
        }
        
        private void CutOut_Click(object sender, EventArgs e)
        {
            usInter.CheckIfFieldsAreFilled(dataGridView, txtWidthWP, txtLengthWP);
            usInter.ConvTxtToDouble(txtWidthWP, txtLengthWP);

            //var itemIdMoto = (DataLoad.DtStringsMotor)DgMotor.SelectedItem;
            //var idMotor = itemIdMoto.IdMotorTechData;

            //var itemIdImpeller = (ImpellerClass)DgImpeller.SelectedItem;
            //var idImpeller = itemIdImpeller.ImpellerId;
            
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
            DataGridViewComboBoxColumn ddd = new DataGridViewComboBoxColumn();
            DataGridViewColumnCollection dd = new DataGridViewColumnCollection(dataGridView);
            ddd. ("sss","Dasha");
            dataGridView.Columns.Add(ddd);



           // dataGridView.DataSource = dt.data;
            //Balance currentObject = (Balance)dataGridView.CurrentRow.DataBoundItem;

            //MessageBox.Show(currentObject.BalanceID.ToString());

            //return;


        }

    }
}
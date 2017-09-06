using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace ResidualMaterials
{
    class UserInterface
    {
        public void ManagingUserInterface(Label l6, Label l3, Label l4, TextBox txt1, TextBox txt3)
        {
            if (MyDtTable.residualType == false)
            {
                MyDtTable.residualType = false;
                l3.Visible = false;
                l6.Visible = false;
                l4.Text = "Диаметр";
                txt1.Visible = false;
                txt3.Visible = false;
            }
            else
            {
                MyDtTable.residualType = true;
                l3.Visible = true;
                l6.Visible = true;
                l4.Text = "Ширина листа/полосы";
                txt1.Visible = true;
                txt3.Visible = true;
            }
        }

        public void CheckIfFieldsAreFilled(DataGridView dgv, TextBox width, TextBox length)//for cutting
        {
            if (MyDtTable.residualType == true)
            {
                if (!string.IsNullOrEmpty(width.Text) && !string.IsNullOrEmpty(length.Text) && dgv.SelectedRows != null)

                { MyDtTable.isFieldsFilled = true; }
                else MyDtTable.isFieldsFilled = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(length.Text) && dgv.SelectedRows != null)

                { MyDtTable.isFieldsFilled = true; }
                else MyDtTable.isFieldsFilled = false;
            }
        }
        public void CheckIfFieldsAreFilled(TextBox name, TextBox width, TextBox length, TextBox height)//for insertion
        {
            if (MyDtTable.residualType == true)
            {
                if (!string.IsNullOrEmpty(name.Text) && !string.IsNullOrEmpty(width.Text) && !string.IsNullOrEmpty(length.Text) && !string.IsNullOrEmpty(height.Text))

                { MyDtTable.isFieldsFilled = true; }
                else MyDtTable.isFieldsFilled = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(name.Text) && !string.IsNullOrEmpty(length.Text) && !string.IsNullOrEmpty(width.Text))

                { MyDtTable.isFieldsFilled = true; }
                else MyDtTable.isFieldsFilled = false;
            }
        }

        public void ConvTextToDecimal(TextBox name, TextBox w, TextBox l, TextBox h)//for input
        {
            if (MyDtTable.isFieldsFilled == true)

            {
                if (MyDtTable.residualType == false)
                {
                    MyDtTable.widthDim = Convert.ToDecimal(w.Text);
                    MyDtTable.length = Convert.ToDecimal(l.Text);
                }
                else
                {
                    MyDtTable.widthDim = Convert.ToDecimal(w.Text);
                    MyDtTable.length = Convert.ToDecimal(l.Text);
                    MyDtTable.height = Convert.ToDecimal(h.Text);
                }
                MyDtTable.name = Convert.ToInt32(name.Text);
            }
            else MessageBox.Show("Заполнены не все параметры остатка!");
        }
        public void ConvTxtToDecimal(TextBox w, TextBox l)//for cut
        {
            if (MyDtTable.isFieldsFilled == true)

            {
                if (MyDtTable.residualType == false)
                {
                    MyDtTable.lengthWP = Convert.ToDecimal(l.Text);
                }
                else
                {
                    MyDtTable.widthWP = Convert.ToDecimal(w.Text);
                    MyDtTable.lengthWP = Convert.ToDecimal(l.Text);
                }
            }
            else MessageBox.Show("Выберите остаток и заполните параметры заготовки!");
        }


        public void CheckType(ComboBox bx)
        {
            if (bx.SelectedIndex == 0) { MyDtTable.residualType = false; } else MyDtTable.residualType = true;
        }

        public Balance GetSelectedBalance(DataGridView dgv)
        {
            Balance currentObject = (Balance)dgv.CurrentRow.DataBoundItem;
            
            return currentObject;
        }

        public void AddParametersOfDeletedWP(dynamic j, DataGridView dgv, string colName)
        {
            dgv.Columns.Add(colName, colName);
            List<Balance> list = j.GetItemsofTheSameVersion();

            for (int i = 0; i < list.Count-1; i++)
            {
                dgv[colName, i].Value = list[i].Length - list[i+1].Length ;
            }
        }
    }
}

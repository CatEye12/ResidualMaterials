using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public void CheckIfFieldsAreFilled(TextBox width, TextBox length, TextBox height)//for insertion
        {
            if (MyDtTable.residualType == true)
            {
                if (!string.IsNullOrEmpty(width.Text) && !string.IsNullOrEmpty(length.Text) && !string.IsNullOrEmpty(height.Text))

                { MyDtTable.isFieldsFilled = true; }
                else MyDtTable.isFieldsFilled = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(length.Text) && !string.IsNullOrEmpty(width.Text))

                { MyDtTable.isFieldsFilled = true; }
                else MyDtTable.isFieldsFilled = false;
            }
        }

        public void ConvTextToDouble(TextBox w, TextBox l, TextBox h)//for input
        {
            if (MyDtTable.isFieldsFilled == true)

            {
                if (MyDtTable.residualType == false)
                {
                    MyDtTable.widthDim = Convert.ToDouble(w.Text);
                    MyDtTable.length = Convert.ToDouble(l.Text);
                }
                else
                {
                    MyDtTable.widthDim = Convert.ToDouble(w.Text);
                    MyDtTable.length = Convert.ToDouble(l.Text);
                    MyDtTable.height = Convert.ToDouble(h.Text);
                }
            }
            else MessageBox.Show("Заполнены не все параметры остатка!");
        }
        public void ConvTxtToDouble(TextBox w, TextBox l)//for cut
        {
            if (MyDtTable.isFieldsFilled == true)

            {
                if (MyDtTable.residualType == false)
                {
                    MyDtTable.lengthWP = Convert.ToDouble(l.Text);
                }
                else
                {
                    MyDtTable.widthWP = Convert.ToDouble(w.Text);
                    MyDtTable.lengthWP = Convert.ToDouble(l.Text);
                }
            }
            else MessageBox.Show("Выберите остаток и заполните параметры заготовки!");
        }
        
        public void CheckType(ComboBox bx)
        {
            if (bx.SelectedIndex == 0) { MyDtTable.residualType = false; } else MyDtTable.residualType = true;
        }
    }
}

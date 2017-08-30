using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace ResidualMaterials
{
     class MyDtTable
     {
        string connection = "Data Source=pdmsrv;Initial Catalog=TaskDataBase;Persist Security Info=True;User ID=airventscad;Password=1";
        public MyDtTable()
        {

        }
        
        SqlConnection objCon;
        SqlCommandBuilder builder;
        DataTable dataTable = new DataTable();
        DataSet dataSet = new DataSet();
        SqlDataAdapter adapter;
        public List<Balance> data;
        int LastBalanceId;
        Balance itemToCutFrom;

        List<Balance> inputMaterial;
        bool isFieldsFilled;



        public List<Balance> MakingDataList(bool type)
        {
            if (type == false)
            {
                var list = (from DataRow row in dataTable.Rows where row["Type"].Equals(false)
               
                          select new Balance
                            {
                                BalanceID = (int)row["BalanceID"],
                                Type = (bool)row["Type"],
                                Dim = (int)row["Dim"],
                                Length = (int)row["Lenth"]
                            }).ToList();
                return list;
            }
            else
            {
                var list = (from DataRow row in dataTable.Rows where row["Type"].Equals(true)
                            select new Balance
                            {
                                BalanceID = (int)row["BalanceID"],
                                Type = (bool)row["Type"],
                                Length = (int)row["Lenth"],
                                W = (int)row["W"],
                                H = (int)row["H"]
                            }).ToList();
                return list;
            }
        }
        private List<Balance> ConvertInputDataToList(TextBox l, TextBox w, TextBox h)
        {
            var list = new List<Balance>() { new Balance
            {
                Type = true,
                Length = Convert.ToInt32(l.Text),
                W = Convert.ToInt32(w.Text),
                H = Convert.ToInt32(h.Text)
            } };

            return list;
        }

        private List<Balance> ConvertInputDataToList(TextBox l, TextBox dim)
        {
            int l_ = Convert.ToInt32(l.Text);
            int dim_ = Convert.ToInt32(dim.Text);

            var list = new List<Balance>() { new Balance
            {
                Type = false,

                Dim = dim_,
                Length = l_
            } };

            return list;           
        }

        public List<Balance> PushingDataInTable(bool type, TextBox l, TextBox w, TextBox h)
        {
            if (LastBalanceId == 0)
            {
                LastBalanceId = LastRowBalanceId();
            }
            else { LastBalanceId++; }


            if (type == true)
            {
                inputMaterial = ConvertInputDataToList(l, w, h); MessageBox.Show("Ploskoe");
            }
            else { inputMaterial = ConvertInputDataToList(l, h); MessageBox.Show("Telo vrascheniya"); }

            foreach (var item in inputMaterial)
            {
                //data.Add(item); // добавление материала в List
                dataTable.Rows.Add(LastBalanceId, item.Type, item.Dim, item.Length, item.W, item.H); //добавление материала в таблицу
            }
            //saving changes
            builder = new SqlCommandBuilder(adapter);
            adapter.Update(dataTable);
            data = MakingDataList(type);
            return data;
        }
        
        public DataTable Load_Data(bool type)
        {
            dataTable.Clear();

            objCon = new SqlConnection(connection);
            objCon.Open();
            adapter = new SqlDataAdapter("SELECT * FROM Balance", connection);

            adapter.Fill(dataSet);

            dataTable = dataSet.Tables[0];

            objCon.Close();

            data = MakingDataList(type);
            return dataTable;
        }
        
        private int LastRowBalanceId()
        {
            int maxBalanceID;
            objCon = new SqlConnection(connection);
            objCon.Open();
            SqlDataReader reader;
            SqlCommand command = new SqlCommand("GetLastBalanceID", objCon);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@param1", SqlDbType.Int).Direction = ParameterDirection.Output;

            reader = command.ExecuteReader();
            maxBalanceID = (int)command.Parameters["@param1"].Value;
            objCon.Close();
            return maxBalanceID + 1;
        }
        
        private Balance GetItemToCut(DataGridView dgv)
        {
            int balID = (int)dgv.SelectedRows[0].Cells[0].Value;
            MessageBox.Show(balID.ToString());
            List<Balance> itemToCutFrom = (from list in data where list.BalanceID == balID select list).ToList();


            /* EnumerableRowCollection<DataRow> query = from order in oTable.AsEnumerable()
                                                     where order.Field<SqlHierarchyId>("HierarchyId").GetAncestor(1).Equals(iID)
                                                     select order;*/


            //EnumerableRowCollection<DataRow> itemToCutFrom = (from list in dataTable.AsEnumerable() where list.Field<int>("BalanceID").Equals(balID) select list);
            
            return itemToCutFrom[0];
        }

        private bool CheckIfFieldsAreFilled(DataGridView dgv, bool residualType, TextBox width, TextBox length)
        {
            if (residualType == true)
            {
                if (!string.IsNullOrEmpty(width.Text) && !string.IsNullOrEmpty(length.Text) && dgv.SelectedRows != null)

                { return true; }
                else return false;
            }
            else
            {
                if (!string.IsNullOrEmpty(length.Text) && dgv.SelectedRows != null)

                { return true; }
                else return false;
            }
        }
        public bool CheckIfFieldsAreFilled(TextBox length, TextBox dimheight)
        {
            if (!string.IsNullOrEmpty(length.Text) && !string.IsNullOrEmpty(dimheight.Text))

            { return true; }
            else return false;
        }
        public bool CheckIfFieldsAreFilled(TextBox length, TextBox width, TextBox dimheight)
        {
            if (!string.IsNullOrEmpty(length.Text) && !string.IsNullOrEmpty(width.Text) && !string.IsNullOrEmpty(dimheight.Text))

            { return true; }
            else return false;
        }


        private bool CheckingWorkpieceLessThanResidual(DataGridView dgv, bool residualType, TextBox width, TextBox length)
        {
            isFieldsFilled = CheckIfFieldsAreFilled(dgv, residualType, width, length);
            if (isFieldsFilled == true)
            {
                itemToCutFrom = GetItemToCut(dgv);
                #region если поля заполнены
                if (residualType == true)
                {
                    int w = Convert.ToInt32(width.Text);
                    int l = Convert.ToInt32(length.Text);
                    int temp;
                    

                    //определяем большую сторону заготовки
                    if (l >= w) { }
                    else
                    {
                        temp = l;
                        l = w;
                        w = temp;
                    }

                    if (itemToCutFrom.Length >= l)
                    {
                        if (itemToCutFrom.W >= w)
                        {
                            return true;
                        }
                        else
                        {
                            if (itemToCutFrom.Length >= w)
                            {
                                if (itemToCutFrom.W >= l)

                                { return true; }

                            }
                            else
                                return false;
                        }

                        return false;
                    }
                    else
                    {
                        if (itemToCutFrom.W >= l)
                        {
                            if (itemToCutFrom.Length >= w)

                            { return true; }

                            else return false;
                        }
                        return false;
                    }
                }
                
                else { return  CheckingWorkpieceLessThanResidual(dgv, length); }
                #endregion
            }
            else
            {
                MessageBox.Show("Выберите остаток и заполните параметры заготовки!");
                return false;
            }
        }

        private bool CheckingWorkpieceLessThanResidual(DataGridView dgv, TextBox length)
        {
                int l = Convert.ToInt32(length.Text);

                if (itemToCutFrom.Length >= l) return true;
                else return false;
        }

        public void CutOut(DataGridView dgv, bool type, TextBox width, TextBox length)
        {
            bool possOrNot = CheckingWorkpieceLessThanResidual(dgv, type, width, length);
            int temp;
            int length_ = Convert.ToInt32(length.Text);

            if (possOrNot == true)
            {
                int new_Length = 0;
                int rowNumber = RowToInsert();

                if (type == false)

                {
                    new_Length = itemToCutFrom.Length - length_;

                    MessageBox.Show(new_Length.ToString());
                    dataTable.Rows[rowNumber].SetField(dataTable.Columns["Lenth"], new_Length);
                }
                else
                {
                    int new_Width = 0;
                    int width_ = Convert.ToInt32(width.Text);
L1:
                    if (itemToCutFrom.Length > length_)
                    {
                        if (itemToCutFrom.W > width_)
                        {/////////////
                            new_Length = itemToCutFrom.Length - length_;
                            new_Width = itemToCutFrom.W - width_;
                        }
                        else if (itemToCutFrom.W == width_)
                        {
                            new_Length = itemToCutFrom.Length - length_;
                            new_Width = itemToCutFrom.W;
                        }
                        else if (itemToCutFrom.W < width_)
                        {/////////////
                            new_Width = itemToCutFrom.W - length_;
                            new_Length = itemToCutFrom.Length - width_;
                        }
                    }

                    else if (itemToCutFrom.Length == length_)
                    {
                        if (itemToCutFrom.W > width_)
                        {
                            new_Length = itemToCutFrom.Length;
                            new_Width = itemToCutFrom.W - width_;
                        }
                    
                        else if (itemToCutFrom.W == width_)
                        {
                            new_Length = 0;
                            new_Width = 0;

                            //нужно удалять остаток
                        }
                    }

                    else if (itemToCutFrom.Length < length_)
                    {                      
                        temp = length_;
                        length_ = width_;
                        width_ = temp;
                        goto L1;
                    }
                    //меняем значения длинны/ширины обратно
                    temp = length_;
                    length_ = width_;
                    width_ = temp;

                    MessageBox.Show("new_Length  " +  new_Length.ToString());
                    MessageBox.Show("new_Width  " + new_Width.ToString());

                    dataTable.Rows[rowNumber].SetField(dataTable.Columns["Lenth"], new_Length);
                    dataTable.Rows[rowNumber].SetField(dataTable.Columns["W"], new_Width);

                }
                builder = new SqlCommandBuilder(adapter);
                adapter.Update(dataTable);
                MessageBox.Show("Заготовка вырезана!)");
            }
            else { MessageBox.Show("Невозможно вырезать заготовку. Параметры заготовки больше параметров остатка!"); }
            data = MakingDataList(type);
        }
        private int RowToInsert()
        {
            EnumerableRowCollection<DataRow> col = (from DataRow item in dataTable.AsEnumerable() where item.Field<int>(0).Equals(itemToCutFrom.BalanceID) select item);
            int id = 0;
            foreach (var item in col)
            {
                id = dataTable.Rows.IndexOf(item);
            }
            return id;
        }
    }
}
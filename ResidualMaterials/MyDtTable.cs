using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

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
        List<Balance> data;
        int LastBalanceId;
        Balance itemToCutFrom;

        bool isFieldsFilled;



        public List<Balance> MakingDataList()
        {
            var list = (from DataRow row in dataTable.Rows
                        select new Balance
                        {
                            BalanceID = (int)row["BalanceID"],
                            Type = (bool)row["Type"],
                            Dim = (int)row["Dim"],
                            Length = (int)row["Lenth"],
                            W = (int)row["W"],
                            H = (int)row["H"]
                        }).ToList();
            return list;
        }
        public List<Balance> ConvertInputDataToList(string l, string w, string h)
        {
            var list = new List<Balance>() { new Balance
            {
                Type = true,
                Length = Convert.ToInt32(l),
                W = Convert.ToInt32(w),
                H = Convert.ToInt32(h)
            } };

            return list;
        }

        public List<Balance> ConvertInputDataToList(string l, string dim)
        {
            var list = new List<Balance>() { new Balance
            {
                Type = false,

                Dim = Convert.ToInt32(dim),
                Length = Convert.ToInt32(l)
            } };

            return list;
        }

        public DataTable PushingDataInTable(List<Balance> list)
        {
            if (LastBalanceId == 0) { LastBalanceId = LastRowBalanceId(); }
            else { LastBalanceId++; }

            foreach (var item in list)
            {
                data.Add(item); // добавление материала в List
                dataTable.Rows.Add(LastBalanceId, item.Type, item.Dim, item.Length, item.W, item.H); //добавление материала в таблицу
                //object[] val = new object[] { BalanceId, item.Type, item.Dim, item.Length, item.W, item.H };
                //dataTable.LoadDataRow(val, true);
            }
            //saving changes
            builder = new SqlCommandBuilder(adapter);
            adapter.Update(dataTable);

            return dataTable;
        }

        public DataTable Load_Data()
        {
            objCon = new SqlConnection(connection);
            objCon.Open();
            adapter = new SqlDataAdapter("SELECT * FROM Balance", connection);

            adapter.Fill(dataSet);

            dataTable = dataSet.Tables[0];

            objCon.Close();

            data = MakingDataList();
            return dataTable;
        }
        // доработать
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
        
        public Balance GetItemToCut(DataGridView dgv)
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

        public bool CheckIfFieldsAreFilled(DataGridView dgv, bool residualType, TextBox width, TextBox length)
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

        public bool CheckingWorkpieceLessThanResidual(DataGridView dgv, bool residualType, TextBox width, TextBox length)
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

        public bool CheckingWorkpieceLessThanResidual(DataGridView dgv, TextBox length)
        {
                int l = Convert.ToInt32(length.Text);

                if (itemToCutFrom.Length >= l) return true;
                else return false;
        }

        public void CutOut(DataGridView dgv, bool type, TextBox width, TextBox length)
        {
            bool possOrNot = CheckingWorkpieceLessThanResidual(dgv, type, width, length);

            if (possOrNot == true)
            {
                int new_Length = 0; 
                int rowNumber = dgv.SelectedRows[0].Cells[0].RowIndex;

                if (type == false)

                {
                    MessageBox.Show(new_Length.ToString());

                    new_Length = itemToCutFrom.Length - Convert.ToInt32(length.Text);
                    dataTable.Rows[rowNumber].SetField(dataTable.Columns["Lenth"], new_Length);
                }
                else
                {
                    int new_Width = 0;
                    if (itemToCutFrom.Length >= Convert.ToInt32(length.Text) && itemToCutFrom.W >= Convert.ToInt32(width.Text))
                    {
                        new_Length = itemToCutFrom.Length - Convert.ToInt32(length.Text);
                        new_Width = itemToCutFrom.W - Convert.ToInt32(width.Text);
                    }
                    else
                    {
                        new_Width = itemToCutFrom.W - Convert.ToInt32(length.Text);
                        new_Length = itemToCutFrom.Length - Convert.ToInt32(width.Text);
                    }

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
            data = MakingDataList();
        }
    }
}
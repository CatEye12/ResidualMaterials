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
        DataSet dataSet = new DataSet();
        SqlDataAdapter adapterR;
        SqlDataAdapter adapterPlane;

        public DataTable dataTablePl = new DataTable();
        public DataTable dataTableR = new DataTable();
        public List<Balance> data;

        int LastBalanceId;
        Balance itemToCutFrom;
        List<Balance> inputMaterial;
        public static bool residualType;
        public static bool isFieldsFilled;


        public static double widthDim { get; set; }
        public static double length { get; set; }
        public static double height { get; set; }
        public static double lengthWP { get; set; }
        public static double widthWP { get; set; }

        
        public List<Balance> MakingDataList(bool type)
        {
            if (type == false)
            {
                var list = (from DataRow row in dataTableR.Rows where row["Type"].Equals(false)
               
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
                var list = (from DataRow row in dataTablePl.Rows where row["Type"].Equals(true)
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
        private List<Balance> ConvertInputDataToList(double l, double w, double h)
        {
            var list = new List<Balance>() { new Balance
            {
                Type = true,
                Length = l,
                W = w,
                H = h
            } };

            return list;
        }

        private List<Balance> ConvertInputDataToList(double l, double dim)
        {
            var list = new List<Balance>() { new Balance
            {
                Type = false,

                Length = l,
                Dim = dim
            } };

            return list;           
        }

        public void PushingDataInTable()
        {
            if (isFieldsFilled == true)
            {
                double l = length;
                double w = widthDim;
                double h = height;

                if (LastBalanceId == 0)
                {
                    LastBalanceId = LastRowBalanceId();
                }
                else { LastBalanceId++; }


                if (residualType == true)
                {
                    inputMaterial = ConvertInputDataToList(l, w, h); MessageBox.Show("Ploskoe");

                    foreach (var item in inputMaterial)
                    {
                        dataTablePl.Rows.Add(LastBalanceId, item.Type, item.Dim, item.Length, item.W, item.H); //добавление материала в таблицу
                    }
                    //saving changes
                    builder = new SqlCommandBuilder(adapterPlane);
                    adapterPlane.Update(dataTablePl);
                }
                else
                {
                    inputMaterial = ConvertInputDataToList(l, w); MessageBox.Show("Telo vrascheniya");

                    foreach (var item in inputMaterial)
                    {
                        dataTableR.Rows.Add(LastBalanceId, item.Type, item.Dim, item.Length, item.W, item.H);
                    }
                    //saving changes
                    builder = new SqlCommandBuilder(adapterR);
                    adapterR.Update(dataTableR);
                }
            }
        }
        
        public void Load_Data(bool type)
        {
            objCon = new SqlConnection(connection);
            objCon.Open();
            
            adapterR = new SqlDataAdapter("SELECT * FROM Balance WHERE Type = 'false'", connection);
            adapterPlane = new SqlDataAdapter("SELECT * FROM Balance WHERE Type = 'true'", connection);

            adapterR.Fill(dataSet, "Тело вращения");
            adapterPlane.Fill(dataSet, "Плоское");

            dataTableR = dataSet.Tables["Тело вращения"];
            dataTablePl = dataSet.Tables["Плоское"];

            data = MakingDataList(residualType);

            objCon.Close();
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
            int balID = dgv.SelectedCells[0].RowIndex;

            return data[balID];
            
            /* EnumerableRowCollection<DataRow> query = from order in oTable.AsEnumerable()
                                                     where order.Field<SqlHierarchyId>("HierarchyId").GetAncestor(1).Equals(iID)
                                                     select order;*/
        }
        
        private bool CheckingWorkpieceLessThanResidual(DataGridView dgv)
        {
            double l = lengthWP;
            double w = widthWP;
            if (isFieldsFilled == true)
            {
                data = MakingDataList(residualType);
                itemToCutFrom = GetItemToCut(dgv);
                #region если поля заполнены
                if (residualType == true)
                {
                    double temp;
                    
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
                
                else
                {
                    l = lengthWP;
                    if (itemToCutFrom.Length >= l) return true;
                    else return false;
                }
                #endregion
            }
            else
            {
                MessageBox.Show("Выберите остаток и заполните параметры заготовки!");
                return false;
            }
        }
        
        public void CutOut(DataGridView dgv, bool type)
        {
            double length_ = lengthWP;
            double width_ = widthWP;
            double temp;

            bool possOrNot = CheckingWorkpieceLessThanResidual(dgv);
            
            if (possOrNot == true)
            {
                double new_Length = 0;
                int rowNumber = RowToInsert();

                if (type == false)

                {
                    new_Length = itemToCutFrom.Length - length_;

                    MessageBox.Show(new_Length.ToString());
                    dataTableR.Rows[rowNumber].SetField(dataTableR.Columns["Lenth"], new_Length);

                    builder = new SqlCommandBuilder(adapterR);
                }
                else
                {
                    double new_Width = 0;
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

                    MessageBox.Show("new_Length  " + new_Length.ToString());
                    MessageBox.Show("new_Width  " + new_Width.ToString());

                    dataTablePl.Rows[rowNumber].SetField(dataTablePl.Columns["Lenth"], new_Length);
                    dataTablePl.Rows[rowNumber].SetField(dataTablePl.Columns["W"], new_Width);

                    builder = new SqlCommandBuilder(adapterPlane);

                }
                adapterR.Update(dataTableR);
                adapterPlane.Update(dataTablePl);
                MessageBox.Show("Заготовка вырезана!)");

            }
            else { MessageBox.Show("Невозможно вырезать заготовку. Параметры заготовки больше параметров остатка!"); }

            dgv.DataSource = FillDgv();
        }
        private int RowToInsert()
        {
            int id = 0;
            if (residualType == false)
            {
                EnumerableRowCollection<DataRow> col = (from DataRow item in dataTableR.AsEnumerable() where item.Field<int>(0).Equals(itemToCutFrom.BalanceID) select item);
                foreach (var item in col)
                {
                    id = dataTableR.Rows.IndexOf(item);
                }
            }
            else
            {
                EnumerableRowCollection<DataRow> col = (from DataRow item in dataTablePl.AsEnumerable() where item.Field<int>(0).Equals(itemToCutFrom.BalanceID) select item);
                foreach (var item in col)
                {
                    id = dataTablePl.Rows.IndexOf(item);
                }
            }            
            return id;
        }
        public DataTable FillDgv()
        {
            DataTable dtToFill = new DataTable();
            if (residualType == false)
            {
                dtToFill.Columns.Add("Диаметр");
                dtToFill.Columns.Add("Длинна");
                for (int i = 0; i < dataTableR.Rows.Count; i++)
                {
                    dtToFill.Rows.Add();
                        
                    dtToFill.Rows[i]["Диаметр"] = dataTableR.Rows[i]["Dim"];
                    dtToFill.Rows[i]["Длинна"] = dataTableR.Rows[i]["Lenth"];
                }
            }
            else
            {
                dtToFill.Columns.Add("Длинна");
                dtToFill.Columns.Add("Ширина");
                dtToFill.Columns.Add("Толщина");
                for (int i = 0; i < dataTablePl.Rows.Count; i++)
                {
                    dtToFill.Rows.Add();

                    dtToFill.Rows[i]["Длинна"] = dataTablePl.Rows[i]["Lenth"];
                    dtToFill.Rows[i]["Ширина"] = dataTablePl.Rows[i]["W"];
                    dtToFill.Rows[i]["Толщина"] = dataTablePl.Rows[i]["H"];
                }
            }
            return dtToFill;
        }

    }
}
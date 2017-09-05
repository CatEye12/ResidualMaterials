using System.Collections.Generic;
using System.Linq;
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
            dataList = new List<Balance>();
        }
        
        SqlConnection objCon;
        
        public List<Balance> dataList;
        public List<Balance> dataListToView;

        int LastBalanceId;
        int lastVersion;

        public static Balance itemToCutFrom;
        List<Balance> inputMaterial;
        public static bool residualType;
        public static bool isFieldsFilled;


        public static decimal widthDim { get; set; }
        public static decimal length { get; set; }
        public static decimal height { get; set; }
        public static decimal lengthWP { get; set; }
        public static decimal widthWP { get; set; }
        public static int name { get; set; }
        public static int version { get; set; }


        public void Load_Data(bool type)
                {
                    objCon = new SqlConnection(connection);
                    objCon.Open();
            
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Balance " + type, objCon);
                        
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dataList.Add(new Balance()
                            {
                                BalanceID = (int)reader["BalanceId"], 
                                Type = (bool)reader["Type"],
                                Dim = (decimal)reader["Dim"],
                                Length = (decimal)reader["Lenth"],
                                W = (decimal)reader["W"],
                                H = (decimal)reader["H"],
                                Name = (int)reader["Name"],
                                Version = (int)reader["Version"]
                            }); //Specify column index 
                        }
                    }
                    objCon.Close();
                }
        public List<Balance> MakingDataList()
        {
            /*var list = from item in dataList where item.Type.Equals(residualType)               
                        select new Balance
                        {
                            BalanceID = item.BalanceID,
                            Type = item.Type,
                            Dim = item.Dim,
                            Length = item.Length, 
                            W = item.W,
                            H = item.H,
                            Name = item.Name,
                            Version = item.Version
                        };
            return list.ToList(); */


            List<Balance> result = new List<Balance>();
            Balance bal;
            var list = (from it in 
                            (from item in dataList where item.Type.Equals(residualType) group item by item.Name ) 
                        orderby version descending select it);
            foreach (var item in list)
            {
                item.AsEnumerable();
                bal = new Balance() {BalanceID = item.Last().BalanceID, Type = item.Last().Type,
                                     Dim = item.Last().Dim, Length = item.Last().Length, W = item.Last().W,
                                     H = item.Last().H, Name = item.Last().Name, Version = item.Last().Version};
                result.Add(bal);
            }                     
                                
            return result;    
        }
        public List<Balance> GetItemsofTheSameVersion()
        {
            var list = (from item in dataList where item.Name.Equals(itemToCutFrom.Name) select item).ToList();
            return list;
        }

        public void PushingDataInTable()
        {
            if (isFieldsFilled == true)
            {
                if (CheckNameUniquenes())
                {
                   
                    if (residualType == true)
                    {
                        inputMaterial = ConvertInputDataToList(name, length, widthDim, height); MessageBox.Show("Ploskoe");
                    }
                    else
                    {
                        inputMaterial = ConvertInputDataToList(name, length, widthDim); MessageBox.Show("Telo vrascheniya");
                    }

                    SaveNewMaterialDb(inputMaterial[0].Name, inputMaterial[0].Type, inputMaterial[0].Dim, inputMaterial[0].Length, inputMaterial[0].W, inputMaterial[0].H, 0);
                }
                else MessageBox.Show("Материал с таким именем уже существует!");
            }
        }

        private List<Balance> ConvertInputDataToList(int n, decimal l, decimal w, decimal h)
                {
                    var list = new List<Balance>() { new Balance
                    { 
                        Name = n,
                        Type = true,
                        Length = l,
                        W = w,
                        H = h
                    } };

                    return list;
                }
        private List<Balance> ConvertInputDataToList(int n, decimal l, decimal dim)
        {
            var list = new List<Balance>() { new Balance
            {
                Name = n,
                Type = false,
                Length = l,
                Dim = dim
            } };

            return list;           
        }
        private bool CheckNameUniquenes()
        {
            var list = (from it in dataList where it.Name.Equals(name) select it).ToList();

            if (list.Count != 0)
            {
                return false;
            }
            else return true;
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
            return maxBalanceID;
        }
        public void SaveNewMaterialDb(int name, bool type, decimal dim, decimal length, decimal w, decimal h, int version)
        {
            objCon.Open();
            SqlCommand save = new SqlCommand("AddMaretial");
            SqlDataReader reader;
            save.CommandType = CommandType.StoredProcedure;
            save.Connection = objCon;

            save.Parameters.AddWithValue("@name", name);
            save.Parameters.AddWithValue("@type", type);
            save.Parameters.AddWithValue("@dim", dim);
            save.Parameters.AddWithValue("@length", length);
            save.Parameters.AddWithValue("@w", w);
            save.Parameters.AddWithValue("@h", h);
            save.Parameters.AddWithValue("@version", version);
            reader = save.ExecuteReader();
            
            objCon.Close();
            if (LastBalanceId == 0)
            {
                LastBalanceId = LastRowBalanceId();
            }
            else { LastBalanceId++; }
            dataList.Add(new Balance {BalanceID = LastBalanceId, Version = lastVersion, Type = type, Dim = dim, Length = length, W = w, H = h, Name = name});            
        }
               

        public void CutOut()
        {
            decimal temp;

            bool possOrNot = CheckingWorkpieceLessThanResidual();
            
            if (possOrNot == true)
            {
                decimal new_Length = 0;
                decimal new_Width = 0;
                lastVersion = LastVersion(itemToCutFrom.BalanceID);

                if (residualType == false)

                {
                    new_Length = itemToCutFrom.Length - lengthWP;

                    MessageBox.Show(new_Length.ToString());
                }
                else
                {
                    
                L1:
                    if (itemToCutFrom.Length > lengthWP)
                    {
                        if (itemToCutFrom.W > widthWP)
                        {/////////////
                            new_Length = itemToCutFrom.Length - lengthWP;
                            new_Width = itemToCutFrom.W - widthWP;
                        }
                        else if (itemToCutFrom.W == widthWP)
                        {
                            new_Length = itemToCutFrom.Length - lengthWP;
                            new_Width = itemToCutFrom.W;
                        }
                        else if (itemToCutFrom.W < widthWP)
                        {/////////////
                            new_Width = itemToCutFrom.W - lengthWP;
                            new_Length = itemToCutFrom.Length - widthWP;
                        }
                    }

                    else if (itemToCutFrom.Length == lengthWP)
                    {
                        if (itemToCutFrom.W > widthWP)
                        {
                            new_Length = itemToCutFrom.Length;
                            new_Width = itemToCutFrom.W - widthWP;
                        }

                        else if (itemToCutFrom.W == widthWP)
                        {
                            new_Length = 0;
                            new_Width = 0;

                            //нужно удалять остаток
                        }
                    }

                    else if (itemToCutFrom.Length < lengthWP)
                    {
                        temp = lengthWP;
                        lengthWP = widthWP;
                        widthWP = temp;
                        goto L1;
                    }
                    //меняем значения длинны/ширины обратно
                    temp = lengthWP;
                    lengthWP = widthWP;
                    widthWP = temp;

                    MessageBox.Show("new_Length  " + new_Length.ToString());
                    MessageBox.Show("new_Width  " + new_Width.ToString());
                }

                SaveNewMaterialDb(itemToCutFrom.Name, itemToCutFrom.Type, itemToCutFrom.Dim, new_Length, new_Width, itemToCutFrom.H, lastVersion);
                MessageBox.Show("Заготовка вырезана!)");
            }
            else { MessageBox.Show("Невозможно вырезать заготовку. Параметры заготовки больше параметров остатка!"); }
        }

        private bool CheckingWorkpieceLessThanResidual()
        {
            decimal l = lengthWP;
            decimal w = widthWP;
            if (isFieldsFilled == true)
            {
                #region если поля заполнены
                if (residualType == true)
                {
                    decimal temp;

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

                                { return true;
                                }

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

                            { return true;
                            }

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
        private int LastVersion(int balID)
        {
            objCon = new SqlConnection(connection);
            objCon.Open();
            SqlDataReader reader;
            SqlCommand command = new SqlCommand("GetLastVersion", objCon);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@balID", balID);
            command.Parameters.Add("@lastVersion", SqlDbType.Int).Direction = ParameterDirection.Output;
            reader = command.ExecuteReader();
            version = (int)command.Parameters["@lastVersion"].Value;
            objCon.Close();
            return version + 1;
        }


        private int RowToInsert()
        {
            int id = 0;
            
            var col = (from item in dataList where item.BalanceID.Equals(itemToCutFrom.BalanceID) select item);
            foreach (var item in col)
            {
                id = dataList.IndexOf(item);
            }
            return id;
        }
       
    }
}
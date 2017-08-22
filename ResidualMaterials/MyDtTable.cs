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
        DataTable dataTable = new DataTable();
        DataSet dataSet = new DataSet();
        SqlDataAdapter adapter;
        public List<Balance> data;
        int BalanceId;
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
        public List<Balance> ConvertTo(string l, string w, string h)
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

        public List<Balance> ConvertTo(string l)
        {
            var list = new List<Balance>() { new Balance
            {
                Type = false,

                //Dim = Convert.ToInt32(dim),
                Length = Convert.ToInt32(l)
            } };

            return list;
        }

        public DataTable PushingDataInTable(List<Balance> list)
        {
            if (BalanceId == 0) { BalanceId = LastRowBalanceId(); }
            else { BalanceId++; }

            foreach (var item in list)
            {
                data.Add(item);
                dataTable.Rows.Add(BalanceId, item.Type, item.Dim, item.Length, item.W, item.H);
                //object[] val = new object[] { BalanceId, item.Type, item.Dim, item.Length, item.W, item.H };
                //dataTable.LoadDataRow(val, true);
            }
            //saving changes
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
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
        

        public void GetItemToCut(DataGridView dgv)
        {
            int balID = (int)dgv.SelectedRows[0].Cells[0].Value;
            MessageBox.Show(balID.ToString());
            //EnumerableRowCollection<DataRow> itemToCut = from list in listOfData.AsEnumerable() where list.Field<Int32>("BalanceID").Equals(balID) select list;// рабочая строка
            List<Balance> itemToCut2 = (from list in data where list.BalanceID == balID select list).ToList();
            foreach (var item in itemToCut2)
            {
                MessageBox.Show(item.BalanceID.ToString() + " " + item.W.ToString() + " " + item.Length.ToString());
            }
        }
    }
}
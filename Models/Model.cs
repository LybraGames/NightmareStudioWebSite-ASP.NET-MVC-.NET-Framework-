using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//ADO.NET library
using System.Data;
using System.Data.SqlClient;
using DataTable = System.Data.DataTable;

namespace Model
{
    public class SQL_local_db
    {
        public static bool isInit = false;

        //Managed Provider Components
        public static SqlConnection sqlConnection;
        public static SqlCommand sqlCommand;
        public static SqlDataReader sqlDataReader;
        public static SqlDataAdapter sqlDataAdapter;

        //Content Components
        public static DataSet dataSet;
        public static DataTable dataTable;

        static string sqlConnectionURL = null;
        public static int rowsNumber = 0;
        public static int columnsNumber = 5;
        public static string[] columnNames = { "ID", "Username", "Email", "Password","Premium"};

        public static void Init() 
        {
            isInit = true;

            sqlConnectionURL = @"
            Data Source=(localdb)\NightmareStudioWebSiteDB;
            Initial Catalog=Database;
            Integrated Security=True;
            Connect Timeout=30;
            Encrypt=False;
            TrustServerCertificate=False;
            ApplicationIntent=ReadWrite;
            MultiSubnetFailover=False";
            //sqlConnectionURL = @"Server=tcp:nightmarestudio.database.windows.net,1433;Initial Catalog=nightmarestudio;Persist Security Info=False;User ID=kowalski;Password=Adevarat1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


            sqlConnection = new SqlConnection(sqlConnectionURL);
            sqlConnection.Open();

            //Command
            sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;

            //Reader
            sqlCommand.CommandText = "select ID from DataTable;";
            sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read()) { rowsNumber++; }
            sqlDataReader.Close();

            //Adapter
            sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;

            //DataTable
            sqlCommand.CommandText = "select * from DataTable;";
            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            for (int i = 0; i < columnsNumber; i++) dataTable.Columns[i].ColumnName = columnNames[i];

            sqlCommand.ExecuteNonQuery();

            //DataSet
            dataSet = new DataSet();
            dataSet.Tables.Add(dataTable);
        }

    }

    public class Account 
    {
        public static bool isLogin = false;
        public static bool isValid = false;

        public static bool loginError = false;
        public static bool registerError = false;
        public static bool editProfileError = false;


        public string username_ { get; set; }
        public string password_ { get; set; }
        public string email_ { get; set; }

        public static string email { get; set; }
        public static string username { get; set; }
        public static string password { get; set; }
        public static bool isPremium { get; set; }
    }
}
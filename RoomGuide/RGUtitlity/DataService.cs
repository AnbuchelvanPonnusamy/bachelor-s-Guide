using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data;
using System.Globalization;
using System.Xml;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;


namespace RGUtitlity
{
    public class DataService
    {
        private SqlDatabase database;
        private DbConnection conncetion;
        RoomGuideDataStructure.DataService dataService = new RoomGuideDataStructure.DataService();

        public DataService(RoomGuideDataStructure.DataService _dataService)
        {
            dataService = _dataService;
            this.database = new SqlDatabase(this.GetConnectionString(dataService));
        }

        public DataTable ExecuteDataTable()
        {

            SqlConnection connection = new SqlConnection(GetConnectionString(dataService));
            connection.Open();
            string query = "SELECT * FROM [Members]";
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            DataTable dt = new DataTable();
            dt.Load(sqlCommand.ExecuteReader());
            connection.Close();
            return dt;
        }
        

        private void makeConnection(RoomGuideDataStructure.DataService dataService)
        {

            SqlConnection connection = new SqlConnection(GetConnectionString(dataService));
            connection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Members", connection);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            connection.Close();
        }

       
        private string GetConnectionString(RoomGuideDataStructure.DataService dataService)
        {
            string connectionString = string.Empty;

            try
            {
                connectionString = "Data Source=" + dataService.serverName + "; Initial Catalog=" + dataService.databaseName + ";User ID=" + dataService.userName + ";Password=" + dataService.password + ";";
                return connectionString;
            }
            catch (Exception connectionException)
            {
                return connectionString;
                throw connectionException;
            }
        }
    }
}

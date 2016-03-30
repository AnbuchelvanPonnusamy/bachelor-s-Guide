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
using System.IO;
using System.Configuration;


namespace RGUtitlity
{
    public class DataService
    {
        SqlConnection sqlConnection = new SqlConnection();

        public DataService()
        {
            RoomGuideDataStructure.DataService dataServiceValue = new RoomGuideDataStructure.DataService();
            dataServiceValue.serverName = GetConfigValue("ServerName");
            dataServiceValue.userName = GetConfigValue("UserName");
            dataServiceValue.databaseName = GetConfigValue("Database");
            dataServiceValue.password = GetConfigValue("Password");
            this.sqlConnection = this.GetConnection(dataServiceValue);
        }

        public DataService(RoomGuideDataStructure.DataService dataService)
        {
            this.sqlConnection = this.GetConnection(dataService);
        }

        public DataTable ExecuteDataTable(string query)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                DataTable dataTable = new DataTable();
                dataTable.Load(sqlCommand.ExecuteReader());
                sqlConnection.Close();
                return dataTable;
            }
            catch (Exception exception)
            {
                sqlConnection.Close();
                return new DataTable();
            }
        }

        private string GetConfigValue(string configKey)
        {
            var value = ConfigurationSettings.AppSettings[configKey];
            return value;
        }

        private SqlConnection GetConnection(RoomGuideDataStructure.DataService dataService)
        {
            string connectionString = string.Empty;
            try
            {
                connectionString = "Data Source=" + dataService.serverName + "; Initial Catalog=" + dataService.databaseName + ";User ID=" + dataService.userName + ";Password=" + dataService.password + ";";

                return new SqlConnection(connectionString);
            }
            catch (Exception connectionException)
            {
                return new SqlConnection(connectionString);
                throw connectionException;
            }
        }
    }
}

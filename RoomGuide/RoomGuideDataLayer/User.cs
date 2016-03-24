using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomGuideDataLayer
{
    public class User
    {
        public bool IsVaildUser(string userName, string password)
        {
            RoomGuideDataStructure.DataService dataServiceStructure = new RoomGuideDataStructure.DataService();
            dataServiceStructure.databaseName = "bachelorGuide";
            dataServiceStructure.password = "Sql2012";
            dataServiceStructure.userName = "sa";
            dataServiceStructure.serverName = @"5GWSIT130\SQLEXPRESS";


            RGUtitlity.DataService dataService = new RGUtitlity.DataService(dataServiceStructure);
            dataService.ExecuteDataTable();
            return false;
        }
    }
}

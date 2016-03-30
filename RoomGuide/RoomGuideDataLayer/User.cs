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
            RGUtitlity.DataService dataService = new RGUtitlity.DataService();
            string query = @"SELECT * FROM USERS WHERE USERNAME = '"+userName +"' AND PASSWORD = '"+ password +"'";
            dataService.ExecuteDataTable(query);
            return false;
        }
    }
}

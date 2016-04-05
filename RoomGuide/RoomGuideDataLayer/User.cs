using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomGuideDataLayer
{
    public class UserLayer
    {
        public RGResponse<string> IsVaildUser(string userName, string password)
        {
            try
            {
                DataTable userTable = new DataTable();
                RGResponse<string> resp = new RGResponse<string>(null);
                RGUtitlity.DataService dataService = new RGUtitlity.DataService();
                string query = @"SELECT * FROM bachelorGuide.dbo.[USERS] WHERE USERNAME = '" + userName + "' AND PASSWORD = '" + password + "'";
                userTable = dataService.ExecuteDataTable(query);
                if (userTable.Rows.Count > 0)
                {
                    return new RGResponse<string>(RGResult.Success, "Valid User", "");
                }
                else
                {
                    return new RGResponse<string>(RGResult.Failure, "Invalid User", "welldone");
                }
            }
            catch (Exception ex)
            {
                return new RGResponse<string>(RGResult.Failure, "Invalid User", ex.ToString());
            }
        }
    }
}

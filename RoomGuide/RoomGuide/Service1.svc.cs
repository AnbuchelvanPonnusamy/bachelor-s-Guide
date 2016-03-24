using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using RoomGuideDataLayer;


namespace RoomGuide
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class GuideService : IGuideService
    {
        public string GetDatas(string value)
        {
            return string.Format("You entered: {0}", value);
        }
        public string GetData()
        {
            return string.Format("You entered: {0}");
        }

        public string login(string userName, string password)
        {
            User user = new User();
            user.IsVaildUser("anbu", "anbu");
            return "anbu";
        }
        public string Logout()
        {
            return string.Format("You entered: {0}");
        }
        public string ValidateUser(string userName, string password, string rememberMe)
        {
            return string.Format("You entered: {0}");
        }
       
    }
}

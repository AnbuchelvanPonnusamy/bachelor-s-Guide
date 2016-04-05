using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using RoomGuideDataLayer;
using System.Web.Http.Cors;

namespace RoomGuide.Resource
{
    [EnableCors(origins: "http://localhost:59538/RoomGuideService.svc/login", headers: "*", methods: "*")]
    public partial class ServiceImpl : IGuideService
    {
        public string login(string userName, string password)
        {
            string result = string.Empty;
            UserLayer user = new UserLayer();
            JavaScriptSerializer jsonString = new JavaScriptSerializer();
            result = jsonString.Serialize(user.IsVaildUser(userName,password));

            return result;
        }
        public string Logout()
        {
            return string.Format("You entered: {0}");
        }

        public string ValidateUser(string userName, string password)
        {
            string result = string.Empty;
            UserLayer user = new UserLayer();
            JavaScriptSerializer jsonString = new JavaScriptSerializer();
            result = jsonString.Serialize(user.IsVaildUser(userName, password));
            return result;
        }
    }
}
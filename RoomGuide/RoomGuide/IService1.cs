using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RoomGuide
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IGuideService
    {

        [OperationContract]
        [WebGet(UriTemplate = "getdata/{value}/")]
        string GetDatas(string value);

        [OperationContract]
        [WebGet(UriTemplate = "login/{userName}/{password}")]
        string login(string userName, string password);

        [OperationContract]
        [WebGet(UriTemplate = "/Check")]
        string GetData();

        /// <summary>
        /// Log out
        /// </summary>
        /// <returns>logout message</returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "/logout", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string Logout();



        /// <summary>
        /// Log In
        /// </summary>
        /// <param name="userName">Name of the User.</param>
        /// <param name="password">The password.</param>
        /// <param name="rememberMe">The remember me.</param>
        /// <returns>true or false.</returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "/login", Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string ValidateUser(string userName, string password, string rememberMe);

        // TODO: Add your service operations here
    }

}

using RoomGuideDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace RoomGuide.Resource
{
    public partial class ServiceImpl : IGuideService
    {
        public string GetRoomMembers()
        {
            string result = string.Empty;
            try
            {
                RoomGuideDataLayer.RoomMembers roomMembers = new RoomGuideDataLayer.RoomMembers();
                JavaScriptSerializer jsonString = new JavaScriptSerializer();
                result = jsonString.Serialize(roomMembers.GetRoomMembers());
                return result;
            }
            catch (Exception e)
            {
                return result;
            }
        }

    }
}
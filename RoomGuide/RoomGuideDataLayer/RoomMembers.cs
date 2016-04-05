using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomGuideDataLayer
{
   public class RoomMembers
    {
        private RoomGuideDataStructure.RoomMembers RetriveMemberObject(DataRow memberRow)
        {
            RoomGuideDataStructure.RoomMembers roomMember = new RoomGuideDataStructure.RoomMembers();
            try
            {
                roomMember.FirstName = memberRow["FirstName"].ToString();
                roomMember.Id = memberRow["Id"].ToString();
                roomMember.LastName = memberRow["LastName"].ToString();
                roomMember.PhoneNumber = long.Parse(memberRow["PhoneNumber"].ToString());
                roomMember.MailId = memberRow["MailId"].ToString();
                roomMember.Message = memberRow["Messages"].ToString();
            }
            catch(Exception exception)
            {
                roomMember = new RoomGuideDataStructure.RoomMembers();
            }

            return roomMember;

        }

        public RGResponse<List<RoomGuideDataStructure.RoomMembers>> GetRoomMembers()
        {
            DataTable Members = new DataTable();
            List<RoomGuideDataStructure.RoomMembers> roomMembersList = new List<RoomGuideDataStructure.RoomMembers>();
            RoomGuideDataStructure.RoomMembers roomMembers = new RoomGuideDataStructure.RoomMembers();
            RGResponse<List<RoomGuideDataStructure.RoomMembers>> resp = new RGResponse<List<RoomGuideDataStructure.RoomMembers>>(null);
            try
            {
                RGUtitlity.DataService dataService = new RGUtitlity.DataService();
                string query = @"SELECT * FROM bachelorGuide.dbo.[Members] ";
                Members = dataService.ExecuteDataTable(query);
                if (Members.Rows.Count <= 0)
                {
                    roomMembersList = (from temp in new string[] { string.Empty }
                              select new RoomGuideDataStructure.RoomMembers
                              {
                                  FirstName = string.Empty,
                                  Id = string.Empty,
                                  LastName = string.Empty,
                                  MailId = string.Empty,
                                  PhoneNumber = 0,
                              }).ToList();

                }
                else
                {
                    for (int index = 0; index <= Members.Rows.Count - 1 ; index++)
                    {
                       roomMembersList.Add(RetriveMemberObject(Members.Rows[index]));
                    }
                }

                resp = new RGResponse<List<RoomGuideDataStructure.RoomMembers>>(RGResult.Success, roomMembersList, "");
            }
            catch (Exception exception)
            {
                resp = new RGResponse<List<RoomGuideDataStructure.RoomMembers>>(RGResult.Success, null, "Error occured.");
            }

            return resp;
        }
    }
}

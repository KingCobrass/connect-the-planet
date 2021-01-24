using System;
using System.Collections.Generic;
using System.Text;

namespace Connect.Interface.Chat
{
    public interface IConnectGroupInfo
    {
        long Id { get; set; }
        string GroupId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        DateTime CreateDate { get; set; }
        DateTime? ModifyDate { get; set; }
        List<string> MemberIds { get; set; }
        int IsActive { get; set; }
    }
}

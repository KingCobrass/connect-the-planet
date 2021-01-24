using System;
using System.Collections.Generic;
using System.Text;

namespace connect.Interface.User
{
    public interface IUser
    {
        string Id { get; set; }
        string Name { get; set; }
        string FirstName { get; set; }
        string lastName { get; set; }
        string Email { get; set; }
        DateTime CreateDate { get; set; }
        DateTime? ModifyDate { get; set; }
        DateTime? LastSighInDate { get; set; }
        string ConnectionId { get; set; }
        string Token { get; set; }
        int IsLive { get; set; }
        int IsActive { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Connect.Interface.Roles
{
    public interface IRole
    {
        int Id { get; set; }
        string Name { get; set; }
        string RoleId { get; set; }
        string Description { get; set; }
        int IsActive { get; set; }
    }
}

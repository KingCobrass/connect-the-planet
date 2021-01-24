using Connect.Interface.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Connect.API.Models
{
    public class ConnectCredentials : IConnectCredentials
    {
        [Required(ErrorMessage = "Email Is Required")]
        [DataType(DataType.EmailAddress)]
        [StringLength(200, ErrorMessage = "The {0} must be at least (3) and at most (200) characters long.", MinimumLength = 3)]
        public string Email { get; set; }
    }
}

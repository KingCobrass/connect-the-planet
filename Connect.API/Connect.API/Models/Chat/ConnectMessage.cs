using Connect.Interface.Chat;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Connect.API.Models.Chat
{
    public class ConnectMessage : IConnectMessage
    {
        public ConnectMessage()
        {
            MessageDate = DateTime.UtcNow;
            IsActive = 1;
        }
        public long Id { get; set; }

        [Required(ErrorMessage = "User Full Name Is Required")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "The {0} must be at least (6) and at most (100) characters long.", MinimumLength = 3)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "User Id Is Required")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "The {0} must be at least (6) and at most (50) characters long.", MinimumLength = 6)]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Message Is Required")]
        [DataType(DataType.Text)]
        [StringLength(10000, ErrorMessage = "The {0} must be at least (1) and at most (10000) characters long.", MinimumLength = 1)]
        public string Message { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime MessageDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        [Required(ErrorMessage = "GroupId Is Required")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "The {0} must be at least (6) and at most (50) characters long.", MinimumLength = 6)]
        public string GroupId { get; set; }
        [Required(ErrorMessage = "ToConnectionId Is Required")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "The {0} must be at least (8) and at most (100) characters long.", MinimumLength = 6)]
        public string ToConnectionId { get; set; }
        public int IsActive { get; set; }
    }
}

using connect.Interface.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Connect.API.Models
{
    public class User : IUser
    {
        public string Id { get; set; }
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "The {0} must be at least (4) and at most (100) characters long.", MinimumLength = 4)]
        public string Name { get; set; }
        [Required(ErrorMessage = "First Name Is Required")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "The {0} must be at least (2) and at most (50) characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name Is Required")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "The {0} must be at least (2) and at most (50) characters long.", MinimumLength = 2)]
        public string lastName { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        [DataType(DataType.EmailAddress)]
        [StringLength(200, ErrorMessage = "The {0} must be at least (3) and at most (200) characters long.", MinimumLength = 3)]
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime? LastSighInDate { get; set; }
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "The {0} must be at least (6) and at most (100) characters long.", MinimumLength = 6)]
        public string ConnectionId { get; set; }
        /// <summary>
        /// User valid token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 1 Means user is signin 0 means sign out
        /// </summary>
        public int IsLive { get; set; }
        /// <summary>
        /// 1 Means active 0 means inactive
        /// </summary>
        public int IsActive { get; set; }

        
    }
}

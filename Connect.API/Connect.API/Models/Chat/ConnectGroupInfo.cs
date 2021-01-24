using Connect.Interface.Chat;
using Connect.Interface.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Connect.API.Models.Chat
{
    public class ConnectGroupInfo : IConnectGroupInfo
    {
        public ConnectGroupInfo()
        {
            CreateDate = DateTime.UtcNow;
            IsActive = 1;
            GroupId = Guid.NewGuid().ToString();
            Name = ConnectConstants.NOT_AVAILABLE;
            Description = ConnectConstants.NOT_AVAILABLE;
        }
        public long Id { get; set; }
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "The {0} must be at least (6) and at most (50) characters long.", MinimumLength = 6)]
        public string GroupId { get; set; }
        /// <summary>
        /// You can put group name
        /// </summary>
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "The {0} must be at least (0) and at most (100) characters long.", MinimumLength = 0)]
        public string Name { get; set; }
        /// <summary>
        /// You can put group description
        /// </summary>
        [DataType(DataType.Text)]
        [StringLength(200, ErrorMessage = "The {0} must be at least (0) and at most (200) characters long.", MinimumLength = 0)]
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// Member id should be valid user id
        /// </summary>
        [Required, MinLength(2, ErrorMessage = "At least two member id required."), MaxLength(100, ErrorMessage ="You can put maximum 100 members id in a group.")]
        public List<string> MemberIds { get; set; }
        public int IsActive { get; set; }
    }
}

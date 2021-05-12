using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Homework2021.EFORM
{
    public partial class User
    {
        public User()
        {
            UserRefGroup = new HashSet<UserRefGroup>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool Enable { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public virtual ICollection<UserRefGroup> UserRefGroup { get; set; }
    }
}

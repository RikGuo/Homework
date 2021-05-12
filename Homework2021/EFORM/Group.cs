using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Homework2021.EFORM
{
    public partial class Group
    {
        public Group()
        {
            UserRefGroup = new HashSet<UserRefGroup>();
        }

        public int Id { get; set; }
        public string GroupName { get; set; }
        public string Desc { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }

        public virtual ICollection<UserRefGroup> UserRefGroup { get; set; }
    }
}

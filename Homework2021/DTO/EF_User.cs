using Homework2021.DTO;
using Homework2021.EFORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Homework2021.Model
{
    public class EF_User
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool Enable { get; set; }        
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        
    }
}

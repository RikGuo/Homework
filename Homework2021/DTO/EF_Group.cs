using Homework2021.EFORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework2021.Model
{
    public class EF_Group
    {
        public int Id { get; set; }        
        public string GroupName { get; set; }
        public string Desc { get; set; }             
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        
    }
}

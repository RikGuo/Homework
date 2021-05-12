using Homework2021.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework2021.Model
{
    public class EF_UserrefGroup
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int Group_Id { get; set; }           
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
      
        
    }
}

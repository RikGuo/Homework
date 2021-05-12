using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework2021.DTO
{
    public class RS_Object
    {
        public string TableName { get; set; }
        public int ModifyCount { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public int Count { get; set; }
        public string Key { get; set; }
    }
}

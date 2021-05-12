using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Homework2021.DTO
{
    public class RS_Customer
    {
        public int Count { get; set; }
        public int PageSize { get; set; }
        public IPagedList<EF_Customer> CustomerPagedlsit { get; set; }
    }
}

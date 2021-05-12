using Homework2021.EFORM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Homework2021.Model
{
    public class EF_Login
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string TokenData { get; set; }
        public string Message { get; set; }            
        public bool Enable { get; set; }
    }
}

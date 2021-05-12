using Homework2021.DTO;
using Homework2021.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework2021.Logic.Interface
{
    public interface IUserService
    {
        Task<EF_User> UserIdentityVerification(int id, bool enable);
        Task<RS_User> GetUserinCustomer(string search);
        Task<RS_User> GetUser(int? page, string UserName);
        Task<RS_Object> CreateUser(EF_User DataEntry);
        Task<RS_Object> UpdateUser(EF_User DataEntry);
        Task<RS_Object> DeleteUser(int userid, int groupid);        
    }
}

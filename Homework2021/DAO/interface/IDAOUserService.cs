using Homework2021.DTO;
using Homework2021.EFORM;
using Homework2021.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework2021.DAO.Interface
{
    public interface IDAOUserService
    {
        Task<EF_User> GetLogin(int id, bool enable);
        public IQueryable<EF_User> GetCustomerInfo(string search);        
        Task<bool> CheckUser(string userId);
        Task<bool> CheckUserinGroup(int userid, int groupid);
        IQueryable<EF_User> GetUsers();
        Task<RS_ModifyResult> CreateUser(EF_User DataEntry);
        Task<RS_ModifyResult> UpdateUser(EF_User DataEntry);
        Task<RS_ModifyResult> DeleteUser(int Id);

    }
}

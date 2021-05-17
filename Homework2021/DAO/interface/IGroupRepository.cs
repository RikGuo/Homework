using Homework2021.DTO;
using Homework2021.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework2021.DAO.Interface
{
    public interface IGroupRepository
    {
        Task<bool> CheckGroup(string groupname);
        Task<bool> CheckGrouphaveUser(int userid, int groupid);
        IQueryable<EF_Group> GetGroups();
        Task<RS_ModifyResult> CreateGroup(EF_Group DataEntry);
        Task<RS_ModifyResult> UpdateGroup(EF_Group DataEntry);
        Task<RS_ModifyResult> DeleteGroup(int Id);
    }
}

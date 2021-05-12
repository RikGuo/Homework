using Homework2021.DTO;
using Homework2021.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework2021.Logic.Interface
{
    public interface IGroupService
    {
        Task<RS_Group> GetGroup(int? page, string GroupName);
        Task<RS_Object> CreateGroup(EF_Group DataEntry);
        Task<RS_Object> UpdateGroup(EF_Group DataEntry);
        Task<RS_Object> DeleteGroup(int userid, int groupid);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homework2021.Content;
using Homework2021.DAO;
using Homework2021.DAO.Interface;
using Homework2021.DTO;
using Homework2021.Logic.Interface;
using Homework2021.Model;
using X.PagedList;

namespace Homework2021.Logic
{
    public class MD_Group:IGroupService
    {
        private readonly IDAOGroupService Daogroup;
        private const int pageSize = 10;

        public MD_Group(IDAOGroupService daogroup)
        {
            this.Daogroup = daogroup;
        }
       
        public async Task<RS_Group> GetGroup(int? page,string?GroupName)
        {
            RS_Group result = new RS_Group();
            try
            {
                var pageIndex = page ?? 1;
                var groups = this.Daogroup.GetGroups();
                if (!string.IsNullOrEmpty(GroupName))
                    groups = groups.Where(m => m.GroupName.Contains(GroupName));

                result.Count = groups.Count();
                result.PageSize = pageSize;
                result.GroupPagedlsit = await groups.ToPagedListAsync(pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                Nlogger.WriteLog(Nlogger.NType.Error, $"{ex.Message}{ex.InnerException}", ex);
            }
            return result;
        }
        public async Task<RS_Object> CreateGroup(EF_Group DataEntry)
        {
            RS_Object result = new RS_Object();
            try
            {
                var Rs_Modify = await this.CheckGroupName(DataEntry.GroupName);
                if (Rs_Modify.Success)
                {
                    Rs_Modify = await this.Daogroup.CreateGroup(DataEntry);
                    Rs_Modify.Message = Rs_Modify.Success ? $"成功新增群組資料{Rs_Modify.Count}筆" : Rs_Modify.Message;
                    
                }
                result = Rs_Modify.Transfor("群組");
                Nlogger.WriteLog(Nlogger.NType.Info, Rs_Modify.Message);
            }
            catch (Exception ex)
            {
                Nlogger.WriteLog(Nlogger.NType.Error, $"{ex.Message}{ex.InnerException}", ex);
            }
            return result;
        }

        public async Task<RS_Object> UpdateGroup(EF_Group DataEntry)
        {
            RS_Object result = new RS_Object();
            try
            {
                var Rs_Modify = await this.Daogroup.UpdateGroup(DataEntry);
                Rs_Modify.Message = Rs_Modify.Success ? $"成功更新群組資料{Rs_Modify.Count}筆" : Rs_Modify.Message;
                result = Rs_Modify.Transfor("群組");
                return result;
            }
            catch (Exception ex)
            {
                Nlogger.WriteLog(Nlogger.NType.Error, $"{ex.Message}{ex.InnerException}", ex);
            }
            return result;
        }

        public async Task<RS_Object> DeleteGroup(int userid,int groupid)
        {
            RS_Object result = new RS_Object();
            try
            {
                var Rs_Modify = await this.CheckGrouphaveUser(userid,groupid);
                if (Rs_Modify.Success)
                {
                    Rs_Modify = await this.Daogroup.DeleteGroup(groupid);
                    Rs_Modify.Message = Rs_Modify.Success ? $"成功刪除群組資料{Rs_Modify.Count}筆" : Rs_Modify.Message;
                    
                }
                result = Rs_Modify.Transfor("群組");
                Nlogger.WriteLog(Nlogger.NType.Info, Rs_Modify.Message);
            }
            catch (Exception ex)
            {
                Nlogger.WriteLog(Nlogger.NType.Error, $"{ex.Message}{ex.InnerException}", ex);
            }
            return result;
        }

        
        private async Task<RS_ModifyResult> CheckGroupName(string User)
        {
            bool checkGroupName = await this.Daogroup.CheckGroup(User);
            if (checkGroupName)
                return new RS_ModifyResult("Check")
                {
                    Count = 0,
                    Message = "帳號已存在",
                    Success = false
                };
            else
                return new RS_ModifyResult("Check")
                {
                    Success = true
                };
        }
        private async Task<RS_ModifyResult> CheckGrouphaveUser(int userid,int groupid)
        {
            bool checkUserName = await this.Daogroup.CheckGrouphaveUser(userid,groupid);
            if (checkUserName)
                return new RS_ModifyResult("Check")
                {
                    Count = 0,
                    Message = "群組已存在使用者",
                    Success = false
                };
            else
                return new RS_ModifyResult("Check")
                {
                    Success = true
                };
        }
    }
}

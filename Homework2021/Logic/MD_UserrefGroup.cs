using Homework2021.Content;
using Homework2021.DTO;
using Homework2021.EFORM;
using Homework2021.Logic.Interface;
using Homework2021.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework2021.Logic
{
    public class MD_UserrefGroup
    {
        private readonly UserWorkContext dbcontext;
        public MD_UserrefGroup(UserWorkContext _dbcontext)
        {
            this.dbcontext = _dbcontext;
        }
        protected UserRefGroup Transfor(EF_UserrefGroup DataEntry)
        {
            return new UserRefGroup()
            {
                Id = DataEntry.Id,
                UserId = DataEntry.User_Id,
                GroupId = DataEntry.Group_Id,
                CreateDate = DataEntry.CreateDate,
                ModifyDate = DataEntry.ModifyDate
            };
        }
        public async Task<EF_UserrefGroup> UserGroupVerification(int id)
        {
            return await this.GetLogin(id);
        }
        public async Task<EF_UserrefGroup> GetLogin(int id)
        {
            var result = await this.dbcontext.UserRefGroup.Where(b => b.Id == id )
               .FirstOrDefaultAsync();
            if (result != null)
            {
                return new EF_UserrefGroup()
                {
                    Id = result.Id,
                    User_Id=result.UserId,
                    Group_Id=result.GroupId,
                    CreateDate=result.CreateDate,
                    ModifyDate=result.ModifyDate
                };
            }
            else
                return null;
        }
        public async Task<bool> CheckUser(int id)
        {
            return await this.dbcontext.UserRefGroup.Where(m => m.Id == id).AnyAsync();
        }
        public IQueryable<EF_User> GetUserid(int userid)
        {
            var ef_linq = this.dbcontext.User.Where(b => b.Id == userid).Select(b => new EF_User
            {
                Id = b.Id,
                UserName = b.UserName,
                UserId = b.UserId,
                Password = b.Password,
                CreateDate = b.CreateDate,
                ModifyDate = b.ModifyDate,
                Enable = b.Enable
            });
            return ef_linq;
        }
        public IQueryable<EF_Group> GetGroupid(int groupid)
        {
            var ef_linq = this.dbcontext.Group.Where(b => b.Id == groupid).Select(b => new EF_Group
            {
                Id = b.Id,
                GroupName = b.GroupName,
                Desc = b.Desc,
                CreateDate = b.CreateDate,
                ModifyDate = b.ModifyDate
            });
            return ef_linq;
        }

        public async Task<RS_Object> AddUserinGroup(EF_UserrefGroup userrefgroup)
        {
            RS_Object result = new RS_Object();
            try
            {
                var checkuserid = this.GetUserid(userrefgroup.User_Id);
                var checkgroupid = this.GetGroupid(userrefgroup.Group_Id);
                if (checkuserid.Count() > 0)
                {
                    if (checkgroupid.Count() > 0)
                    {

                        result.Message = $"{userrefgroup.Group_Id}群組{userrefgroup.User_Id}使用者成功加入！";

                    }
                    await this.dbcontext.AddAsync(Transfor(userrefgroup));
                    await this.dbcontext.SaveChangesAsync();


                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;


            }
            return result;
        }
        public async Task<RS_Object> AddGroupwithUser(EF_UserrefGroup userrefgroup)
        {
            RS_Object result = new RS_Object();
            try
            {
                var checkuserid = this.GetUserid(userrefgroup.User_Id);
                var checkgroupid = this.GetGroupid(userrefgroup.Group_Id);
                if (checkgroupid.Count() > 0)
                {
                    if (checkuserid.Count() > 0)
                    {
                        result.Message = $"{userrefgroup.User_Id}使用者加入{userrefgroup.Group_Id}群組成功";
                    }
                    await this.dbcontext.AddAsync(Transfor(userrefgroup));
                    await this.dbcontext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;

            }
            return result;
        }

        public async Task<RS_ModifyResult> DeleteGroupinUser(int id)
        {
            RS_ModifyResult result = new RS_ModifyResult("Delete");
            try
            {
                var ef_find = await this.dbcontext.UserRefGroup.SingleAsync(b => b.Id == id);
                this.dbcontext.UserRefGroup.Remove(ef_find);
                var SaveResult = await this.dbcontext.SaveChangesAsync();
                result.Count = SaveResult;
                result.Success = true;

            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();
                result.Success = false;
                Nlogger.WriteLog(Nlogger.NType.Error, ex.Message, ex);
            }
            return result;
        }
        public async Task<int> Useringroup(int userid)
        {
            return await this.dbcontext.UserRefGroup.Where(m => m.UserId == userid).CountAsync();
        }
        public async Task<int> GrouphaveUser(int groupid)
        {
            return await this.dbcontext.UserRefGroup.Where(m => m.GroupId == groupid).CountAsync();
        }
        public async Task<RS_Object> DeleteUserGroup(int id)
        {
            RS_Object result = new RS_Object();
            try
            {
                var Rs_Modify = await this.CheckUserinGroup(id);
                if (Rs_Modify.Success)
                {
                    Rs_Modify = await this.DeleteGroupinUser(id);
                    Rs_Modify.Message = Rs_Modify.Success ? $"成功刪除使用者資料{Rs_Modify.Count}筆" : Rs_Modify.Message;
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
        private async Task<RS_ModifyResult> CheckUserinGroup(int id)
        {
            bool checkUserName = await this.CheckUser(id);
            if (checkUserName)
                return new RS_ModifyResult("Check")
                {
                    Success = true
                };
            else
                return new RS_ModifyResult("Check")
                {
                    Count = 0,
                    Message = "無使用者存在於群組",
                    Success = false
                };
        }
    }
}

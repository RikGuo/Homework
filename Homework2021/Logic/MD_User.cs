using Homework2021.Content;
using Homework2021.DAO;
using Homework2021.DAO.Interface;
using Homework2021.DTO;
using Homework2021.Logic.Interface;
using Homework2021.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Homework2021.Logic
{
    public class MD_User:IUserService
    {
        private readonly IDAOUserService Daouser;
        private const int pageSize = 10;
        public MD_User(IDAOUserService daouser)
        {
            this.Daouser = daouser;
        }
        public async Task<EF_User> UserIdentityVerification(int id,bool enable)
        {
            return await this.Daouser.GetLogin(id,enable);
        }
        public async Task<RS_User> GetUserinCustomer(string search)
        {
            RS_User result = new RS_User();
            try
            {
                var ef_linq = this.Daouser.GetCustomerInfo(search);
                result.Count = ef_linq.Count();
                result.PageSize = pageSize;
                result.UserPagedlsit = await ef_linq.OrderByDescending(b => b.Id).ToPagedListAsync(1, pageSize);
            }
            catch (Exception ex)
            {
                Nlogger.WriteLog(Nlogger.NType.Error, $"{ex.Message}{ex.InnerException}", ex);
            }
            return result;
        }
        public async Task<RS_User> GetUser(int?page,string?UserName)
        {
            RS_User result = new RS_User();
            try
            {
                var pageIndex = page ?? 1;
                var users = this.Daouser.GetUsers();
               
                if (!string.IsNullOrEmpty(UserName))
                    users = users.Where(m => m.UserName.Contains(UserName));                              
                result.Count = users.Count();
                result.PageSize = pageSize;                
                result.UserPagedlsit = await users.ToPagedListAsync(pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                Nlogger.WriteLog(Nlogger.NType.Error, $"{ex.Message}{ex.InnerException}", ex);
            }
            return result;
        }       
        public async Task<RS_Object> CreateUser(EF_User DataEntry)
        {
            RS_Object result = new RS_Object();
            try
            {
                var Rs_Modify = await this.CheckUserId(DataEntry.UserId);
                if (Rs_Modify.Success)
                {
                    Rs_Modify = await this.Daouser.CreateUser(DataEntry);
                    Rs_Modify.Message = Rs_Modify.Success ? $"成功新增使用者資料{Rs_Modify.Count}筆" : Rs_Modify.Message;

                }
                result = Rs_Modify.Transfor("使用者");                
            }
            catch (Exception ex)
            {
                Nlogger.WriteLog(Nlogger.NType.Error, $"{ex.Message}{ex.InnerException}", ex);
            }
            return result;
        }

        public async Task<RS_Object> UpdateUser(EF_User DataEntry)
        {
            RS_Object result = new RS_Object();
            try
            {
                var Rs_Modify = await this.Daouser.UpdateUser(DataEntry);
                Rs_Modify.Message = Rs_Modify.Success ? $"成功更新使用者資料{Rs_Modify.Count}筆" : Rs_Modify.Message;
                result = Rs_Modify.Transfor("使用者");
                return result;
            }
            catch (Exception ex)
            {
                Nlogger.WriteLog(Nlogger.NType.Error, $"{ex.Message}{ex.InnerException}", ex);
            }
            return result;
        }

        public async Task<RS_Object> DeleteUser(int userid, int groupid)
        {
            RS_Object result = new RS_Object();
            try
            {
                var Rs_Modify = await this.CheckUserinGroup(userid, groupid);
                if (Rs_Modify.Success)
                {
                    Rs_Modify = await this.Daouser.DeleteUser(userid);
                    Rs_Modify.Message = Rs_Modify.Success ? $"成功刪除使用者資料{Rs_Modify.Count}筆" : Rs_Modify.Message;

                }
                result = Rs_Modify.Transfor("使用者");
                Nlogger.WriteLog(Nlogger.NType.Info, Rs_Modify.Message);
            }
            catch (Exception ex)
            {
                Nlogger.WriteLog(Nlogger.NType.Error, $"{ex.Message}{ex.InnerException}", ex);
            }
            return result;
        }

       
        private async Task<RS_ModifyResult> CheckUserId(string User)
        {
            bool checkUserName = await this.Daouser.CheckUser(User);
            if (checkUserName)
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

        private async Task<RS_ModifyResult> CheckUserinGroup(int userid, int groupid)
        {
            bool checkUserName = await this.Daouser.CheckUserinGroup(userid, groupid);
            if (checkUserName)
                return new RS_ModifyResult("Check")
                {
                    Count = 0,
                    Message = "使用者已存在於群組",
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

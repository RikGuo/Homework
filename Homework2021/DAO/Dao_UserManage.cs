using Homework2021.Content;
using Homework2021.DAO.Interface;
using Homework2021.DTO;
using Homework2021.EFORM;
using Homework2021.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework2021.DAO
{
    public class Dao_UserManage: IDAOUserService
    {
        private readonly UserWorkContext dbcontext;
        
        public Dao_UserManage(UserWorkContext _dbcontext)
        {
            this.dbcontext = _dbcontext;           
        }
        protected User Transfor(EF_User DataEntry)
        {

            return new User()
            {
                Id = DataEntry.Id,
                UserName = DataEntry.UserName,
                UserId = DataEntry.UserId,
                Password = DataEntry.Password,
                Enable = DataEntry.Enable,
                CreateDate = DataEntry.CreateDate,
                ModifyDate = DataEntry.ModifyDate
            };
        }
        public async Task<EF_User> GetLogin(int id,bool enable)
        {
            var result = await this.dbcontext.User.Where(b => b.Id==id&&b.Enable==enable)
               .FirstOrDefaultAsync();
            if (result != null)
            {                
                return new EF_User()
                {
                    Id = result.Id,
                    UserId=result.UserId,
                    UserName = result.UserName,
                    Password=result.Password,
                    Enable=result.Enable
                };               
            }
            else
                return null;
        }
        public IQueryable<EF_User> GetCustomerInfo(string search)
        {
            var data = from a in dbcontext.User
                       join b in dbcontext.Customer on a.Id equals b.UserId into ac
                       from t in ac.DefaultIfEmpty()
                       where a.Id.ToString().Contains(search) || string.IsNullOrEmpty(search)
                       select new EF_User
                       {
                           Id = t.UserId,
                           UserName = a.UserName,
                           UserId = a.UserId,
                           Password = a.Password,
                           Enable = a.Enable,
                           CreateDate = a.CreateDate,
                           ModifyDate = a.ModifyDate
                       };
            return data;
        }
        public string GetUserCount(int id)
        {
            return this.dbcontext.User.OrderByDescending(b => b.Id==id).Select(b => b.UserName).First();
        }
        public async Task<bool> CheckUser(string userId)
        {
            return await this.dbcontext.User.Where(m => m.UserId == userId).AnyAsync();
        }
        public async Task<bool> CheckUserinGroup(int userid,int groupid)
        {
            return await this.dbcontext.UserRefGroup.Where(m => m.UserId == userid&&m.GroupId==groupid).AnyAsync();
        }        
        public IQueryable<EF_User> GetUsers()
        {
            var linq_ef = this.dbcontext.User.Select(c => new EF_User
            {
                Id = c.Id,
                UserName = c.UserName,
                UserId=c.UserId,
                Password=c.Password,
                Enable=c.Enable,
                CreateDate=c.CreateDate,
                ModifyDate=c.ModifyDate
            });

            return linq_ef;
        }
        
        public async Task<RS_ModifyResult> CreateUser(EF_User DataEntry)
        {
            RS_ModifyResult result = new RS_ModifyResult("Add");
            try
            {
                this.dbcontext.User.Add(this.Transfor(DataEntry));
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
        public async Task<RS_ModifyResult> UpdateUser(EF_User DataEntry)
        {
            RS_ModifyResult result = new RS_ModifyResult("Update");
            try
            {
                var clone = this.Transfor(DataEntry);
                var ef_find = await this.dbcontext.User.FirstAsync(b => b.Id == DataEntry.Id);
                this.dbcontext.Entry(ef_find).CurrentValues.SetValues(clone);
                this.dbcontext.User.Update(ef_find);                
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
        public async Task<RS_ModifyResult> DeleteUser(int Id)
        {
            RS_ModifyResult result = new RS_ModifyResult("Delete");
            try
            {
                var ef_find = await this.dbcontext.User.SingleAsync(b => b.Id == Id);
                this.dbcontext.User.Remove(ef_find);
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
    }
}

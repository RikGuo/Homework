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
    public class Dao_GroupManage:IGroupRepository
    {
        private readonly UserWorkContext dbcontext;
        public Dao_GroupManage(UserWorkContext _dbcontext)
        {
            this.dbcontext = _dbcontext;
        }
        protected Group Transfor(EF_Group DataEntry)
        {
            return new Group()
            {
                Id = DataEntry.Id,
                GroupName = DataEntry.GroupName,
                Desc = DataEntry.Desc,
                CreateDate = DataEntry.CreateDate,
                ModifyDate = DataEntry.ModifyDate
            };
        }
        public string GetGroupCount(int id)
        {
            return this.dbcontext.Group.OrderByDescending(b => b.Id == id).Select(b => b.GroupName).First();
        }
        public async Task<bool> CheckGroup(string groupname)
        {
            return await this.dbcontext.Group.Where(m => m.GroupName == groupname).AnyAsync();
        }
        public async Task<bool> CheckGrouphaveUser(int userid,int groupid)
        {
            return await this.dbcontext.UserRefGroup.Where(m => m.UserId == userid&&m.GroupId==groupid).AnyAsync();
        }
        public IQueryable<EF_Group> GetGroups()
        {
            var linq_ef = this.dbcontext.Group.Select(c => new EF_Group
            {
                Id = c.Id,
                GroupName=c.GroupName,
                Desc=c.Desc,
                CreateDate = c.CreateDate,
                ModifyDate = c.ModifyDate
            });

            return linq_ef;
        }
        public async Task<RS_ModifyResult> CreateGroup(EF_Group DataEntry)
        {
            RS_ModifyResult result = new RS_ModifyResult("Add");
            try
            {
                this.dbcontext.Group.Add(this.Transfor(DataEntry));
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
        public async Task<RS_ModifyResult> UpdateGroup(EF_Group DataEntry)
        {
            RS_ModifyResult result = new RS_ModifyResult("Update");
            try
            {
                var clone = this.Transfor(DataEntry);
                var ef_find = await this.dbcontext.Group.FirstAsync(b => b.Id == DataEntry.Id);
                this.dbcontext.Entry(ef_find).CurrentValues.SetValues(clone);
                this.dbcontext.Group.Update(ef_find);
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
        public async Task<RS_ModifyResult> DeleteGroup(int Id)
        {
            RS_ModifyResult result = new RS_ModifyResult("Delete");
            try
            {
                var ef_find = await this.dbcontext.Group.SingleAsync(b => b.Id == Id);
                this.dbcontext.Group.Remove(ef_find);
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

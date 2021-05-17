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
    public class Dao_Customer:ICustomerRepository
    {
        private readonly UserWorkContext dbcontext;
        public Dao_Customer(UserWorkContext _dbcontext)
        {
            this.dbcontext = _dbcontext;
        }
        protected Customer Transfor(EF_Customer DataEntry)
        {
            return new Customer()
            {
                Id = DataEntry.Id,
                Name = DataEntry.Name,
                Email = DataEntry.Email,
                Address = DataEntry.Address,
                Company = DataEntry.Company,
                Phone = DataEntry.Phone,
                UserId = DataEntry.UserId,
                CreateDate = DataEntry.CreateDate,
                ModifyDate = DataEntry.ModifyDate
            };
        }
        public string GetCustomerCount(int id)
        {
            return this.dbcontext.Customer.OrderByDescending(b => b.Id == id).Select(b => b.Name).First();
        }
        public async Task<bool> CheckCustomer(string customerName)
        {
            return await this.dbcontext.Customer.Where(m => m.Name == customerName).AnyAsync();
        }
        public async Task<bool> CheckCustomerinUser(int Id)
        {
            return await this.dbcontext.Customer.Where(m => m.Id == Id).AnyAsync();
        }
        public IQueryable<EF_Customer> GetCustomers()
        {
            var linq_ef = this.dbcontext.Customer.Select(c => new EF_Customer
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Address = c.Address,
                Company = c.Company,
                Phone = c.Phone,
                UserId = c.UserId,
                CreateDate = c.CreateDate,
                ModifyDate = c.ModifyDate
            });

            return linq_ef;
        }
       
        public async Task<RS_ModifyResult> CreateCustomer(EF_Customer DataEntry)
        {
            RS_ModifyResult result = new RS_ModifyResult("Add");
            try
            {
                this.dbcontext.Customer.Add(this.Transfor(DataEntry));
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
        public async Task<RS_ModifyResult> UpdateCustomer(EF_Customer DataEntry)
        {
            RS_ModifyResult result = new RS_ModifyResult("Update");
            try
            {
                var clone = this.Transfor(DataEntry);
                var ef_find = await this.dbcontext.Customer.FirstAsync(b => b.Id == DataEntry.Id);
                this.dbcontext.Entry(ef_find).CurrentValues.SetValues(clone);
                this.dbcontext.Customer.Update(ef_find);
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
        public async Task<RS_ModifyResult> DeleteCustomer(int Id)
        {
            RS_ModifyResult result = new RS_ModifyResult("Delete");
            try
            {
                var ef_find = await this.dbcontext.Customer.SingleAsync(b => b.Id == Id);
                this.dbcontext.Customer.Remove(ef_find);
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

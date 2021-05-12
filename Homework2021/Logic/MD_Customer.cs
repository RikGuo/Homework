using Homework2021.Content;
using Homework2021.DAO;
using Homework2021.DAO.Interface;
using Homework2021.DTO;
using Homework2021.Logic.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Homework2021.Logic
{
    public class MD_Customer:ICustomerService
    {
        private readonly IDAOCustomerService Daocustomer;
        private const int pageSize = 10;
        public MD_Customer(IDAOCustomerService daocustomer)
        {
            this.Daocustomer = daocustomer;
        }          
        public async Task<RS_Customer> GetCustomer(int?page,string?search)
        {
            RS_Customer result = new RS_Customer();
            try
            {
                var pageIndex = page ?? 1;               
                var customers = this.Daocustomer.GetCustomers();
                if (!string.IsNullOrEmpty(search))
                    customers = customers.Where(m => m.Name.Contains(search) || m.Company.Contains(search));
                result.Count = customers.Count();
                result.PageSize = pageSize;
                result.CustomerPagedlsit = await customers.ToPagedListAsync(pageIndex,pageSize);
            }
            catch (Exception ex)
            {
                Nlogger.WriteLog(Nlogger.NType.Error, $"{ex.Message}{ex.InnerException}", ex);
            }
            return result;
        }
        public async Task<RS_Customer> GetCustomerList(int? page)
        {
            RS_Customer result = new RS_Customer();
            try
            {
                var pageIndex = page ?? 1;
                var customers = this.Daocustomer.GetCustomers();               
                result.Count = customers.Count();
                result.PageSize = pageSize;
                result.CustomerPagedlsit = await customers.ToPagedListAsync(pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                Nlogger.WriteLog(Nlogger.NType.Error, $"{ex.Message}{ex.InnerException}", ex);
            }
            return result;
        }

        public async Task<RS_Object> CreateCustomer(EF_Customer DataEntry)
        {
            RS_Object result = new RS_Object();
            try
            {
                var Rs_Modify = await this.CheckCustomerId(DataEntry.Name.ToString());
                if (Rs_Modify.Success)
                {
                    Rs_Modify = await this.Daocustomer.CreateCustomer(DataEntry);
                    Rs_Modify.Message = Rs_Modify.Success ? $"成功新增客戶資料{Rs_Modify.Count}筆" : Rs_Modify.Message;

                }
                result = Rs_Modify.Transfor("客戶");
            }
            catch (Exception ex)
            {
                Nlogger.WriteLog(Nlogger.NType.Error, $"{ex.Message}{ex.InnerException}", ex);
            }
            return result;
        }

        public async Task<RS_Object> UpdateCustomer(EF_Customer DataEntry)
        {
            RS_Object result = new RS_Object();
            try
            {
                var Rs_Modify = await this.Daocustomer.UpdateCustomer(DataEntry);
                Rs_Modify.Message = Rs_Modify.Success ? $"成功更新客戶資料{Rs_Modify.Count}筆" : Rs_Modify.Message;
                result = Rs_Modify.Transfor("客戶");
                return result;
            }
            catch (Exception ex)
            {
                Nlogger.WriteLog(Nlogger.NType.Error, $"{ex.Message}{ex.InnerException}", ex);
            }
            return result;
        }

        public async Task<RS_Object> DeleteCustomer(int Id)
        {
            RS_Object result = new RS_Object();
            try
            {
                var Rs_Modify = await this.CheckCustomerinUser(Id);
                if (Rs_Modify.Success)
                {
                    Rs_Modify = await this.Daocustomer.DeleteCustomer(Id);
                    Rs_Modify.Message = Rs_Modify.Success ? $"成功刪除客戶資料{Rs_Modify.Count}筆" : Rs_Modify.Message;

                }
                result = Rs_Modify.Transfor("客戶");
                Nlogger.WriteLog(Nlogger.NType.Info, Rs_Modify.Message);
            }
            catch (Exception ex)
            {
                Nlogger.WriteLog(Nlogger.NType.Error, $"{ex.Message}{ex.InnerException}", ex);
            }
            return result;
        }


        private async Task<RS_ModifyResult> CheckCustomerId(string Customer)
        {
            bool checkCustomerName = await this.Daocustomer.CheckCustomer(Customer);
            if (checkCustomerName)
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

        private async Task<RS_ModifyResult> CheckCustomerinUser(int Id)
        {
            bool checkUserName = await this.Daocustomer.CheckCustomerinUser(Id);
            if (checkUserName)
                return new RS_ModifyResult("Check")
                {
                    Message="成功刪除客戶資料",
                    Success=true
                };
            else
                return new RS_ModifyResult("Check")
                {
                    Count = 0,
                    Message = "使用者是客戶",
                    Success = false
                };
        }
    }
}

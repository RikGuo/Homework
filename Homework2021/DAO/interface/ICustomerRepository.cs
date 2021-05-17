using Homework2021.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework2021.DAO.Interface
{
    public interface ICustomerRepository
    {
        string GetCustomerCount(int id);
        Task<bool> CheckCustomer(string customerName);
        Task<bool> CheckCustomerinUser(int Id);
        IQueryable<EF_Customer> GetCustomers();
        Task<RS_ModifyResult> CreateCustomer(EF_Customer DataEntry);
        Task<RS_ModifyResult> UpdateCustomer(EF_Customer DataEntry);
        Task<RS_ModifyResult> DeleteCustomer(int Id);

    }
}

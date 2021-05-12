using Homework2021.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework2021.Logic.Interface
{
    public interface ICustomerService
    {
        Task<RS_Customer> GetCustomer(int? page, string? search);
        Task<RS_Customer> GetCustomerList(int? page);
        Task<RS_Object> CreateCustomer(EF_Customer DataEntry);
        Task<RS_Object> UpdateCustomer(EF_Customer DataEntry);
        Task<RS_Object> DeleteCustomer(int Id);
    }
}

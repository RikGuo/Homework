using System.Threading.Tasks;
using Homework2021.DTO;
using Homework2021.Logic.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Homework2021.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customer;
        public CustomerController(ICustomerService _Customer)
        {
            this.customer = _Customer;
        }        

        [HttpGet]
        public async Task<IActionResult> GetCustomer(int? page,string?search)
        {
            var r = await this.customer.GetCustomer(page,search);
            return new JsonResult(r);
        }
      
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(EF_Customer DataEntry)
        {
            var r = await this.customer.CreateCustomer(DataEntry);
            return new JsonResult(r);

        }
        [HttpPut]
        public async Task<RS_Object> UpdateCustoemr(EF_Customer DataEntry)
        {
            return await this.customer.UpdateCustomer(DataEntry);

        }
        [HttpDelete]
        public async Task<RS_Object> DeleteCustomer(int Id)
        {
            return await this.customer.DeleteCustomer(Id);

        }
    }
}
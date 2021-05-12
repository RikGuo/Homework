using System.Threading.Tasks;
using Homework2021.DTO;
using Homework2021.Logic.Interface;
using Homework2021.Model;
using Microsoft.AspNetCore.Mvc;

namespace Homework2021.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService user;
        public UserController(IUserService _User)
        {
            this.user = _User;
        }
        [HttpGet]
        public async Task<RS_User> GetUserinCustomer(string search)
        {
            return await this.user.GetUserinCustomer(search);
        }
        [HttpGet]
        public async Task<IActionResult> GetUser(int?page,string?UserName)
        {
            var r = await this.user.GetUser(page,UserName);
            return new JsonResult(r);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUser(EF_User DataEntry)
        {
            var r=await this.user.CreateUser(DataEntry);
            return new JsonResult(r);

        }
        [HttpPut]
        public async Task<RS_Object> UpdateUser(EF_User DataEntry)
        {
            return await this.user.UpdateUser(DataEntry);
            
        }
        [HttpDelete]
        public async Task<RS_Object> DeleteUser(int userid, int groupid)
        {
            return await this.user.DeleteUser(userid, groupid);
            
        }
    }
}
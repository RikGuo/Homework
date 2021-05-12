using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homework2021.DAO;
using Homework2021.DTO;
using Homework2021.Logic;
using Homework2021.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Homework2021.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserrefGroupController : ControllerBase
    {
        
        private readonly MD_UserrefGroup userrefgroup;


        public UserrefGroupController(MD_UserrefGroup _userrefgroup)
        {
            this.userrefgroup = _userrefgroup;
        }        
        
        [HttpPost]
        public async Task<RS_Object> AddUserinGroup(EF_UserrefGroup userrefgroup)
        {
            return await this.userrefgroup.AddUserinGroup(userrefgroup);
        }
        [HttpPost]
        public async Task<RS_Object> AddGroupwithUser(EF_UserrefGroup userrefgroup)
        {
            return await this.userrefgroup.AddGroupwithUser(userrefgroup);
        }
       
        [HttpDelete]
        public async Task<RS_Object> DeleteUserGroup(int id)
        {
            return await this.userrefgroup.DeleteUserGroup(id);
        }
        [HttpGet]
        public async Task<IActionResult> Useringroup(int userid)
        {
            JsonResult r;
            var result = await this.userrefgroup.Useringroup(userid);
            r = new JsonResult(result);
            return r;
        }
        [HttpGet]
        public async Task<IActionResult> GrouphaveUser(int groupid)
        {
            JsonResult r;
            var result = await this.userrefgroup.GrouphaveUser(groupid);
            r = new JsonResult(result);
            return r;
        }
    }
}
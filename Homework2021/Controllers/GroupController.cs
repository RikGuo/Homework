using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Homework2021.Model;
using Homework2021.DTO;
using Homework2021.Logic.Interface;

namespace Homework2021.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService group;
        public GroupController(IGroupService _Group)
        {
            this.group = _Group;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetGroup(int? page,string?GroupName)
        {
            var r = await this.group.GetGroup(page,GroupName);
            return new JsonResult(r);
        }        
        [HttpPost]
        public async Task<RS_Object> CreateGroup(EF_Group DataEntry)
        {
            return await this.group.CreateGroup(DataEntry);

        }
        [HttpPut]
        public async Task<RS_Object> UpdateGroup(EF_Group DataEntry)
        {
            return await this.group.UpdateGroup(DataEntry);

        }
        [HttpDelete]
        public async Task<RS_Object> DeleteGroup(int userid, int groupid)
        {
            return await this.group.DeleteGroup(userid, groupid);

        }
    }
}
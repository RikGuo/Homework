using System.Threading.Tasks;
using Homework2021.Content;
using Homework2021.Logic;
using Homework2021.Model;
using Microsoft.AspNetCore.Mvc;

namespace Homework2021.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly MD_User UserIdentity;
        private readonly JwtHelpers jwt;


        public LoginController(MD_User _UserIdentity, JwtHelpers _jwt)
        {
            this.UserIdentity = _UserIdentity;
            this.jwt = _jwt;
        }
        [HttpPost]
        public async Task<EF_Login> Login(EF_User user)
        {

            var userIdentity = await this.UserIdentity.UserIdentityVerification(user.Id,user.Enable);
            
            EF_Login result = new EF_Login()
            {
                UserName = user.UserName

            };

            if (userIdentity.Enable == true)
            {
                result.Id = userIdentity.Id;
                result.UserId = userIdentity.UserId;          
                result.UserName = userIdentity.UserName;
                result.Password = userIdentity.Password;
                var TokenStr = this.jwt.JwtGenerateToken(user);
                result.Enable = true;
                result.Message = "登入成功";
            }
            else
            {
                result.Enable = false;
                result.Message = "帳號或密碼輸入錯誤";
            }
            return result;
        }
    }
}
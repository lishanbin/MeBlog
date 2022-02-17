using MeBlog.IService;
using MeBlog.JWT.Utility._MD5;
using MeBlog.JWT.Utility.ApiResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MeBlog.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IWriterInfoService _iWriterInfoService;

        public AuthorizeController(IWriterInfoService iWriterInfoService)
        {
            this._iWriterInfoService = iWriterInfoService;
        }

        [HttpPost("Login")]
        public async Task<ApiResult> Login(string username,string userpwd)
        {
            //数据校验
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(userpwd)) return ApiResultHelper.Error("用户名或密码不能为空");

            string pwd = MD5Helper.MD5Encrypt32(userpwd);
            var writer=await _iWriterInfoService.FindAsync(c => c.UserName == username && c.UserPwd == pwd);
            if (writer != null)
            {
                //登陆成功
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name,writer.Name),
                    new Claim("Id",writer.Id.ToString()),
                    new Claim("UserName",writer.UserName)
                };
                SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("lishanbinlishanbin"));//太短了不行
                //issuer代表颁发Token的Web应用程序，audience是Token的受理者
                var token = new JwtSecurityToken(
                    issuer:"http://localhost:6060",
                    audience:"http://localhost:5000",
                    claims:claims,
                    notBefore:DateTime.Now,
                    expires:DateTime.Now.AddHours(1),
                    signingCredentials:new SigningCredentials(key,SecurityAlgorithms.HmacSha256));

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return ApiResultHelper.Success(jwtToken);
            }
            else
            {
                return ApiResultHelper.Error("账号或密码错误");
            }
        }


    }
}

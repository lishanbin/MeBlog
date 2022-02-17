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
    public class AuthStuController : ControllerBase
    {
        private readonly IStudentService _iStudentService;

        public AuthStuController(IStudentService iStudentService)
        {
            this._iStudentService = iStudentService;
        }

        /// <summary>
        /// 学生登录
        /// </summary>
        /// <param name="idcard"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<ApiResult> Login(string idcard, string password)
        {
            if (string.IsNullOrWhiteSpace(idcard) || string.IsNullOrWhiteSpace(password)) return ApiResultHelper.Error("学生身份证号或密码不能为空");

            string pwd = MD5Helper.MD5Encrypt32(password);
            var student = await _iStudentService.FindAsync(s => s.IDCard == idcard && s.Password == pwd);
            if (student!=null)
            {
                //登录成功
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name,student.Name),
                    new Claim("Id",student.Id.ToString()),
                    new Claim("IDCard",student.IDCard),
                    new Claim("Sex",student.Sex),
                    new Claim("Role",student.Role.ToString())
                };
                SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("lishanbinlishanbin"));//太短了不行
                //issuer代表颁发Token的Web应用程序，audience是Token的受理者
                var token = new JwtSecurityToken(
                    issuer: "http://localhost:6060",
                    audience: "http://localhost:5000",
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return ApiResultHelper.Success(jwtToken);
            }
            else
            {
                return ApiResultHelper.Error("身份证号或密码错误");
            }
        }



    }
}

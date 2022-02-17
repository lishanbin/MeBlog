using MeBlog.IService;
using MeBlog.WebApi.Utility._MD5;
using MeBlog.WebApi.Utility.ApiResult;
using MeBlog.WebApi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MeBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IStudentService _iStudentService;

        public LoginController(IStudentService iStudentService)
        {
            this._iStudentService = iStudentService;
        }

        /// <summary>
        /// 学生登录
        /// </summary>
        /// <param name="idcard"></param>
        /// <param name="password"></param>
        /// <param name="captcha"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<ApiResult> Login(LoginViewModel loginViewModel)
        {
            if (string.IsNullOrWhiteSpace(loginViewModel.username) || string.IsNullOrWhiteSpace(loginViewModel.password) ||string.IsNullOrWhiteSpace(loginViewModel.code)) return ApiResultHelper.Error("学生身份证号或密码或验证码不能为空");

            if (!ValidateCaptchaCode(loginViewModel.code))
            {
                return ApiResultHelper.Error("验证码不正确！");
            }

            string pwd = MD5Helper.MD5Encrypt32(loginViewModel.password);
            var student = await _iStudentService.FindAsync(s => s.IDCard == loginViewModel.username && s.Password == pwd);
            if (student != null)
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
                    issuer: "http://localhost:5000",
                    audience: "http://localhost:5000",
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                var stuInfo =new { Id = student.Id, jwtToken = jwtToken };
                return ApiResultHelper.Success(stuInfo);
            }
            else
            {
                return ApiResultHelper.Error("身份证号或密码错误");
            }
        }

        /// <summary>
        /// 学生退出登录
        /// </summary>
        /// <returns></returns>
        [HttpGet("Logout")]
        public ApiResult Logout()
        {
            return ApiResultHelper.Success(null);
        }

        /// <summary>
        /// 验证码是否正确
        /// </summary>
        /// <param name="userInputCaptcha"></param>
        /// <returns></returns>
        private bool ValidateCaptchaCode(string userInputCaptcha)
        {
            var isValid=userInputCaptcha.Equals(HttpContext.Session.GetString("captcha"),System.StringComparison.OrdinalIgnoreCase);
            HttpContext.Session.Remove("captcha");
            return isValid;
        }


    }
}

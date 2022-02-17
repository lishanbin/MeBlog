using MeBlog.WebApi.Utility._Captcha;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MeBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        [HttpGet]
        public async Task<FileContentResult> CaptchaAsync([FromServices] ICaptcha _captcha)
        {
            var code = await _captcha.GenerateRandomCaptchaAsync();

            HttpContext.Session.SetString("captcha",code);

            var result = await _captcha.GenerateCaptchaImageAsync(code);

            return File(result.CaptchaMemoryStream.ToArray(), "image/png");
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("Authorize")]
        [Authorize]
        public string Authorize()
        {
            return "this is Authorize";
        }

        [HttpGet("NoAuthorize")]
        public string NoAuthorize()
        {
            return "this is NoAuthorize";
        }


    }
}

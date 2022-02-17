using MeBlog.IService;
using MeBlog.Model;
using MeBlog.WebApi.Utility.ApiResult;
using MeBlog.WebApi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MeBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissController : ControllerBase
    {
        private readonly IPermissService _iPermissService;
        private readonly IYebPerMenuService _iYebPerMenuService;

        public PermissController(IPermissService iPermissService,IYebPerMenuService iYebPerMenuService)
        {
            this._iPermissService = iPermissService;
            this._iYebPerMenuService = iYebPerMenuService;
        }

        /// <summary>
        /// 获取所有的角色信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("Permisses")]
        public async Task<ApiResult> GetPermisses()
        {
            var permisses=await _iPermissService.QueryAsync();
            if (permisses.Count == 0) return ApiResultHelper.Error("没有角色信息");
            return ApiResultHelper.Success(permisses);
        }

        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <param name="permissViewModel"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ApiResult> Create(PermissViewModel permissViewModel)
        {
            if (string.IsNullOrWhiteSpace(permissViewModel.Name) || string.IsNullOrWhiteSpace(permissViewModel.NameZh)) return ApiResultHelper.Error("角色信息不能为空！");

            Permiss permiss = new Permiss
            {
                Name = permissViewModel.Name,
                NameZh = permissViewModel.NameZh
            };

            bool b=await _iPermissService.CreateAsync(permiss);
            if (!b) return ApiResultHelper.Error("角色添加失败！");
            return ApiResultHelper.Success(b);
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ApiResult> Delete(string rid)
        {
            if (string.IsNullOrWhiteSpace(rid)) return ApiResultHelper.Error("角色编号不能为空");
            bool yb=await _iYebPerMenuService.DeleteAsync(r => r.PermissId == int.Parse(rid));
            if (!yb) ApiResultHelper.Error("角色权限删除失败！");

            bool b=await _iPermissService.DeleteAsync(int.Parse(rid));
            if (!b) ApiResultHelper.Error("角色删除失败！");
            return ApiResultHelper.Success(b);
        }


    }
}

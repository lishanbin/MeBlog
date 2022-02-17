using MeBlog.IService;
using MeBlog.Model;
using MeBlog.WebApi.Utility.ApiResult;
using MeBlog.WebApi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MeBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YebPerMenuController : ControllerBase
    {
        private readonly IYebPerMenuService _iYebPerMenuService;

        public YebPerMenuController(IYebPerMenuService iYebPerMenuService)
        {
            this._iYebPerMenuService = iYebPerMenuService;
        }

        /// <summary>
        /// 获取所有角色菜单权限
        /// </summary>
        /// <returns></returns>
        [HttpGet("YebPerMenus")]
        public async Task<ApiResult> GetYebPerMenus(string rid)
        {
            if (string.IsNullOrWhiteSpace(rid)) return ApiResultHelper.Error("角色编号不能为空");

            var ypmList=await _iYebPerMenuService.QueryAsync(r=>r.PermissId==int.Parse(rid));
            if (ypmList.Count == 0) return ApiResultHelper.Error("当前角色权限菜单数据不存在");
            return ApiResultHelper.Success(ypmList);
        }

        /// <summary>
        /// 添加角色菜单
        /// </summary>
        /// <param name="yebPerMenuViewModel"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ApiResult> Create(YebPerMenuViewModel yebPerMenuViewModel)
        {
            if (String.IsNullOrWhiteSpace(yebPerMenuViewModel.YebMenuIds))
                return ApiResultHelper.Error("菜单权限为空");

            var oldYebPerMenuList=await _iYebPerMenuService.QueryAsync(y=>y.PermissId== yebPerMenuViewModel.PermissId);
            if (oldYebPerMenuList.Count > 0)
            {
                foreach (var ypm in oldYebPerMenuList)
                {
                    bool oldb= await _iYebPerMenuService.DeleteAsync(ypm.Id);
                    if (!oldb) return ApiResultHelper.Error("删除失败");
                }
            }

            string[] idsStr= yebPerMenuViewModel.YebMenuIds.Split(',');
            foreach (var idStr in idsStr)
            {
                YebPerMenu yebPerMenu = new YebPerMenu
                {
                    PermissId = yebPerMenuViewModel.PermissId,
                    YebMenuId = int.Parse(idStr)
                };
                bool b = await _iYebPerMenuService.CreateAsync(yebPerMenu);
                if (!b) return ApiResultHelper.Error("添加失败");
            }

            return ApiResultHelper.Success("添加成功！");
        }

    }
}

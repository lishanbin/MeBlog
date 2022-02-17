using MeBlog.IService;
using MeBlog.Model;
using MeBlog.WebApi.Utility.ApiResult;
using MeBlog.WebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class YebMenuController : ControllerBase
    {
        private readonly IYebMenuService _iYebMenuService;

        public YebMenuController(IYebMenuService iYebMenuService)
        {
            this._iYebMenuService = iYebMenuService;
        }
        /// <summary>
        /// 获取所有菜单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("YebMenus")]
        public async Task<ApiResult> GetYebMenu()
        {
            List<YebMenuViewModel> list = new List<YebMenuViewModel>();
            var yebMenuList=await _iYebMenuService.QueryAsync(y=>y.ParentId==1);
            if (yebMenuList.Count == 0) return ApiResultHelper.Error("还没有菜单信息！");
            foreach (var yebMenu in yebMenuList)
            {
                YebMenuViewModel yebMenuViewModel = new YebMenuViewModel();
                yebMenuViewModel.Id = yebMenu.Id;
                yebMenuViewModel.Url = yebMenu.Url;
                yebMenuViewModel.Path = yebMenu.Path;
                yebMenuViewModel.Component = yebMenu.Component;
                yebMenuViewModel.Name = yebMenu.Name;
                yebMenuViewModel.IconCls = yebMenu.IconCls;
                yebMenuViewModel.KeepAlive = yebMenu.KeepAlive;
                yebMenuViewModel.RequireAuth = yebMenu.RequireAuth;
                yebMenuViewModel.ParentId=yebMenu.ParentId;
                yebMenuViewModel.Enabled=yebMenu.Enabled;

                List<YebMenuViewModel> subList = new List<YebMenuViewModel>();
                var subYebMenuList=await _iYebMenuService.QueryAsync(s => s.ParentId == yebMenu.Id);
                if(subYebMenuList.Count==0)
                {
                    yebMenuViewModel.Children = null;
                }
                else
                {
                    foreach (var subYebMenu in subYebMenuList)
                    {
                        YebMenuViewModel subYebMenuViewModel = new YebMenuViewModel();
                        subYebMenuViewModel.Id = subYebMenu.Id;
                        subYebMenuViewModel.Url = subYebMenu.Url;
                        subYebMenuViewModel.Path = subYebMenu.Path;
                        subYebMenuViewModel.Component = subYebMenu.Component;
                        subYebMenuViewModel.Name = subYebMenu.Name;
                        subYebMenuViewModel.IconCls = subYebMenu.IconCls;
                        subYebMenuViewModel.KeepAlive = subYebMenu.KeepAlive;
                        subYebMenuViewModel.RequireAuth = subYebMenu.RequireAuth;
                        subYebMenuViewModel.ParentId = subYebMenu.ParentId;
                        subYebMenuViewModel.Enabled = subYebMenu.Enabled;
                        subList.Add(subYebMenuViewModel);
                    }
                    yebMenuViewModel.Children = subList;
                }
                list.Add(yebMenuViewModel);
            }
            
            return ApiResultHelper.Success(list);
        }

        /// <summary>
        /// 添加菜单信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// <param name="component"></param>
        /// <param name="name"></param>
        /// <param name="iconCls"></param>
        /// <param name="keepAlive"></param>
        /// <param name="requireAuth"></param>
        /// <param name="parentId"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ApiResult> Create(string url,string path,string component,string name,string iconCls,bool keepAlive,bool requireAuth,int parentId,bool enabled)
        {
            YebMenu yebMenu = new YebMenu
            {
                Url = url,
                Path = path,
                Component = component,
                Name = name,
                IconCls = iconCls,
                KeepAlive = keepAlive,
                RequireAuth = requireAuth,
                ParentId = parentId,
                Enabled = enabled
            };
            bool b=await _iYebMenuService.CreateAsync(yebMenu);
            if (!b) return ApiResultHelper.Error("菜单信息添加失败！");
            return ApiResultHelper.Success(yebMenu);
        }


    }
}

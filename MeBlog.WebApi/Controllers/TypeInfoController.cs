using MeBlog.IService;
using MeBlog.Model;
using MeBlog.WebApi.Utility.ApiResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MeBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TypeInfoController : ControllerBase
    {
        private readonly ITypeInfoService _iTypeInfoService;

        public TypeInfoController(ITypeInfoService iTypeInfoService)
        {
            this._iTypeInfoService = iTypeInfoService;
        }
        /// <summary>
        /// 文章类型列表 
        /// </summary>
        /// <returns></returns>
        [HttpGet("TypeInfos")]
        public async Task<ApiResult> TypeInfos()
        {
            var types=await _iTypeInfoService.QueryAsync();
            if (types.Count == 0) return ApiResultHelper.Error("没有更多的文章类型");
            return ApiResultHelper.Success(types);
        }
        /// <summary>
        /// 文章类型添加
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ApiResult> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return ApiResultHelper.Error("类型名不能为空");
            TypeInfo typeInfo = new TypeInfo
            {
                Name = name
            };
            bool b= await _iTypeInfoService.CreateAsync(typeInfo);
            if (!b) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success(typeInfo);
        }
        /// <summary>
        /// 删除文章类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ApiResult> Delete(int id)
        {            
            bool b= await _iTypeInfoService.DeleteAsync(id);
            if (!b) return ApiResultHelper.Error("文章类型删除失败");
            return ApiResultHelper.Success("文章类型删除成功");
        }
        /// <summary>
        /// 编辑文章类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(int id, string name)
        {
            var typeInfo = await _iTypeInfoService.FindAsync(id);
            if (typeInfo == null) return ApiResultHelper.Error("文章类型不存在");
            typeInfo.Name = name;
            bool b= await _iTypeInfoService.EditAsync(typeInfo);
            if (!b) return ApiResultHelper.Error("编辑文章类型失败");
            return ApiResultHelper.Success(typeInfo);
        }

    }
}

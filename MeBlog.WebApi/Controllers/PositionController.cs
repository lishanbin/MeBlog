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
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _iPositionService;

        public PositionController(IPositionService iPositionService)
        {
            this._iPositionService = iPositionService;
        }

        /// <summary>
        /// 获取所有职位
        /// </summary>
        /// <returns></returns>
        [HttpGet("Positions")]
        public async Task<ApiResult> GetPositions()
        {
            var positionList=await _iPositionService.QueryAsync();
            if (positionList.Count == 0) return ApiResultHelper.Error("职位信息为空");
            return ApiResultHelper.Success(positionList);
        }

        /// <summary>
        /// 添加职位信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ApiResult> Create(PositionViewModel positionViewModel)
        {
            if (string.IsNullOrWhiteSpace(positionViewModel.Name)) return ApiResultHelper.Error("职位信息名称不能为空");

            Position position = new Position
            {
                Name = positionViewModel.Name,
                Enabled = positionViewModel.Enabled
            };
            bool b = await _iPositionService.CreateAsync(position);
            if (!b) return ApiResultHelper.Error("添加职位信息失败");
            return ApiResultHelper.Success(position);
        }

        /// <summary>
        /// 删除职位信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ApiResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return ApiResultHelper.Error("删除职位Id信息不能为空！");
            bool b = await _iPositionService.DeleteAsync(Convert.ToInt32(id));
            if (!b) return ApiResultHelper.Error("删除职位信息为空！");
            return ApiResultHelper.Success(b);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete("DeleteMany")]
        public async Task<ApiResult> DeleteMany(string ids)
        {
            string[] idList = ids.Split(',');
            if (idList.Length == 0) return ApiResultHelper.Error("删除信息有误！");
            foreach (var id in idList)
            {
                bool b=await _iPositionService.DeleteAsync(Convert.ToInt32(id));
                if (!b) return ApiResultHelper.Error("删除职位信息失败！");                
            }
            
            return ApiResultHelper.Success("删除成功！");
        }

        /// <summary>
        /// 修改职位信息
        /// </summary>
        /// <param name="positionViewModel"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(PositionViewModel positionViewModel)
        {
            var position=await _iPositionService.FindAsync(positionViewModel.Id);
            if (position == null) return ApiResultHelper.Error("职位信息不存在！");
            if (string.IsNullOrWhiteSpace(positionViewModel.Name)) return ApiResultHelper.Error("职位名称信息不能为空！");

            position.Enabled = positionViewModel.Enabled;
            position.Name = positionViewModel.Name;

            bool b=await _iPositionService.EditAsync(position);
            if (!b) return ApiResultHelper.Error("职位信息更新失败！");
            return ApiResultHelper.Success(b);
        }

    }
}

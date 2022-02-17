using AutoMapper;
using MeBlog.IService;
using MeBlog.Model;
using MeBlog.Model.DTO;
using MeBlog.WebApi.Utility._MD5;
using MeBlog.WebApi.Utility.ApiResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MeBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WriterInfoController : ControllerBase
    {
        private readonly IWriterInfoService _iWriterInfoService;

        public WriterInfoController(IWriterInfoService iWriterInfoService)
        {
            this._iWriterInfoService = iWriterInfoService;
        }
        /// <summary>
        /// 作者列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("WriterInfo")]
        public async Task<ApiResult> WriterInfo()
        {
            var writers=await _iWriterInfoService.QueryAsync();
            if (writers.Count == 0) return ApiResultHelper.Error("没有更多文章作者");
            return ApiResultHelper.Success(writers);
        }

        /// <summary>
        /// 添加作者
        /// </summary>
        /// <param name="name"></param>
        /// <param name="username"></param>
        /// <param name="userpwd"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ApiResult> Create(string name,string username,string userpwd)
        {
            WriterInfo writer = new WriterInfo
            {
                Name = name,
                UserName = username,
                UserPwd =MD5Helper.MD5Encrypt32(userpwd)
            };
            var oldWirter=await _iWriterInfoService.FindAsync(c => c.UserName == username);
            if (oldWirter != null) return ApiResultHelper.Error("作者用户名已经存");
            bool b=await _iWriterInfoService.CreateAsync(writer);
            if (!b) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success(writer);
        }
        /// <summary>
        /// 修改作者
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(string name)
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var writer=await _iWriterInfoService.FindAsync(id);
            if (writer == null) return ApiResultHelper.Error("用户不存在");
            writer.Name=name;
            bool b =await _iWriterInfoService.EditAsync(writer);
            if (!b) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success("修改成功");
        }

        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="iMapper"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("FindWriter")]
        public async Task<ApiResult> FindWriter([FromServices]IMapper iMapper,int id)
        {
            var writer = await _iWriterInfoService.FindAsync(id);
            if (writer == null) return ApiResultHelper.Error("用户不存在");
            var writerDto= iMapper.Map<WriterInfoDTO>(writer);

            return ApiResultHelper.Success(writerDto);
        }

    }
}

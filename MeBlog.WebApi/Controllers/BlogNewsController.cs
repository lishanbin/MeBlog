using AutoMapper;
using MeBlog.IService;
using MeBlog.Model;
using MeBlog.Model.DTO;
using MeBlog.WebApi.Utility.ApiResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogNewsController : ControllerBase
    {
        private readonly IBlogNewsService _iBlogNewsService;

        public BlogNewsController(IBlogNewsService iBlogNewsService)
        {
            this._iBlogNewsService = iBlogNewsService;
        }
        /// <summary>
        /// 获取所有文章
        /// </summary>
        /// <returns></returns>
        [HttpGet("BlogNews")]
        public async Task<ActionResult<ApiResult>> BlogNews()
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
           var data= await  _iBlogNewsService.QueryAsync(c=>c.WriterId==id);
            if (data.Count == 0) return ApiResultHelper.Error("没有更多的文章");
            return ApiResultHelper.Success(data);
        }
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="typeid"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ActionResult<ApiResult>> Create(string title,string content,int typeid)
        {
            //数据验证
            BlogNews blogNews = new BlogNews
            {
                Title = title,
                Content = content,
                BrowseCount = 0,
                LikeCount = 0,
                Time = DateTime.Now,
                TypeId = typeid,
                WriterId =Convert.ToInt32(this.User.FindFirst("Id").Value)
            };
            bool b =await _iBlogNewsService.CreateAsync(blogNews);
            if (!b) return ApiResultHelper.Error("添加文章失败");
            return ApiResultHelper.Success(blogNews);
        }
        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {
            bool b=await _iBlogNewsService.DeleteAsync(id);
            if (!b) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success(b);
        }
        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="typeid"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<ActionResult<ApiResult>> Edit(int id,string title,string content,int typeid)
        {
            var blogNews=await _iBlogNewsService.FindAsync(id);
            if (blogNews == null) return ApiResultHelper.Error("没有找到要修改的文章");
            blogNews.Title=title;
            blogNews.Content=content;
            blogNews.TypeId=typeid;

            bool b=await _iBlogNewsService.EditAsync(blogNews);
            if (!b) return ApiResultHelper.Error("文章修改失败");
            return ApiResultHelper.Success(blogNews);
        }

        [HttpGet("BlogNewsPage")]
        public async Task<ApiResult> GetBlogNewsPage([FromServices]IMapper iMapper,int page,int size)
        {
            RefAsync<int> total = 0;
            var blognews = await _iBlogNewsService.QueryAsync(page, size, total);
            try
            {
                var blognewsDTO = iMapper.Map<List<BlogNewsDTO>>(blognews);
                return ApiResultHelper.Success(blognewsDTO, total);
            }
            catch (Exception)
            {
                return ApiResultHelper.Error("AutoMapper映射错误");
            }

        }



    }
}

using AutoMapper;
using MeBlog.IService;
using MeBlog.Model;
using MeBlog.Model.DTO;
using MeBlog.WebApi.Utility.ApiResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _iCourseService;

        public CourseController(ICourseService iCourseService)
        {
            this._iCourseService = iCourseService;
        }

        /// <summary>
        /// 获取课程列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("Courses")]
        public async Task<ApiResult> Courses([FromServices]IMapper iMapper)
        {
            var courses=await _iCourseService.QueryAsync();
            if (courses.Count == 0) return ApiResultHelper.Error("课程列表信息为空");

            var courseDtoList= iMapper.Map<List<CourseDTO>>(courses);
            return ApiResultHelper.Success(courseDtoList);
        }

        /// <summary>
        /// 添加课程信息
        /// </summary>
        /// <param name="coursename"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ApiResult> Create(string coursename)
        {
            if (string.IsNullOrWhiteSpace(coursename)) return ApiResultHelper.Error("课程名不能为空");

            Course course = new Course
            {
                CourseName = coursename
            };
            bool b=await _iCourseService.CreateAsync(course);
            if (!b) return ApiResultHelper.Error("课程信息添加失败");
            return ApiResultHelper.Success(course);
        }

        /// <summary>
        /// 删除课程信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ApiResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return ApiResultHelper.Error("课程编号不能为空");

            bool b=await _iCourseService.DeleteAsync(Convert.ToInt32(id));
            if (!b) return ApiResultHelper.Error("课程信息删除失败");
            return ApiResultHelper.Success(b);
        }

        /// <summary>
        /// 修改课程信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="coursename"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(int id, string coursename)
        {
            var course=await _iCourseService.FindAsync(id);
            if (course == null) return ApiResultHelper.Error("课程信息不存在");

            if (string.IsNullOrWhiteSpace(coursename)) return ApiResultHelper.Error("课程名不能为空");
            course.CourseName = coursename;

            bool b=await _iCourseService.EditAsync(course);
            if (!b) return ApiResultHelper.Error("课程信息修改失败");
            return ApiResultHelper.Success(course);
        }
    }
}

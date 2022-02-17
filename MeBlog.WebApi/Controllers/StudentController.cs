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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _iStudentService;

        public StudentController(IStudentService iStudentService)
        {
            this._iStudentService = iStudentService;
        }

        /// <summary>
        /// 获取所有学生信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("Students")]
        public async Task<ApiResult> Students([FromServices]IMapper iMapper)
        {
            var students=await _iStudentService.QueryAsync();
            if (students.Count == 0) return ApiResultHelper.Error("没有学生信息");

            var studentDTOList=iMapper.Map<List<StudentDTO>>(students);
            return ApiResultHelper.Success(studentDTOList);
        }

        /// <summary>
        /// 获取学生信息
        /// </summary>
        /// <param name="iMapper"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Student")]
        public async Task<ApiResult> Student([FromServices]IMapper iMapper,int id)
        {
            var student=await _iStudentService.FindAsync(id);
            if (student == null) return ApiResultHelper.Error("学生信息不存在");

            var studentDTO = iMapper.Map<StudentDTO>(student);
            return ApiResultHelper.Success(studentDTO);
        }

        /// <summary>
        /// 添加学生信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="idcard"></param>
        /// <param name="sex"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ApiResult> Create(string name,string idcard,string sex,string password)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(idcard) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(sex)) return ApiResultHelper.Error("学生信息填写不完整");

            string pwd = MD5Helper.MD5Encrypt32(password);
            Student student = new Student
            {
                Name = name,
                IDCard = idcard,
                Sex = sex,
                Password = pwd
            };
            var OldStudent=await _iStudentService.FindAsync(s => s.IDCard == idcard);
            if (OldStudent != null) return ApiResultHelper.Error("学生信息已经存在");

            bool b=await _iStudentService.CreateAsync(student);
            if (!b) return ApiResultHelper.Error("学生信息添加失败");
            return ApiResultHelper.Success(student);
        }

        /// <summary>
        /// 删除学生信息
        /// </summary>
        /// <param name="idcard"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ApiResult> Delete(string idcard)
        {
            bool b=await _iStudentService.DeleteAsync(s=>s.IDCard==idcard);
            if (!b) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success(b);
        }

        /// <summary>
        /// 修改学生信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="oldidcard"></param>
        /// <param name="idcard"></param>
        /// <param name="sex"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(string name,string password,string oldidcard, string idcard, string sex,string role)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(oldidcard) || string.IsNullOrWhiteSpace(idcard) || string.IsNullOrWhiteSpace(sex) || string.IsNullOrWhiteSpace(role)||string.IsNullOrWhiteSpace(password)) return ApiResultHelper.Error("学生信息不能为空");

            var student=await _iStudentService.FindAsync(s => s.IDCard == oldidcard);
            if (student == null) return ApiResultHelper.Error("要修改的学生信息不存在");
            student.Name = name;
            student.IDCard = idcard;
            student.Role = Convert.ToInt32(role);
            student.Sex = sex;
            student.Password = MD5Helper.MD5Encrypt32(password);

            bool b=await _iStudentService.EditAsync(student);
            if (!b) return ApiResultHelper.Error("学生信息修改失败");
            return ApiResultHelper.Success(student);
        }
    }
}

using AutoMapper;
using MeBlog.IService;
using MeBlog.Model;
using MeBlog.Model.DTO;
using MeBlog.WebApi.Utility.ApiResult;
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
    public class ScoreController : ControllerBase
    {
        private readonly IScoreService _iScoreService;

        public ScoreController(IScoreService iScoreService)
        {
            this._iScoreService = iScoreService;
        }

        /// <summary>
        /// 获取所有成绩信息
        /// </summary>
        /// <param name="iMapper"></param>
        /// <returns></returns>
        [HttpGet("Scores")]
        public async Task<ApiResult> Scores([FromServices]IMapper iMapper)
        {
            var scores=await _iScoreService.QueryAsync();
            if (scores.Count == 0) return ApiResultHelper.Error("成绩列表信息为空");

            var scoreDtoList = iMapper.Map<List<ScoreDTO>>(scores);
            return ApiResultHelper.Success(scoreDtoList);
        }

        /// <summary>
        /// 添加学生成绩
        /// </summary>
        /// <param name="stuid"></param>
        /// <param name="courseid"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ApiResult> Create(int stuid,int courseid,int grade)
        {
            Score score = new Score
            {
                StuId = stuid,
                CourseId = courseid,
                Grade = grade
            };
            var oldScore=await _iScoreService.FindAsync(s => s.StuId == stuid && s.CourseId == courseid);
            if (oldScore != null) return ApiResultHelper.Error("该生本课程成绩已存在");

            bool b=await _iScoreService.CreateAsync(score);
            if (!b) return ApiResultHelper.Error("该生本课程成绩添加失败");
            return ApiResultHelper.Success(score);
        }

        /// <summary>
        /// 删除学生成绩信息
        /// </summary>
        /// <param name="stuid"></param>
        /// <param name="courseid"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ApiResult> Delete(int stuid,int courseid)
        {
            var score=await _iScoreService.FindAsync(c => c.StuId == stuid && c.CourseId == courseid);
            if (score == null) return ApiResultHelper.Error("要删除的学生成绩信息不存在");

            var b=await _iScoreService.DeleteAsync(score.Id);
            if (!b) return ApiResultHelper.Error("学生成绩信息删除失败");
            return ApiResultHelper.Success(b);
        }

        /// <summary>
        /// 修改学生成绩信息
        /// </summary>
        /// <param name="stuid"></param>
        /// <param name="courseid"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(int stuid,int courseid,int grade)
        {
            var score=await _iScoreService.FindAsync(c => c.StuId == stuid && c.CourseId == courseid);
            if (score == null) return ApiResultHelper.Error("要修改的学生成绩信息不存在");
            score.Grade = grade;
            score.StuId= stuid;
            score.CourseId=courseid;

            bool b=await _iScoreService.EditAsync(score);
            if (!b) return ApiResultHelper.Error("修改学生成绩信息失败");
            return ApiResultHelper.Success(b);
        }

    }
}

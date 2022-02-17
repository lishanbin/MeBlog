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
    public class JobLevelController : ControllerBase
    {
        private readonly IJobLevelService _iJobLevelService;

        public JobLevelController(IJobLevelService iJobLevelService)
        {
            this._iJobLevelService = iJobLevelService;
        }

        /// <summary>
        /// 获取所有的职称信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("JobLevels")]
        public async Task<ApiResult> GetJobLevels()
        {
            var jls=await _iJobLevelService.QueryAsync();
            if (jls.Count == 0) return ApiResultHelper.Error("职称信息为空！");
            return ApiResultHelper.Success(jls);
        }

        /// <summary>
        /// 添加职称信息
        /// </summary>
        /// <param name="jobLevelViewModel"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ApiResult> Create(JobLevelViewModel jobLevelViewModel)
        {
            if (string.IsNullOrWhiteSpace(jobLevelViewModel.Name) || string.IsNullOrWhiteSpace(jobLevelViewModel.TitleLevel))
            {
                return ApiResultHelper.Error("职称信息不能为空");
            }

            JobLevel jobLevel = new JobLevel
            {
                Name = jobLevelViewModel.Name,
                TitleLevel = jobLevelViewModel.TitleLevel,
                Enabled = jobLevelViewModel.Enabled
            };

            bool b = await _iJobLevelService.CreateAsync(jobLevel);
            if (!b) return ApiResultHelper.Error("职称信息添加失败！");
            return ApiResultHelper.Success(jobLevel);

        }

        /// <summary>
        /// 删除职称信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ApiResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return ApiResultHelper.Error("参数有误！");
            bool b= await _iJobLevelService.DeleteAsync(Convert.ToInt32(id));
            if (!b) return ApiResultHelper.Error("删除职称失败");
            return ApiResultHelper.Success(b);
        }

        /// <summary>
        /// 批量删除职称信息
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
                bool b = await _iJobLevelService.DeleteAsync(Convert.ToInt32(id));
                if (!b) return ApiResultHelper.Error("删除职位信息失败！");
            }

            return ApiResultHelper.Success("删除成功！");
        }

        /// <summary>
        /// 修改职称信息
        /// </summary>
        /// <param name="jobLevelViewModel"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(JobLevelViewModel jobLevelViewModel)
        {
            var jobLevel=await _iJobLevelService.FindAsync(jobLevelViewModel.Id);
            if (jobLevel == null) return ApiResultHelper.Error("没有找到要修改的职称信息！");
            jobLevel.Name=jobLevelViewModel.Name;
            jobLevel.TitleLevel=jobLevelViewModel.TitleLevel;
            jobLevel.Enabled=jobLevelViewModel.Enabled;
            jobLevel.CreateDate = jobLevelViewModel.CreateDate;

            bool b = await _iJobLevelService.EditAsync(jobLevel);
            if (!b) return ApiResultHelper.Error("职称信息编辑失败！");
            return ApiResultHelper.Success(b);
        }

    }
}

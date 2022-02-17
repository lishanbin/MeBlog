using MeBlog.IService;
using MeBlog.Model;
using MeBlog.WebApi.Utility.ApiResult;
using MeBlog.WebApi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _iDepartmentService;

        public DepartmentController(IDepartmentService iDepartmentService)
        {
            this._iDepartmentService = iDepartmentService;
        }

        private async Task<List<DepartmentViewModel>> Departments(int pid)
        {
            List<DepartmentViewModel> deps = new List<DepartmentViewModel>();
            var departmentList = await _iDepartmentService.QueryAsync(d => d.ParentId == pid);
            if (departmentList.Count > 0)
            {
                foreach (var dep in departmentList)
                {
                    DepartmentViewModel departmentViewModel = new DepartmentViewModel
                    {
                        Id = dep.Id,
                        Name = dep.Name,
                        ParentId = dep.ParentId,
                        DepPath = dep.DepPath,
                        Enabled = dep.Enabled,
                        IsParent = dep.IsParent,
                        Result = dep.Result
                    };
                    List<DepartmentViewModel> dps =await Departments(dep.Id);
                    if (dps.Count>0)
                    {
                        departmentViewModel.Children = dps;
                    }                    
                    deps.Add(departmentViewModel);
                }
            }
            return deps;
        }

        /// <summary>
        /// 获取所有部门信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("Departments")]
        public async Task<ApiResult> GetDepartments()
        {
            int pid = -1;
            List<DepartmentViewModel> deps=await this.Departments(pid);
            if (deps.Count == 0) return ApiResultHelper.Error("部门名称列表为空！");
            return ApiResultHelper.Success(deps);
        }

        /// <summary>
        /// 部门信息添加
        /// </summary>
        /// <param name="departmentViewModel"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ApiResult> Create(DepartmentViewModel departmentViewModel)
        {
            if (string.IsNullOrWhiteSpace(departmentViewModel.Name))
                return ApiResultHelper.Error("部门名称不能为空！");

            Department department = new Department
            {
                Name = departmentViewModel.Name,
                ParentId = departmentViewModel.ParentId,
                DepPath = departmentViewModel.DepPath,
                Enabled = departmentViewModel.Enabled,
                IsParent = departmentViewModel.IsParent
            };

            int id=await _iDepartmentService.CreateReturnIdAsync(department);
            if (id<=0) return ApiResultHelper.Error("部门信息添加失败");
            var dep=await _iDepartmentService.FindAsync(id);
            return ApiResultHelper.Success(dep);
        }

        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ApiResult> Delete(int id)
        {
            bool b=await _iDepartmentService.DeleteAsync(id);
            if (!b) return ApiResultHelper.Error("部门信息删除失败！");
            return ApiResultHelper.Success(b);
        }


    }
}

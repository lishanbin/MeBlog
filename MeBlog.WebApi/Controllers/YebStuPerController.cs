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
    public class YebStuPerController : ControllerBase
    {
        private readonly IYebStuPerService _iYebStuPerService;
        private readonly IStudentService _iStudentService;

        public YebStuPerController(IYebStuPerService iYebStuPerService,IStudentService iStudentService)
        {
            this._iYebStuPerService = iYebStuPerService;
            this._iStudentService = iStudentService;
        }

        /// <summary>
        /// 获取所有学生角色信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("YebStuPers")]
        public async Task<ApiResult> GetYebStuPers(string keywords=null)
        {
            List<YebStuPer> yebStuPers = new List<YebStuPer>();
            if (keywords==null|| keywords.Length==0)
            {
                yebStuPers = await _iYebStuPerService.QueryAsync();
            }
            else
            {
                yebStuPers = await _iYebStuPerService.QueryAsync(s => s.Student.Name.Contains(keywords));
            }
            
            if (yebStuPers.Count == 0) return ApiResultHelper.Error("暂无学生角色信息");

            List<YebStuPerViewModel> list = new List<YebStuPerViewModel>();

            foreach (var yebStuPer in yebStuPers)
            {
                bool flag = false;
                foreach (var item in list)
                {
                    if (item.StuId == yebStuPer.StudentId)
                    {
                        flag = true;
                        item.PermissId.Add(yebStuPer.PermissId);
                        item.PerName.Add(yebStuPer.Permiss.Name);
                        item.PerNameZh.Add(yebStuPer.Permiss.NameZh);
                        break;
                    }
                }
                if (!flag)
                {
                    YebStuPerViewModel yebStuPerViewModel = new YebStuPerViewModel();

                    yebStuPerViewModel.Id = yebStuPer.Id;
                    yebStuPerViewModel.StuId = yebStuPer.StudentId;
                    yebStuPerViewModel.StuName = yebStuPer.Student.Name;
                    yebStuPerViewModel.IDCard = yebStuPer.Student.IDCard;
                    yebStuPerViewModel.Sex = yebStuPer.Student.Sex;
                    yebStuPerViewModel.Password = yebStuPer.Student.Password;
                    yebStuPerViewModel.PermissId.Add(yebStuPer.PermissId);
                    yebStuPerViewModel.PerName.Add(yebStuPer.Permiss.Name);
                    yebStuPerViewModel.PerNameZh.Add(yebStuPer.Permiss.NameZh);

                    list.Add(yebStuPerViewModel);
                }

            }
            return ApiResultHelper.Success(list);
        }

        /// <summary>
        /// 删除学生角色信息
        /// </summary>
        /// <param name="stuId"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ApiResult> Delete(int stuId)
        {
            bool stuB=await _iStudentService.DeleteAsync(stuId);
            if (stuB)
            {
                bool yB=await _iYebStuPerService.DeleteAsync(s => s.StudentId == stuId);
                if (yB)
                {
                    return ApiResultHelper.Success("学生角色信息删除成功");
                }
                else
                {
                    return ApiResultHelper.Error("删除失败！");
                }
            }
            else
            {
                return ApiResultHelper.Error("删除失败！");
            }
        }

        /// <summary>
        /// 更新学生角色信息
        /// </summary>
        /// <param name="updateYebStuPer"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<ApiResult> Update(UpdateYebStuPer updateYebStuPer)
        {
            bool ub=await _iYebStuPerService.DeleteAsync(s=>s.StudentId== updateYebStuPer.StuId);
            if (!ub) return ApiResultHelper.Error("更新失败(删除)");
            if (updateYebStuPer.PermissId.Count>0)
            {
                foreach (var pid in updateYebStuPer.PermissId)
                {
                    YebStuPer yebStuPer = new YebStuPer
                    {
                        StudentId = updateYebStuPer.StuId,
                        PermissId = pid
                    };
                    bool b=await _iYebStuPerService.CreateAsync(yebStuPer);
                    if (!b) return ApiResultHelper.Error("更新失败(添加)");
                }
            }
            return ApiResultHelper.Success("学生角色更新成功！");
        }


    }
}

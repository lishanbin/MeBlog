using MeBlog.IRepository;
using MeBlog.IService;
using MeBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeBlog.Service
{
    public class JobLevelService:BaseService<JobLevel>,IJobLevelService
    {
        private readonly IJobLevelRepository _iJobLevelRepository;

        public JobLevelService(IJobLevelRepository iJobLevelRepository)
        {
            this._iJobLevelRepository = iJobLevelRepository;
            base._iBaseRepository = iJobLevelRepository;
        }
    }
}

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
    public class YebPerMenuService:BaseService<YebPerMenu>,IYebPerMenuService
    {
        private readonly IYebPerMenuRepository _iYebPerMenuRepository;

        public YebPerMenuService(IYebPerMenuRepository iYebPerMenuRepository)
        {
            this._iYebPerMenuRepository = iYebPerMenuRepository;
            base._iBaseRepository = iYebPerMenuRepository;
        }
    }
}

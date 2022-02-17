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
    public class YebMenuService:BaseService<YebMenu>,IYebMenuService
    {
        private readonly IYebMenuRepository _iYebMenuRepository;

        public YebMenuService(IYebMenuRepository iYebMenuRepository)
        {
            this._iYebMenuRepository = iYebMenuRepository;
            this._iBaseRepository = iYebMenuRepository;
        }
    }
}

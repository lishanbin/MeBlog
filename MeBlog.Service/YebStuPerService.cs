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
    public class YebStuPerService:BaseService<YebStuPer>,IYebStuPerService
    {
        private readonly IYebStuPerRepository _iYebStuPerRepository;

        public YebStuPerService(IYebStuPerRepository iYebStuPerRepository)
        {
            this._iYebStuPerRepository = iYebStuPerRepository;
            base._iBaseRepository = iYebStuPerRepository;
        }

        public override Task<List<YebStuPer>> QueryAsync()
        {
            return base.QueryAsync();
        }


    }
}

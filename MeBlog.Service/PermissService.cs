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
    public class PermissService:BaseService<Permiss>,IPermissService
    {
        private readonly IPermissRepository _iPermissRepository;

        public PermissService(IPermissRepository iPermissRepository)
        {
            this._iPermissRepository = iPermissRepository;
            base._iBaseRepository=iPermissRepository;
        }
    }
}

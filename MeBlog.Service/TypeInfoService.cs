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
    public class TypeInfoService : BaseService<TypeInfo>, ITypeInfoService
    {
        private readonly ITypeInfoRepository _iTypeInfoRepository;

        public TypeInfoService(ITypeInfoRepository iTypeInfoRepository)
        {
            this._iTypeInfoRepository = iTypeInfoRepository;
            base._iBaseRepository = iTypeInfoRepository;
        }
    }
}

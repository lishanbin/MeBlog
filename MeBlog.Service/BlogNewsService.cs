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
    public class BlogNewsService : BaseService<BlogNews>, IBlogNewsService
    {
        private readonly IBlogNewsRepository _iBlogNewsRepository;

        public BlogNewsService(IBlogNewsRepository iBlogNewsRepository)
        {
            this._iBlogNewsRepository = iBlogNewsRepository;
            base._iBaseRepository=iBlogNewsRepository;
        }
    }
}

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
    public class WriterInfoService : BaseService<WriterInfo>, IWriterInfoService
    {
        private readonly IWriterInfoRepository _iWriterInfoService;

        public WriterInfoService(IWriterInfoRepository iWriterInfoService)
        {
            this._iWriterInfoService = iWriterInfoService;
            base._iBaseRepository = iWriterInfoService;
        }
    }
}

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
    public class PositionService:BaseService<Position>,IPositionService
    {
        private readonly IPositionRepository _iPositionRepository;

        public PositionService(IPositionRepository iPositionRepository)
        {
            this._iPositionRepository = iPositionRepository;
            base._iBaseRepository = iPositionRepository;
        }
    }
}

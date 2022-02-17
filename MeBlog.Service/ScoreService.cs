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
    public class ScoreService:BaseService<Score>,IScoreService
    {
        private readonly IScoreRepository _iScoreRepository;

        public ScoreService(IScoreRepository iScoreRepository)
        {
            this._iScoreRepository = iScoreRepository;
            base._iBaseRepository=iScoreRepository;
        }
    }
}

using MeBlog.IRepository;
using MeBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeBlog.Repository
{
    public class ScoreRepository:BaseRepository<Score>,IScoreRepository
    {
        public override Task<List<Score>> QueryAsync()
        {
            return base.Context.Queryable<Score>()
                .Mapper(c => c.Student, c => c.StuId, c => c.Student.Id)
                .Mapper(c => c.Course, c => c.CourseId, c => c.Course.Id)
                .ToListAsync();
        }
    }
}

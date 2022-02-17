using MeBlog.IRepository;
using MeBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MeBlog.Repository
{
    public class YebStuPerRepository:BaseRepository<YebStuPer>,IYebStuPerRepository
    {
        public override Task<List<YebStuPer>> QueryAsync()
        {
            return base.Context.Queryable<YebStuPer>()
                        .Mapper(s => s.Student, s => s.StudentId, s => s.Student.Id)
                        .Mapper(s=>s.Permiss,s=>s.PermissId,s=>s.Permiss.Id)
                        .ToListAsync();
        }

        public override Task<List<YebStuPer>> QueryAsync(Expression<Func<YebStuPer, bool>> func)
        {
            return base.Context.Queryable<YebStuPer>()
                        .Mapper(s => s.Student, s => s.StudentId, s => s.Student.Id)
                        .Mapper(s => s.Permiss, s => s.PermissId, s => s.Permiss.Id)
                        .Where(func)
                        .ToListAsync();
        }
    }
}

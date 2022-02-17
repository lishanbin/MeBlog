using MeBlog.IRepository;
using MeBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeBlog.Repository
{
    public class DepartmentRepository:BaseRepository<Department>,IDepartmentRepository
    {
        public async Task<int> CreateReturnIdAsync(Department entity)
        {
            return await base.InsertReturnIdentityAsync(entity);
        }
    }
}

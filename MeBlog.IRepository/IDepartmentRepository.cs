using MeBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeBlog.IRepository
{
    public interface IDepartmentRepository:IBaseRepository<Department>
    {
        Task<int> CreateReturnIdAsync(Department entity);
    }
}

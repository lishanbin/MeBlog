using MeBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeBlog.IService
{
    public interface IDepartmentService:IBaseService<Department>
    {
        Task<int> CreateReturnIdAsync(Department entity);
    }
}

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
    public class DepartmentService:BaseService<Department>,IDepartmentService
    {
        private readonly IDepartmentRepository _iDepartmentRepository;

        public DepartmentService(IDepartmentRepository iDepartmentRepository)
        {
            this._iDepartmentRepository = iDepartmentRepository;
            base._iBaseRepository = iDepartmentRepository;            
        }

        public async Task<int> CreateReturnIdAsync(Department entity)
        {
           return await _iDepartmentRepository.CreateReturnIdAsync(entity);
        }
    }
}

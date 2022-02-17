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
    public class StudentService:BaseService<Student>,IStudentService
    {
        private readonly IStudentRepository _iStudentRepository;

        public StudentService(IStudentRepository iStudentRepository)
        {
            this._iStudentRepository = iStudentRepository;
            base._iBaseRepository = iStudentRepository;
        }
    }
}

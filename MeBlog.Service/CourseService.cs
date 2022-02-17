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
    public class CourseService:BaseService<Course>,ICourseService
    {
        private readonly ICourseRepository _iCourseRepository;

        public CourseService(ICourseRepository iCourseRepository)
        {
            this._iCourseRepository = iCourseRepository;
            base._iBaseRepository = iCourseRepository;
        }
    }
}

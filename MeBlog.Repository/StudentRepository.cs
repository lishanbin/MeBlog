using MeBlog.IRepository;
using MeBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeBlog.Repository
{
    public class StudentRepository:BaseRepository<Student>,IStudentRepository
    {
    }
}

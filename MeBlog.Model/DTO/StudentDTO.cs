using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeBlog.Model.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IDCard { get; set; }
        public string Sex { get; set; }
        public int Role { get; set; } //0 学生，1 老师
    }
}

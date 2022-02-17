using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeBlog.Model.DTO
{
    public class ScoreDTO
    {
        public int Id { get; set; }
        public int StuId { get; set; }
        public string StuName { get; set; }
        public string IDCard { get; set; }
        public string Sex { get; set; }
        public int Role { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Grade { get; set; }
    }
}

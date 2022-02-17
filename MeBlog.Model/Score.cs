using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace MeBlog.Model
{
    public class Score:BaseId
    {
        public int StuId { get; set; }
        [SugarColumn(IsIgnore = true)]
        public Student Student { get; set; }

        public int CourseId { get; set; }
        [SugarColumn(IsIgnore = true)]
        public Course Course { get; set; }

        public int Grade { get; set; }
    }
}

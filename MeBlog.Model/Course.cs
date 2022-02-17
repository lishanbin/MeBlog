using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace MeBlog.Model
{
    public class Course:BaseId
    {
        [SugarColumn(ColumnDataType ="nvarchar(16)")]
        public string CourseName { get; set; }
    }
}

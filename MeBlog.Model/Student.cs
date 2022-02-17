using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace MeBlog.Model
{
    public class Student:BaseId
    {
        [SugarColumn(ColumnDataType ="nvarchar(12)")]
        public string Name { get; set; }
        [SugarColumn(ColumnDataType ="nvarchar(18)")]
        public string IDCard { get; set; }
        [SugarColumn(ColumnDataType ="nvarchar(2)")]
        public string Sex { get; set; }
        [SugarColumn(ColumnDataType ="nvarchar(64)")]
        public string Password { get; set; }
        public int Role { get; set; } //0 学生，1 老师
    }
}

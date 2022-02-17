using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace MeBlog.Model
{
    public class JobLevel:BaseId
    {
        [SugarColumn(ColumnDataType ="nvarchar(16)")]
        public string Name { get; set; }
        [SugarColumn(ColumnDataType = "nvarchar(16)")]
        public string TitleLevel { get; set; }
        public DateTime CreateDate { get; set; }=DateTime.Now;
        public bool Enabled { get; set; } = true;
    }
}

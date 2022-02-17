using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace MeBlog.Model
{
    public class Department:BaseId
    {
        [SugarColumn(ColumnDataType ="nvarchar(30)")]
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string DepPath { get; set; }
        public bool Enabled { get; set; }
        public bool IsParent { get; set; }
        public string Result { get; set; }
    }
}

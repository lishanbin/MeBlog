using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace MeBlog.Model
{
    public class YebPerMenu:BaseId
    {
        public int PermissId { get; set; }
        [SugarColumn(IsIgnore = true)]
        public Permiss Permiss { get; set; }
        public int YebMenuId { get; set; }
        [SugarColumn(IsIgnore =true)]
        public YebMenu YebMenu { get; set; }
    }
}

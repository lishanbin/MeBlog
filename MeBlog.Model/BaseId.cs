﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace MeBlog.Model
{
    public class BaseId
    {
        [SugarColumn(IsPrimaryKey = true,IsIdentity =true)]
        public int Id { get; set; }
    }
}

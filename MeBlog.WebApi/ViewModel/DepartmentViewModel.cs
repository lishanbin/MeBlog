using System.Collections.Generic;

namespace MeBlog.WebApi.ViewModel
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string DepPath { get; set; }
        public bool Enabled { get; set; }=true;
        public bool IsParent { get; set; } = true;
        public string Result { get; set; } = null;
        public List<DepartmentViewModel> Children { get; set; } = null;
    }
}

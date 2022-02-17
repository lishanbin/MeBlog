using System.Collections.Generic;

namespace MeBlog.WebApi.ViewModel
{
    public class UpdateYebStuPer
    {
        public int StuId { get; set; }
        public List<int> PermissId { get; set; } = new List<int>();
    }
}

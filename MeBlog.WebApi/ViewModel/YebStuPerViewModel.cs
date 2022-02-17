using System.Collections.Generic;

namespace MeBlog.WebApi.ViewModel
{
    public class YebStuPerViewModel
    {
        public int Id { get; set; }
        public int StuId { get; set; }
        public string StuName { get; set; }
        public string IDCard { get; set; }
        public string Sex { get; set; }
        public string Password { get; set; }
        public List<int> PermissId { get; set; }=new List<int>();
        public List<string> PerName { get; set; } = new List<string>();
        public List<string> PerNameZh { get; set; } = new List<string>();
    }
}

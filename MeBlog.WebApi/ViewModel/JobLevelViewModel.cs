using System;

namespace MeBlog.WebApi.ViewModel
{
    public class JobLevelViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TitleLevel { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool Enabled { get; set; } = true;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace MeBlog.Model
{
    public class Employee:BaseId
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string IdCard { get; set; }
        public string Wedlock { get; set; }
        public int NationId { get; set; }
        public string NativePlace { get; set; }
        public int PoliticId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
        public int JobLevelId { get; set; }
        public int PosId { get; set; }
        public string EngageForm { get; set; }
        public string TiptopDegree { get; set; }
        public string Specialty { get; set; }
        public string School { get; set; }
        public DateTime BeginDate { get; set; }
        public string WorkState { get; set; }
        public string WorkID { get; set; }
        public string ContractTerm { get; set; }
        public DateTime ConversionTime { get; set; }
        public DateTime NotWorkDate { get; set; }
        public DateTime BeginContract { get; set; }
        public DateTime EndContract { get; set; }
        public int WorkAge { get; set; }
        public int SalaryId { get; set; }
        public Decimal Salary { get; set; }

    }
}

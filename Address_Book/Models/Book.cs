using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Address_Book.Models
{
    public class Book
    {
        public int id { get; set; }
        public string FullName { get; set; }
        public string JobTitle { get; set; }
        public string DepartmentName { get; set; }
        public Nullable <int> MobileNo { get; set; }
        public Nullable <int> HomeTelNo { get; set; }

        [DataType(DataType.Date)]
        public DateTime Dateofbirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public byte[] Photo { get; set; }

        [ForeignKey("Job")]
        public int Job_id { get; set; }
        public Job Job { get; set; }

        [ForeignKey("Department")]
        public int Dept_id { get; set; }
        public Department Department { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bisaTest
{
    public class Student
    {
        public int StudentID { get; set; }
        [StringLength(255)]
        public string FullName { get; set; }
        [StringLength(15)]
        public string Birthday { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        [StringLength(255)]
        public string ImageUrl { get; set; }
        public bool Gender { get; set; }
        [NotMapped]
        public int ObjectState { get; set; }
    }
}

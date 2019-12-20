using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWithSecutiry.Models
{
    public class EmployeeCourse
    {
        [Key]
        public long EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Key]
        public int CourseId { get; set; }
        public Courses Courses { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWithSecutiry.Models
{
    public class Courses
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }

        public ICollection<EmployeeCourse> EmployeeCourse { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWithSecutiry.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public ICollection<Employee> Employee { get; set; }
    }
}

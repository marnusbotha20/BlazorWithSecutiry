using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWithSecutiry.Models
{
    public enum Gender
    {
        [Description("Male")]
        Male,
        [Description("Female")]
        Female
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWithSecutiry.Models
{
    public class ContactUsDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string EmailAddress { get; set; }
        public string Message { get; set; }

    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseAPI.Models
{
    public class Course
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        [Range(0, 20)]
        public int? Credits { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelBlog.ViewModels
{
    public class CreateViewModel
    {
       
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace taske2.ViewModel
{
    public class SkillsVM
    {
        [Required(ErrorMessage= "Please enter your skill name")]  
        public string SkillName { get; set; }
    }
}
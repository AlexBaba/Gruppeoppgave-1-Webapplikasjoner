﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nettbutikk.Models
{
    public class ZipCode
    {
        [Key]
        [Required]
        public string Code
        {
            get;
            set;
        }

        [Required]
        public string City
        {
            get;
            set;
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblig1_Nettbutikk.Model
{
    public class PersonModel
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }

        // -1 if not present
        //public int AdminstratorId { get; set; }
        //public int CustomerId { get; set; }
    }
}
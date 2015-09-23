﻿using System.ComponentModel.DataAnnotations;

namespace Nettbutikk.Models
{
    public class Address
    {
        [Key]
        public int Id
        {
            get;
            set;
        }

        [Required]
        public string Street
        {
            get;
            set;
        }

        [Required]
        public string HouseNumber
        {
            get;
            set;
        }

        [Required]
        public string ZipCode
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

        [Required]
        public string State
        {
            get;
            set;
        }

        [Required]
        public string Country
        {
            get;
            set;
        }
    }
}
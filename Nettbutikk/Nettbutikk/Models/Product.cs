﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nettbutikk.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public long CategoryId { get; set; }
    
        public virtual Category Category { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nettbutikk.Models.Binding
{
    public class CreateProduct
    {
        public Guid Id;

        public string Name;

        public double Price;

        public string Description;

        public string CategoryName;
    }

    public class EditProduct
    {
        public Guid Id;

        public string Name;

        public double Price;

        public string Description;

        public string CategoryName;
    }
}
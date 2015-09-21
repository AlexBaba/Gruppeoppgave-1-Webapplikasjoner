﻿namespace Nettbutikk.Models
{
    public class OrderLine
    {
        public int Id
        {
            get;
            set;
        }

        public int Order
        {
            get;
            set;
        }

        public Product Product
        {
            get;
            set;
        }

        public int Amount
        {
            get;
            set;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralStore.Models
{
    public class ProductEdit
    {
        public string Name { get; set; } = null!;
        public double Price {get; set;}
        public int Quantity {get; set;}
    }
}
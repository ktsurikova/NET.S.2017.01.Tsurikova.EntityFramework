using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel
{
    /// <summary>
    /// class for working with goods info
    /// </summary>
    public class Goods
    {
        public int GoodsId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[] Photo { get; set; }

        public virtual Category Category { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}

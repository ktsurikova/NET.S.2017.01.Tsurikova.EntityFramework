using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel
{
    public class Order
    {
        public int OrderId { get; set; }
        public int Count { get; set; }

        public virtual Purchase Purchase { get; set; }
        public virtual Goods Goods { get; set; }
    }
}

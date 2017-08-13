using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel
{
    /// <summary>
    /// class for working with purchase info
    /// </summary>
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public DateTime Date { get; set; }

        public virtual List<Order> OrdersList { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel
{
    /// <summary>
    /// class for working with goods category
    /// </summary>
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual List<Goods> Goods { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel
{
    public class EntityModel : DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Goods> Goods { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
    }
}

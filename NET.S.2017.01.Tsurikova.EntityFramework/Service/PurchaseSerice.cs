using System;
using System.Collections.Generic;
using System.Linq;
using EntityModel;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Service
{
    public class PurchaseSerice
    {
        private readonly DbContext context;

        public PurchaseSerice(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException($"{nameof(context) is null}");
        }

        #region category

        public void AddCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException($"{nameof(category)} is null");
            if (context.Set<Category>().Find(category.Name) == null)
                return;
            context.Set<Category>().Add(category);
            context.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategories() => context.Set<Category>();

        #endregion

        #region goods

        public void AddGoods(Goods goods)
        {
            if (goods == null)
                throw new ArgumentNullException($"{nameof(goods)} is null");
            context.Set<Goods>().Add(goods);
            context.SaveChanges();
        }

        public void AddRangeOfGoods(IEnumerable<Goods> goods)
        {
            if (goods == null)
                throw new ArgumentNullException($"{nameof(goods)} is null");
            context.Set<Goods>().AddRange(goods);
            context.SaveChanges();

        }

        public IEnumerable<Goods> GetAllGoods() => context.Set<Goods>();

        public Goods GetFirstOrDefaultGoodsByPredicate(Expression<Func<Goods, bool>> expr)
        {
            if (expr == null)
                throw new ArgumentNullException($"{nameof(expr)} is null");
            return context.Set<Goods>().FirstOrDefault(expr);
        }

        public IEnumerable<Goods> GetAllGoodsByPredicate(Expression<Func<Goods, bool>> expr)
        {
            if (expr == null)
                throw new ArgumentNullException($"{nameof(expr)} is null");
            return context.Set<Goods>().Where(expr);
        }

        public void ChangePrice(Goods goods, decimal price)
        {
            if (goods == null)
                throw new ArgumentNullException($"{nameof(goods)} is null");
            if (price < 0) throw new ArgumentException("new price can't be negative");
            if (context.Set<Goods>().Find(goods.GoodsId) == null)
                throw new InvalidOperationException("There is no such goods");
            Goods changePriceGood = GetFirstOrDefaultGoodsByPredicate(i => i.GoodsId == goods.GoodsId);
            changePriceGood.Price = price;
            context.SaveChanges();
        }

        public void DeleteGoods(Goods goods)
        {
            if (goods == null)
                throw new ArgumentNullException($"{nameof(goods)} is null");
            if (context.Set<Goods>().Find(goods.GoodsId) == null)
                throw new InvalidOperationException("There is no such goods");
            context.Set<Order>().RemoveRange(goods.Orders);
            context.Set<Goods>().Remove(GetFirstOrDefaultGoodsByPredicate(i => i.GoodsId == goods.GoodsId));
            context.SaveChanges();
        }

        #endregion

        #region purchase

        public void AddPurchase(Purchase purchase, params Order[] orders)
        {
            if (purchase == null)
                throw new ArgumentNullException($"{nameof(purchase) is null}");
            if (orders == null)
                throw new ArgumentNullException($"{nameof(orders)} is null");

            context.Set<Order>().AddRange(orders);
            foreach (var item in orders)
            {
                item.Purchase = purchase;
            }
            context.Set<Purchase>().Add(purchase);

            context.SaveChanges();
        }

        public void AddOrdersToPurchase(Purchase purchase, params Order[] orders)
        {
            if (purchase == null)
                throw new ArgumentNullException($"{nameof(purchase) is null}");
            if (orders == null)
                throw new ArgumentNullException($"{nameof(orders)} is null");
            if (context.Set<Purchase>().Find(purchase.PurchaseId) == null)
                throw new InvalidOperationException("there is no such purchase");

            context.Set<Order>().AddRange(orders);
            foreach (var item in orders)
            {
                item.Purchase = purchase;
            }
            context.SaveChanges();
        }

        public void DeletePurchase(Purchase purchase)
        {
            if (purchase == null)
                throw new ArgumentNullException($"{nameof(purchase) is null}");
            if (context.Set<Purchase>().Find(purchase.PurchaseId) == null)
                throw new InvalidOperationException("there is no such purchase");

            context.Set<Order>().RemoveRange(purchase.OrdersList);
            context.Set<Purchase>().Remove(purchase);
            context.SaveChanges();
        }

        public IEnumerable<Purchase> GetAllPurchase() => context.Set<Purchase>();

        #endregion
    }
}

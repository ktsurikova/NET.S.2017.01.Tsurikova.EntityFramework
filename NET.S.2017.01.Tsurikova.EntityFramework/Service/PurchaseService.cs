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
    /// <summary>
    /// class for working with entities
    /// </summary>
    public class PurchaseService
    {
        private readonly DbContext context;

        /// <summary>
        /// creates new instance with specified context
        /// </summary>
        /// <param name="context">dbcontextparam>
        public PurchaseService(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException($"{nameof(context) is null}");
        }

        #region category

        /// <summary>
        /// add new category of goods
        /// </summary>
        /// <param name="category">category to be added</param>
        public void AddCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException($"{nameof(category)} is null");
            if (context.Set<Category>().Find(category.Name) == null)
                return;
            context.Set<Category>().Add(category);
            context.SaveChanges();
        }

        /// <summary>
        /// retun all available categories of goods
        /// </summary>
        /// <returns>all available categories of goods</returns>
        public IEnumerable<Category> GetAllCategories() => context.Set<Category>();

        #endregion

        #region goods

        /// <summary>
        /// adds goods
        /// </summary>
        /// <param name="goods">goods to be added</param>
        public void AddGoods(Goods goods)
        {
            if (goods == null)
                throw new ArgumentNullException($"{nameof(goods)} is null");
            context.Set<Goods>().Add(goods);
            context.SaveChanges();
        }

        /// <summary>
        /// adds range of goods
        /// </summary>
        /// <param name="goods">goods to be added</param>
        public void AddRangeOfGoods(IEnumerable<Goods> goods)
        {
            if (goods == null)
                throw new ArgumentNullException($"{nameof(goods)} is null");
            context.Set<Goods>().AddRange(goods);
            context.SaveChanges();

        }

        /// <summary>
        /// returns all goods
        /// </summary>
        /// <returns>all goods</returns>
        public IEnumerable<Goods> GetAllGoods() => context.Set<Goods>();

        /// <summary>
        /// find first goods that satisfies specified conditions
        /// </summary>
        /// <param name="expr">condition</param>
        /// <returns>first goods if exisits otherwise null</returns>
        public Goods GetFirstOrDefaultGoodsByPredicate(Expression<Func<Goods, bool>> expr)
        {
            if (expr == null)
                throw new ArgumentNullException($"{nameof(expr)} is null");
            return context.Set<Goods>().FirstOrDefault(expr);
        }

        /// <summary>
        /// find all goods that satisfy specifies conditions
        /// </summary>
        /// <param name="expr">condition</param>
        /// <returns>all goods that satisfy specifies conditions</returns>
        public IEnumerable<Goods> GetAllGoodsByPredicate(Expression<Func<Goods, bool>> expr)
        {
            if (expr == null)
                throw new ArgumentNullException($"{nameof(expr)} is null");
            return context.Set<Goods>().Where(expr);
        }

        /// <summary>
        /// change price of specifies goodsS
        /// </summary>
        /// <param name="goods">goods to be changed</param>
        /// <param name="price">new price</param>
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

        /// <summary>
        /// delete goods
        /// </summary>
        /// <param name="goods">goods to be removed</param>
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

        /// <summary>
        /// add purchase
        /// </summary>
        /// <param name="purchase">puchase to be added</param>
        /// <param name="orders">orders to be added</param>
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

        /// <summary>
        /// adds orders to purchase
        /// </summary>
        /// <param name="purchase">purchase in which orders to be added</param>
        /// <param name="orders">orders to be added</param>
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

        /// <summary>
        /// removes purchase with its orders
        /// </summary>
        /// <param name="purchase">purchase to be removed</param>
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

        /// <summary>
        /// return all purchase
        /// </summary>
        /// <returns>all purchase</returns>
        public IEnumerable<Purchase> GetAllPurchase() => context.Set<Purchase>();

        #endregion
    }
}

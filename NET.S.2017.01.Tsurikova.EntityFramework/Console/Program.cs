using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using EntityModel;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new EntityModel.EntityModel())
            {
                PurchaseService service = new PurchaseService(context);

                //Category clothes = new Category() { Name = "Clothes" };
                //Category shoes = new Category() { Name = "Shoes" };

                //service.AddCategory(clothes);
                //service.AddCategory(shoes);

                //Goods skirt = new Goods()
                //{
                //    Name = "Skirt",
                //    Description = "good decision during the heat",
                //    Price = 5.56M,
                //    Category = clothes
                //};

                //Goods sandals = new Goods()
                //{
                //    Name = "Sandals",
                //    Description = "good decision during the heat",
                //    Price = 5.56M,
                //    Category = shoes
                //};

                //service.AddGoods(skirt);
                //service.AddGoods(sandals);

                //Category product = new Category() { Name = "Product" };
                //context.Categories.Add(product);

                //Goods apple = new Goods() { Name = "Graspe", Category = product, Description = "taste fruit", Price = 1.23M };
                //context.Goods.Add(apple);

                //Goods apple2 = new Goods() { Name = "FRuita", Category = product, Description = "taste fruit", Price = 3.23M };
                //context.Goods.Add(apple2);

                //context.SaveChanges();

                //////////////foreach (var item in context.Goods)
                //////////////{
                //////////////    Console.WriteLine(item.Name);
                //////////////}

                //////////////Console.WriteLine(context.Goods.First().Name);

                //Purchase p = new Purchase() { Date = DateTime.Now };
                //Order o1 = new Order() { Goods = context.Goods.First(), Count = 2, Purchase = p };
                //Order o2 = new Order() { Goods = context.Goods.Where(i=>i.Price>3M).First(), Count = 1, Purchase = p };
                ////////p.OrdersList.Add(o1);
                ////////p.OrdersList.Add(o2);

                //context.Orders.Add(o1);
                //context.Orders.Add(o2);

                //context.Purchases.Add(p);
                //context.SaveChanges();

                //foreach (var item in context.Purchases)
                //{
                //    Console.WriteLine(item.Date);
                //    foreach (var item2 in item.OrdersList)
                //    {
                //        Console.WriteLine($"\t {item2.Goods.Name} {item2.Count}");
                //    }
                //}

                //Purchase p = context.Purchases.First();
                //Console.WriteLine(p.Date);
                //foreach (var item in p.OrdersList)
                //{
                //    Console.WriteLine(item.GoodsId);
                //}

                //context.SaveChanges();

                //foreach (var item in context.Categories)
                //{
                //    Console.WriteLine(item.Name);
                //}

                //service.GetAllGoods().First().Name = "Grapes";

                //Goods g = service.GetByPredicate(i => i.Price > 5);
                //Console.WriteLine(g.Name);

                //service.ChangePrice(service.GetFirstOrDefaultGoodsByPredicate(i => i.GoodsId == 3), 7.34M);
                //service.DeleteGoods(service.GetFirstOrDefaultGoodsByPredicate(i => i.GoodsId == 1));

                //foreach (var item in service.GetAllGoodsByPredicate(i=>i.Price>1))
                //
                //    Console.WriteLine($"{item.Name} {item.Price:C}");
                //}

                // Order o = new Order() { Goods = service.GetFirstOrDefaultGoodsByPredicate(i => i.GoodsId == 4), Count = 3 };
                //Order o = context.Orders.Find(3);

                //Purchase p = service.GetAllPurchase().First();
                //service.AddOrdersToPurchase(p, o);

                //foreach (var item in service.GetAllPurchase())
                //{
                //    Console.WriteLine(item.Date);
                //    foreach (var item2 in item.OrdersList)
                //    {
                //        Console.WriteLine($"{item2.Count} {item2.Goods.Name}");
                //    }
                //}

                Purchase p = new Purchase() { Date = DateTime.Now };
                Order o = new Order() { Goods = service.GetFirstOrDefaultGoodsByPredicate(i => i.GoodsId == 3), Count = 1 };
                Order o2 = new Order() { Goods = service.GetFirstOrDefaultGoodsByPredicate(i => i.GoodsId == 4), Count = 1 };
                service.AddPurchase(p, o, o2);

                foreach (var item in service.GetAllPurchase())
                {
                    Console.WriteLine(item.Date);
                    foreach (var item2 in item.OrdersList)
                    {
                        Console.WriteLine($"{item2.Count} {item2.Goods.Name}");
                    }
                }

                Console.ReadKey();

            }
        }
    }
}

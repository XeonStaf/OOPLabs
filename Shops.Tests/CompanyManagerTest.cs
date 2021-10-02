using System;
using System.Collections.Generic;
using Shops.Services;
using NUnit.Framework;
using Shops.Models;
using Shops.Tools;

namespace Shops.Tests
{
    public class CompanyManagerTest
    {
        private ICompanyManager _company;

        [SetUp]
        public void Setup()
        {
            _company = new CompanyManager();
        }

        [Test]
        public void SupplyProduct_ShopContainsThisProduct()
        {
            var shop = _company.AddShop("Test", "Test2", 1000);
            var product = _company.AddProduct("Milk");
            var order = new Order();
            order.AddProductToOrder(product, 20, 50);
            shop.SupplyOrder(order);
            Assert.True(shop.CanBuy(product.Id));
        }

        [Test]
        public void SupplyProducts_ShopContainsAllThisProduct()
        {
            var shop = _company.AddShop("Test", "Test2", 1000);
            Guid milkId = _company.AddProduct("Milk").Id;
            Guid cheeseId = _company.AddProduct("Cheese").Id;
            var order = new Order();
            order.AddProductToOrder(_company.GetProduct(milkId), 20, 60);
            order.AddProductToOrder(_company.GetProduct(cheeseId), 40, 200);
            shop.SupplyOrder(order);
            Assert.True(shop.CanBuy(milkId));
            Assert.True(shop.CanBuy(cheeseId));
        }

        [Test]
        public void ChangeProductCost_ProductShouldHaveNewCost()
        {
            var shop = _company.AddShop("Test", "Test2", 1000);
            Product milk = _company.AddProduct("Milk");
            var order = new Order();
            order.AddProductToOrder(milk, 10, 10);
            shop.SupplyOrder(order);
            Assert.AreEqual(shop.GetCostOfTheProduct(milk.Id), 10);
            shop.ChangeCost(milk, 40);
            Assert.AreEqual(shop.GetCostOfTheProduct(milk.Id), 40);
        }

        [Test]
        public void FindCheapestShop_ShouldFindFirstAndLastShops()
        {
            Product targetProduct = _company.AddProduct("Milk");
            const int n = 30;
            for (int i = 0; i < n; i++)
            {
                Shop shop = _company.AddShop("Test Shop" + i.ToString(), i.ToString(), 1000);
                var order = new Order();
                order.AddProductToOrder(targetProduct, 100 + i * 100, i + 1);
                shop.SupplyOrder(order);
            }

            Shop shop1 = _company.FindCheapestShop(targetProduct, 1);
            Shop shop2 = _company.FindCheapestShop(targetProduct, n);
            Assert.AreEqual("0",shop1.Address);
            Assert.AreEqual("29", shop2.Address);
        }

        [Test]
        public void ClientPurchaseProducts_Successfully()
        {
            Shop shop = _company.AddShop("Test", "Test Adress", 1000);
            Product milk = _company.AddProduct("Milk");
            Product cheese = _company.AddProduct("Cheese");
            var order = new Order();
            order.AddProductToOrder(milk, 100, 20);
            order.AddProductToOrder(cheese, 150, 30);
            shop.SupplyOrder(order);

            var client = new Client("Ivan", 5000);
            var groceryList = new List<(Product product, int amount)>();
            groceryList.Add((milk, 7));
            groceryList.Add((cheese, 2));
            shop.MakeAPurchase(client, groceryList);
            Assert.AreEqual(13, shop.GetAmountOfTheProduct(milk.Id));
            Assert.AreEqual(28, shop.GetAmountOfTheProduct(cheese.Id));
        }

        [Test]
        public void ClientPurchaseProducts_ClientHasNoMoneyCatch()
        {
            Shop shop = _company.AddShop("Test", "Test Adress", 1000);
            Product milk = _company.AddProduct("Elite Milk");
            var order = new Order();
            order.AddProductToOrder(milk, 200, 200);
            shop.SupplyOrder(order);

            var client = new Client("Andrey", 100);
            var groceryList = new List<(Product product, int amount)>();
            groceryList.Add((milk, 1));
            Assert.Catch<CompanyManagerException>(() =>
            {
                shop.MakeAPurchase(client, groceryList);
            });

        }
        
    }
}
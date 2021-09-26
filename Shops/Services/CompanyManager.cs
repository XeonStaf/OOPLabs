using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Models;
using Shops.Tools;

namespace Shops.Services
{
    public class CompanyManager : ICompanyManager
    {
        private List<Product> _products = new List<Product>();
        private List<Shop> _shops = new List<Shop>();

        public Product AddProduct(string name)
        {
            var newProduct = new Product(name);
            _products.Add(newProduct);
            return newProduct;
        }

        public Shop AddShop(string name, string address, int startMoney)
        {
            var newShop = new Shop(name, address, startMoney);
            _shops.Add(newShop);
            return newShop;
        }

        public Product GetProduct(Guid id)
        {
            foreach (Product product in _products.Where(product => product.Id == id))
            {
                return product;
            }

            throw new CompanyManagerException("This product is not exist");
        }

        public Shop FindCheapestShop(Product product, int amount)
        {
            int min = int.MaxValue;
            Shop cheapestShop = null;
            foreach (Shop shop in _shops)
            {
                try
                {
                    int value = shop.GetCostOfTheProduct(product.Id);
                    int amountInShop = shop.GetAmountOfTheProduct(product.Id);
                    if (value >= min || amountInShop < amount) continue;
                    min = value;
                    cheapestShop = shop;
                }
                catch
                {
                    continue;
                }
            }

            if (cheapestShop == null)
                throw new CompanyManagerException("This product are not exists in any shops\nOr there are not enough");
            return cheapestShop;
        }
    }
}
using System;
using Shops.Models;

namespace Shops.Services
{
    public interface ICompanyManager
    {
        Product AddProduct(string name);
        Shop AddShop(string name, string address, int startMoney);
        Product GetProduct(Guid id);
        Shop FindCheapestShop(Product product, int amount);
    }
}
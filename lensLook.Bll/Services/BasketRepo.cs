using lensLook.Dal;
using lensLook.Dal.Context;
using lensLook.Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace lensLook.Bll.Services
{
    public class BasketRepo : IBasketRepo
    {
        private readonly LensLookDbContext _context;

        public BasketRepo(LensLookDbContext context)
        {
            _context = context;
        }

        public int GetCountBasketItems(string userId)
        {
            var basketCustomer = _context.BasketCustomers
                .Include(bc => bc.BasketItems)
                .FirstOrDefault(bc => bc.UserId == userId);

            return basketCustomer?.BasketItems.Count ?? 0;
        }

        public BasketCustomer GetCustomerBasket(string userId)
        {
            return _context.BasketCustomers
                .Include(bc => bc.BasketItems)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefault(bc => bc.UserId == userId);
        }

        public BasketCustomer GetCustomerBasketWithProduct(string userId)
        {
            return GetCustomerBasket(userId); // This method can be consolidated with GetCustomerBasket
        }

        public BasketCustomer GetCustomerBasketWithProductById(int customerBasketId)
        {
            return _context.BasketCustomers
                .Include(bc => bc.BasketItems)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefault(bc => bc.Id == customerBasketId);
        }

        public bool UpdateBasket(BasketCustomer newBasket)
        {
            try
            {
                _context.BasketCustomers.Update(newBasket);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Uncomment and improve if needed later
        // public bool DeleteBasket(BasketCustomer customer)
        // {
        //     try
        //     {
        //         _context.BasketCustomers.Remove(customer);
        //         _context.SaveChanges();
        //         return true;
        //     }
        //     catch (Exception)
        //     {
        //         return false;
        //     }
        // }

        // public async Task<BasketCustomer?> GetBasketCustomer(int id)
        // {
        //     return await _context.BasketCustomers
        //         .FirstOrDefaultAsync(bc => bc.Id == id);
        // } 

        // public async Task<BasketCustomer?> UpdateBasket(BasketCustomer basket)
        // {
        //     if (basket.Id != 0)
        //     {
        //         _context.BasketCustomers.Update(basket);
        //         await _context.SaveChangesAsync();
        //         return await _context.BasketCustomers
        //             .FirstOrDefaultAsync(bc => bc.Id == basket.Id);
        //     }
        //     else
        //     {
        //         _context.BasketCustomers.Add(basket);
        //         await _context.SaveChangesAsync();
        //         return null;
        //     }
        // }
    }
}

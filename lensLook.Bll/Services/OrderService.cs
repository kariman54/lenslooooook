using lensLook.Dal;
using lensLook.Dal.Context;
using lensLook.Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lensLook.Bll.Services
{
    public class OrderService : IOrderService
    {
        private readonly LensLookDbContext _context;

        public OrderService(LensLookDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(Order model)
        {
            await _context.Order.AddAsync(model);

            var basket = _context.BasketCustomers
                .Include(bc => bc.BasketItems)
                .FirstOrDefault(bc => bc.UserId == model.UserIdCreateOrder);

            if (basket == null)
            {
                throw new InvalidOperationException("Basket not found for the user.");
            }

            model.SubTotal = model.Items.Sum(item => item.Price * item.Quantity) + 10;

            basket.BasketItems.Clear();
            _context.BasketCustomers.Update(basket);

            await _context.SaveChangesAsync();

            return model;
        }

        public List<Order> GetallOrder()
        {
            return _context.Order.ToList();
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Order
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == orderId);
        }

        public List<Order> GetOrdersForUser(string userId)
        {
            return _context.Order
                .Where(o => o.UserIdCreateOrder == userId)
                .Include(o => o.Items)
                .ToList();
        }

        public bool RemoveOrder(int orderId)
        {
            try
            {
                var order = GetOrderById(orderId);
                if (order == null)
                {
                    return false;
                }

                _context.Order.Remove(order);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int TotalOrders()
        {
            return _context.Order.Count();
        }

        public bool Update(Order model)
        {
            try
            {
                _context.Order.Update(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;

                throw;
            }
        }
    }
}

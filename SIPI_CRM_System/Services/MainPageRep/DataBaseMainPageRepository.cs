﻿using System;
using Microsoft.EntityFrameworkCore;
using SIPI_CRM_System.Models;

namespace SIPI_CRM_System.Services.MainPageRep
{
	public class DataBaseMainPageRepository : IMainPageRepository
	{
        private readonly CRMdbContext _context;

		public DataBaseMainPageRepository(CRMdbContext context)
		{
            _context = context;
		}

        public async void CreateDailyOrder(DailyOrder order)
        {
            _context.DailyOrders.Add(order);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<DailyOrder> GetDayliOrders()
        {
            return _context.DailyOrders.Include(x => x.DailyOrderDishes).ThenInclude(x => x.Dish);
        }

        public IEnumerable<Dish> GetDishes()
        {
            return _context.Dishes;
        }

        public IEnumerable<Table> GetTables()
        {
            return _context.Tables;
        }

        public DailyOrder GetDailyOrderById(int id)
        {
            return _context.DailyOrders
                .Include(x => x.Table)
                .Include(x => x.DailyOrderDishes)
                .ThenInclude(x => x.Dish)
                .ThenInclude(x => x.ProductDishes)
                .ThenInclude(x => x.Product)
                .FirstOrDefault(x => x.Id == id);
        }

        public async void SetOrderStatusDone(int id)
        {
            var dailyOrder = _context.DailyOrders.Include(x => x.DailyOrderDishes).First(x => x.Id == id);
            dailyOrder.IsDone = true;

            var order = new Order()
            {
                Id = _context.Orders.Any() ? _context.Orders.OrderBy(x => x.Id).Last().Id + 1 : 1,
                Price = dailyOrder.Price,
                Discount = dailyOrder.Discount,
                OrderDateTime = dailyOrder.OrderDateTime,
                ClientId = dailyOrder.ClientId,
                TableId = dailyOrder.TableId,
                EmployeeId = dailyOrder.EmployeeId,
            };

            _context.Orders.Add(order);

            foreach(var order_dish in dailyOrder.DailyOrderDishes)
            {
                var navigation = new OrderDish()
                {
                    Id = _context.OrderDishes.Any() ? _context.OrderDishes.OrderBy(x => x.Id).Last().Id + 1 : 1,
                    OrderId = order.Id,
                    DishId = order_dish.Id,
                    Amount = order_dish.Amount
                };

                _context.OrderDishes.Add(navigation);
                await _context.SaveChangesAsync();
            }

            await _context.SaveChangesAsync();
        }

        public async void DeleteDailyOrder(int id)
        {
            var order = _context.DailyOrders.Include(x => x.DailyOrderDishes).First(x => x.Id == id);

            _context.DailyOrders.Remove(order);

            await _context.SaveChangesAsync();
        }

        public async void UpdateDailyOrder(DailyOrder order)
        {
            _context.DailyOrders.Update(order);

            await _context.SaveChangesAsync();
        }

        public bool CheckPassword(int id, string password)
        {
            if (_context.Employees.Find(id).Password == password)
                return true;
            else
                return false;
        }

        public async void CloseWorkDay()
        {
            foreach (var dailyOrder in _context.DailyOrders)
            {
                if (!dailyOrder.IsReserved)
                    _context.DailyOrders.Remove(dailyOrder);
            }

            await _context.SaveChangesAsync();
        }

        public async void RemoveProducts(int id)
        {
            var order = GetDailyOrderById(id);

            foreach (var orderDish in order.DailyOrderDishes)
            {
                foreach(var dishProduct in orderDish.Dish.ProductDishes)
                {
                    dishProduct.Product.Amount -= dishProduct.Amount;
                    _context.Products.Update(dishProduct.Product);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}


using System;
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

        public async void CreateDailyOrder(DateTime orderTime, bool isReserved)
        {
            var order = new DailyOrder()
            {
                Id = _context.DailyOrders.Any() ? _context.DailyOrders.OrderBy(x => x.Id).Last().Id + 1 : 1,
                OrderDateTime = orderTime,
                IsReserved = isReserved,
                IsDone = false,
            };

            _context.DailyOrders.Add(order);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<DailyOrder> GetDayliOrders()
        {
            return _context.DailyOrders.Include(x => x.DailyOrderDishes);
        }

        public IEnumerable<Dish> GetDishes()
        {
            return _context.Dishes;
        }

        public DailyOrder GetDailyOrderById(int id)
        {
            return _context.DailyOrders.Find(id);
        }

        public async void SetOrderStatusDone(int id)
        {
            var dailyOrder = _context.DailyOrders.Include(x => x.DailyOrderDishes).ToList().First(x => x.Id == id);
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
            //await _context.SaveChangesAsync();

            foreach(var order_dish in dailyOrder.DailyOrderDishes)
            {
                var navigation = new OrderDish()
                {
                    Id = _context.OrderDishes.Any() ? _context.OrderDishes.OrderBy(x => x.Id).Last().Id + 1 : 1,
                    OrderId = order.Id,
                    DishId = order_dish.Id
                };

                _context.OrderDishes.Add(navigation);
                await _context.SaveChangesAsync();
            }

            await _context.SaveChangesAsync();
        }
    }
}


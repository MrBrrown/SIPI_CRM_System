using System;
using Microsoft.EntityFrameworkCore;
using SIPI_CRM_System.Models;

namespace SIPI_CRM_System.Services.OrderEditPageRep
{
	public class DataBaseOrderEditPageRepository : IOrderEditPageRepository
	{
        private readonly CRMdbContext _context;

        public DataBaseOrderEditPageRepository(CRMdbContext context)
        {
            _context = context;
        }

        public IEnumerable<Dish> GetDishes()
        {
            return _context.Dishes.Include(x => x.ProductDishes).ThenInclude(x => x.Product);
        }

        public List<string> GetDishSubCategoryList(string category)
        {
            HashSet<string> dishSubCategories = new();

            foreach (var item in _context.Dishes.Where(x => x.Category == category))
            {
                if (item.SubCategory == null)
                {
                    item.SubCategory = "Уникальные блюда";
                    _context.Dishes.Update(item);
                }
                dishSubCategories.Add(item.SubCategory);
            }

            var returnList = dishSubCategories.ToList();

            returnList.Sort();

            _context.SaveChanges();

            return returnList;
        }

        public List<string> GetDishCategoryList()
        {
            HashSet<string> dishCategories = new();

            foreach (var item in _context.Dishes)
            {
                dishCategories.Add(item.Category);
            }

            var returnList = dishCategories.ToList();

            returnList.Sort();

            return returnList;
        }

        public DailyOrder GetDailyOrder(int id)
        {
            return _context.DailyOrders
                .Include(x => x.Table)
                .Include(x => x.DailyOrderDishes)
                .ThenInclude(x => x.Dish)
                .ThenInclude(x => x.ProductDishes)
                .ThenInclude(x => x.Product)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<DailyOrderDish> GetDailyOrderDishes()
        {
            return _context.DailyOrderDishes;
        }

        public async void AddDailyOrderDish(DailyOrderDish dailyOrderDish)
        {
            _context.DailyOrderDishes.Add(dailyOrderDish);

            await _context.SaveChangesAsync();
        }

        public async void DeleteDailyOrderDish(int id)
        {
            var orderDish = _context.DailyOrderDishes.Find(id);

            _context.DailyOrderDishes.Remove(orderDish);

            await _context.SaveChangesAsync();
        }

        public async void UpdateOrderDish(int id, int amount)
        {
            var updOredrDish = _context.DailyOrderDishes.Find(id);

            updOredrDish.Amount = amount;

            _context.DailyOrderDishes.Update(updOredrDish);

            await _context.SaveChangesAsync();
        }

        public async void UpdatePrice(int id)
        {
            var order = GetDailyOrder(id);

            order.Price = 0;

            foreach (var orderDish in order.DailyOrderDishes)
            {
                order.Price += orderDish.Amount * orderDish.Dish.Price;
            }

            _context.DailyOrders.Update(order);

            await _context.SaveChangesAsync();
        }
    }
}


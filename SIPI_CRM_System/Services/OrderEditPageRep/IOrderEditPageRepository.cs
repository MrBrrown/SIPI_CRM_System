﻿using System;
using SIPI_CRM_System.Models;

namespace SIPI_CRM_System.Services.OrderEditPageRep
{
	public interface IOrderEditPageRepository
	{
        DailyOrder GetDailyOrder(int id);

        IEnumerable<Dish> GetDishes();

        IEnumerable<DailyOrderDish> GetDailyOrderDishes();

        List<string> GetDishCategoryList();

        List<string> GetDishSubCategoryList(string category);

        void DeleteDailyOrderDish(int id);

        void AddDailyOrderDish(DailyOrderDish dailyOrderDish);

        void UpdateOrderDish(int id, int amount);

        void UpdatePrice(int id);
    }
}


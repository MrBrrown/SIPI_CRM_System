using System;
using SIPI_CRM_System.Models;

namespace SIPI_CRM_System.Services.OrderEditPageRep
{
	public interface IOrderEditPageRepository
	{
        DailyOrder GetDailyOrder(int id);

        IEnumerable<Dish> GetDishes();

        List<string> GetDishCategoryList();

        List<string> GetDishSubCategoryList(string category);
    }
}


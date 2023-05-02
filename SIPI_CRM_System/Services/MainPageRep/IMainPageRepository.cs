using System;
using SIPI_CRM_System.Models;

namespace SIPI_CRM_System.Services.MainPageRep
{
	public interface IMainPageRepository
	{
		void CreateDailyOrder(DateTime orderTime, bool isReserved);

		IEnumerable<DailyOrder> GetDayliOrders();

		IEnumerable<Dish> GetDishes();

		DailyOrder GetDailyOrderById(int id);

		void SetOrderStatusDone(int id);
	}
}


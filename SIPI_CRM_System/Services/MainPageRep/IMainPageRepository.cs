using System;
using SIPI_CRM_System.Models;

namespace SIPI_CRM_System.Services.MainPageRep
{
	public interface IMainPageRepository
	{
		void CreateDailyOrder(DailyOrder order);

		IEnumerable<DailyOrder> GetDayliOrders();

		IEnumerable<Dish> GetDishes();

		IEnumerable<Table> GetTables();

		DailyOrder GetDailyOrderById(int id);

		void SetOrderStatusDone(int id);

		void DeleteDailyOrder(int id);

		void UpdateDailyOrder(DailyOrder order);
	}
}


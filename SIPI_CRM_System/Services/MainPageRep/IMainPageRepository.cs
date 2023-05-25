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

		bool CheckPassword(int id, string password);

		void CloseWorkDay();

        void SetOrderStatusDone(int id);

		void DeleteDailyOrder(int id);

		void UpdateDailyOrder(DailyOrder order);
	}
}


using System;
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
            return _context.DailyOrders;
        }
    }
}


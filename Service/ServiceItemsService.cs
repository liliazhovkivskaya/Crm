using CrmService.Interfaces;
using CrmService.Models;
using CrmService.PostgreDb;

namespace CrmService.Service
{
    public class ServiceItemsService : IServiceItemsService
    {
        private readonly AppDbContext _context;

        public ServiceItemsService(AppDbContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Получаем все услуги салона
        /// </summary>
        /// <returns>Список услуг</returns>
        public List<ServiceItem> GetServiceItems()
        {
            return _context.Services.ToList();
        }
    }
}

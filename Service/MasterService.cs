using CrmService.Interfaces;
using CrmService.Models;
using CrmService.PostgreDb;

namespace CrmService.Service
{
    public class MasterService : IMasterService
    {
        private readonly AppDbContext _context;

        public MasterService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Метод получения всех мастеров
        /// </summary>
        /// <returns>Список мастеров</returns>
        public List<Master> GetMasters()
        {
            return _context.Masters.ToList();
        }
    }
}

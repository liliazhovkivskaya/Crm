using CrmService.Interfaces;
using CrmService.Models;
using CrmService.PostgreDb;
using Microsoft.EntityFrameworkCore;

namespace CrmService.Service
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _context;
        public ClientService(AppDbContext context)
        {
            _context = context;
        }

        public List<Client> GetClients()
        {
            return _context.Clients.ToList();
        }

        public Task<Client?> FindByPhoneAsync(string phone) =>
        _context.Clients.AsNoTracking()
                   .FirstOrDefaultAsync(c => c.PhoneNumber == phone);

        public async Task<Client> CreateAsync(Client client)
        {
            // Желательно нормализовать телефон, чтобы «+7-123…» и «8123…» совпадали.
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public void DeleteClients(Client client)
        {
            _context.Clients.Remove(client);
        }

        public void UpdateClient(Client client)
        {
            var clientToDb = _context.Clients.FirstOrDefault(x=>x.Id == client.Id);

            if (clientToDb != null)
            {
                clientToDb = client;
            }
            _context.SaveChanges();
        }

        public Client FilterClients(string fullName)
        {
            var client = _context.Clients.FirstOrDefault(c => c.FullName == fullName);
            if(client != null)
            {
                return client;
            }
            else
            {
                return new Client();
            }
        }
    }
}

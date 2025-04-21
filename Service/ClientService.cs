using CrmService.Models;
using CrmService.PostgreDb;

namespace CrmService.Service
{
    public class ClientService
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

        public async Task AddClients(Client client)
        {
            try
            {
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

            
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

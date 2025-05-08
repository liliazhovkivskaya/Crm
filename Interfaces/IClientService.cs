using CrmService.Models;

namespace CrmService.Interfaces
{
    public interface IClientService
    {
        Task<Client?> FindByPhoneAsync(string phone);
        Task<Client> CreateAsync(Client client);
        public List<Client> GetClients();
    }
}

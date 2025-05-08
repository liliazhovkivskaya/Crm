using CrmService.Enums;
using CrmService.Models;

namespace CrmService.Interfaces
{
    public interface IAppointmentService
    {
        public List<Appointment> GetNewAppoiments();

        public List<Appointment> GetAppointments();

        public List<Appointment> GetAllAppointments();

        public Task AddAsync(Appointment appointment);

        public Task UpdateAppointmentStatus(int appointmentId, AppointmentStatus status);

        public Task SendTelegramNotification(long? chatId, string message);
    }
}

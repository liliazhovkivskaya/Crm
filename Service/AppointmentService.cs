using CrmService.Models;
using CrmService.PostgreDb;
using CrmService.Interfaces;
using Microsoft.EntityFrameworkCore;
using CrmService.Enums;

namespace CrmService.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly AppDbContext _context;
        public AppointmentService(AppDbContext context)
        {
            _context = context;
        }

        public List<Appointment> GetAppointments()
        {
            return _context.Appointments.Include(x=>x.ServiceItem).Include(x=>x.Master).Where(x=>x.Status == Enums.AppointmentStatus.Accepted).ToList();
        }
        
        public List<Appointment> GetAllAppointments()
        {
            return _context.Appointments.Include(x=>x.ServiceItem).Include(x=>x.Master).Include(x=>x.Client).ToList();
        }

        public async Task UpdateAppointmentStatus(int appointmentId, AppointmentStatus status)
        {
            var appointment = _context.Appointments.FirstOrDefault(x => x.Id == appointmentId);

            if (appointment != null)
            {
                appointment.Status = status;
                //await SendTelegramNotification(appointment.TelegramChatId, "Ваша запись подтверждена");
            }
            _context.SaveChanges();
        }

        public List<Appointment> GetNewAppoiments()
        {
            return _context.Appointments
                           .Include(x => x.Client)
                           .Where(x => x.Status == Enums.AppointmentStatus.Pending)
                           .ToList();
        }

        public async Task AddAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }

        public void DeleteAppointments(Appointment appointment)
        {
            _context.Appointments.Remove(appointment);
        }

        public void UpdateAppointment(Appointment appointment)
        {
            var appointmentToDb = _context.Appointments.FirstOrDefault(x => x.Id == appointment.Id);

            if (appointmentToDb != null)
            {
                appointmentToDb = appointment;
            }
            _context.SaveChanges();
        }

        public async Task SendTelegramNotification(long? chatId, string message)
        {
            var dto = new
            {
                ChatId = chatId,
                Message = message
            };

            var http = new HttpClient();
            var response = await http.PostAsJsonAsync("https://localhost:7098/api/appointmentmanagement/send", dto);

            if (!response.IsSuccessStatusCode)
            {
                // логирование/ошибка
                Console.WriteLine($"Ошибка при отправке Telegram: {response.StatusCode}");
            }
        }

    }
}

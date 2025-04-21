using CrmService.Models;
using CrmService.PostgreDb;

namespace CrmService.Service
{
    public class AppointmentService
    {
        private readonly AppDbContext _context;
        public AppointmentService(AppDbContext context)
        {
            _context = context;
        }

        public List<Appointment> GetAppointments()
        {
            return _context.Appointments.ToList();
        }
        

        public async Task AddAppointments(Appointment appointment)
        {
            try
            {
                _context.Appointments.Add(appointment);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при сохранении Appointment: " + ex.Message);
                throw; // или как-то иначе отобразить
            }
            finally
            {
                
            }
            

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
    }
}

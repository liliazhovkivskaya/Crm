using CrmService.Models;
using CrmService.PostgreDb;
using CrmService.DTO;
using CrmService.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrmService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TGBotController : ControllerBase
    {
        private readonly AppDbContext _db;

        public TGBotController(AppDbContext db) => _db = db;

        // DTO
        public record ServiceItemDto(int Id, string Name);
        public record MasterDto(int Id, string FullName);

        // ----------  справочники  ----------
        [HttpGet("serviceitems")]
        public async Task<ActionResult<IEnumerable<ServiceItemDto>>> GetServiceItems()
            => Ok(await _db.Services
                           .AsNoTracking()
                           .Select(s => new ServiceItemDto(s.Id, s.Name))
                           .ToListAsync());

        [HttpGet("masters")]
        public async Task<ActionResult<IEnumerable<MasterDto>>> GetMasters()
            => Ok(await _db.Masters
                           .AsNoTracking()
                           .Select(m => new MasterDto(m.Id, m.FullName))
                           .ToListAsync());

        // ----------  создание записи  ----------
        [HttpPost("appointments")]
        public async Task<ActionResult<AppointmentResponseDto>> CreateAppointment(
            [FromBody] AppointmentRequestDto req)
        {
            // 1.  проверяем, что услуга и мастер существуют
            var service = await _db.Services.FindAsync(req.ServiceItemId);
            if (service is null) return BadRequest("ServiceItemId not found");

            var master = await _db.Masters.FindAsync(req.MasterId);
            if (master is null) return BadRequest("MasterId not found");

            // 2.  ищем / создаём клиента
            var client = await _db.Clients
                                  .FirstOrDefaultAsync(c => c.PhoneNumber == req.PhoneNumber);

            if (client is null)
            {
                client = new Client
                {
                    FullName = req.FullName,
                    PhoneNumber = req.PhoneNumber
                };
                _db.Clients.Add(client);
                await _db.SaveChangesAsync();            // понадобится Id
            }

            // 3.  создаём запись
            var appointment = new Appointment
            {
                Client = client,
                ServiceItem = service,
                Master = master,
                Time = DateTime.SpecifyKind(req.Time, DateTimeKind.Utc),
                Status = AppointmentStatus.Pending,
                ClientId = client.Id,
                ServiceItemId = service.Id,
                MasterId = master.Id,
                TelegramChatId = req.TelegramChatId
            };

            _db.Appointments.Add(appointment);
            await _db.SaveChangesAsync();

            // 4.  ответ
            return Ok(new AppointmentResponseDto
            {
                Id = appointment.Id,
                Service = service.Name,
                Master = master.FullName,
                Time = appointment.Time,
                Status = appointment.Status.ToString(),
                ClientName = client.FullName
            });
        }
    }

}
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CrmService.Enums;

namespace CrmService.Models
{

    /// <summary>
    /// Запись клиента на услугу
    /// </summary>
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Дата и время записи
        /// </summary>
        [Required]
        public DateTime Time { get; set; }

        /// <summary>
        /// Статус записи
        /// </summary>
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        /// <summary>
        /// Внешний ключ на клиента
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Клиент, сделавший запись
        /// </summary>
        public Client? Client { get; set; } = new Client();

        /// <summary>
        /// Внешний ключ на мастера
        /// </summary>
        public int? MasterId { get; set; }

        /// <summary>
        /// Мастер
        /// </summary>
        public Master? Master { get; set; } = new();

        /// <summary>
        /// Внешний ключ на услугу
        /// </summary>
        public int? ServiceItemId { get; set; }

        /// <summary>
        /// Услуга
        /// </summary>
        public ServiceItem? ServiceItem { get; set; } = new();

        /// <summary>
        /// Telegram ChatId заявки (может отличаться от клиента),
        /// используется для уведомлений.
        /// Null, если запись создана через приложение.
        /// </summary>
        public long? TelegramChatId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmService.Models
{
    /// <summary>
    /// Клиент салона красоты.
    /// Может быть добавлен вручную или через Telegram-бота.
    /// </summary>
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// ФИО клиента (обязательно)
        /// </summary>
        [Required]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Номер телефона клиента (обязательно)
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Telegram ChatId, если клиент общался через Telegram-бота.
        /// Null, если клиент добавлен вручную через приложение.
        /// </summary>
        public long? TelegramChatId { get; set; }

        /// <summary>
        /// Telegram username клиента (например, "@username").
        /// Null, если неизвестен или клиент добавлен вручную.
        /// </summary>
        public string? TelegramUserName { get; set; }

        /// <summary>
        /// Список всех записей клиента.
        /// </summary>
        public List<Appointment> Appointments { get; set; } = new();
    }
}

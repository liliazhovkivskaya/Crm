using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmService.Models
{
    /// <summary>
    /// Таблица услуг
    /// </summary>
    public class ServiceItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Название услуги
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Цена услуги
        /// </summary>
        public decimal? Price { get; set; }

        public List<Appointment> Appointments { get; set; } = new();
    }
}

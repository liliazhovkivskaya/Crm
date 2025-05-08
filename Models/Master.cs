using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmService.Models
{
    public class Master
    {
        /// <summary>
        /// Таблица мастеров
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// ФИО Мастера
        /// </summary>
        [Required]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Номер телефона Мастера
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        
    }
}

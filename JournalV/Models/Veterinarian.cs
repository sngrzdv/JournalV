using System.ComponentModel.DataAnnotations;

namespace JournalV.Models
{
    public class Veterinarian
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "ФИО ветеринара обязательно")]
        [StringLength(100, ErrorMessage = "ФИО не должно превышать 100 символов")]
        public string FullName { get; set; } // ФИО ветеринара
        [Required(ErrorMessage = "Специализация обязательна")]
        [StringLength(100, ErrorMessage = "Специализация не должна превышать 100 символов")]
        public string Specialization { get; set; } // Специализация
        public ICollection<Visit> Visits { get; set; } // Связь с посещениями
    }
}

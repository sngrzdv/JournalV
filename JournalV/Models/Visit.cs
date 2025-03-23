using System.ComponentModel.DataAnnotations;

namespace JournalV.Models
{
    public class Visit
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Дата посещения обязательна")]
        [DataType(DataType.DateTime, ErrorMessage = "Некорректный формат даты и времени")]
        public DateTime VisitDate { get; set; } // Дата посещения

        [StringLength(500, ErrorMessage = "Заметки не должны превышать 500 символов")]
        public string Notes { get; set; } // Заметки

        // Внешние ключи
        [Required(ErrorMessage = "Питомец обязателен")]
        public int PetId { get; set; }
        [Required(ErrorMessage = "Ветеринар обязателен")]
        public int VeterinarianId { get; set; }
        [Required(ErrorMessage = "Процедура обязательна")]
        public int ProcedureId { get; set; }

        // Навигационные свойства
        public Pet Pet { get; set; }
        public Veterinarian Veterinarian { get; set; }
        public Procedure Procedure { get; set; }
    }
}

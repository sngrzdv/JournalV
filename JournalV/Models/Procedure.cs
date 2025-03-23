using System.ComponentModel.DataAnnotations;

namespace JournalV.Models
{
    public class Procedure
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Название процедуры обязательно")]
        [StringLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
        public string Name { get; set; } // Название процедуры (например, "Вакцинация")
        [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        public string Description { get; set; } // Описание процедуры
        [Required(ErrorMessage = "Стоимость обязательна")]
        [Range(0, 100000, ErrorMessage = "Стоимость должна быть в диапазоне от 0 до 100000")]
        public decimal Cost { get; set; } // Стоимость процедуры
        public ICollection<Visit> Visits { get; set; } // Связь с посещениями
    }
}

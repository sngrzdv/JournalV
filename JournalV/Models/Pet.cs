using System.ComponentModel.DataAnnotations;

namespace JournalV.Models
{
    public class Pet
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Имя питомца обязательно")]
        [StringLength(50, ErrorMessage = "Имя не должно превышать 50 символов")]
        public string Name { get; set; } // Имя питомца
        [Required(ErrorMessage = "Вид животного обязателен")]
        [StringLength(50, ErrorMessage = "Вид не должен превышать 50 символов")]
        public string Species { get; set; } // Вид животного (например, кошка, собака)
        [Required(ErrorMessage = "Дата рождения обязательна")]
        [DataType(DataType.Date, ErrorMessage = "Некорректный формат даты")]
        public DateTime DateOfBirth { get; set; } // Дата рождения
        [Required(ErrorMessage = "Владелец обязателен")]
        public int OwnerId { get; set; } // Внешний ключ на владельца
        public Owner Owner { get; set; } // Навигационное свойство
    }
}

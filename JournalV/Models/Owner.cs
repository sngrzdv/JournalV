using System.ComponentModel.DataAnnotations;

namespace JournalV.Models
{
    public class Owner
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "ФИО владельца обязательно")]
        [StringLength(100, ErrorMessage = "ФИО не должно превышать 100 символов")]
        public string FullName { get; set; } // ФИО владельца
        [Required(ErrorMessage = "Номер телефона обязателен")]
        [Phone(ErrorMessage = "Некорректный формат номера телефона")]
        public string PhoneNumber { get; set; } // Номер телефона
        [Required(ErrorMessage = "Электронная почта обязательна")]
        [EmailAddress(ErrorMessage = "Некорректный формат электронной почты")]
        public string Email { get; set; } // Электронная почта
        public ICollection<Pet> Pets { get; set; } // Связь с питомцами
    }
}

using System.ComponentModel.DataAnnotations;
using Packt.Shared;
namespace Employee.Models
{
    public class AddDivisionViewModel
    {
        [Required(ErrorMessage = "Поле Код Подразделения обязателен к заполнению!")]
        public int DivisionId { get; set; }

        [Required(ErrorMessage = "Поле Код Должности обязателен к заполнению!")]
        public string DivisionName { get; set; } = null!;
    }
}

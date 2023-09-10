using System.ComponentModel.DataAnnotations;
using Packt.Shared;
namespace Employee.Models
{
    public class AddPositionViewModel
    {
        [Required(ErrorMessage = "Поле Код Должности обязателен к заполнению!")]
        public int PositionId { get; set; }

        [Required(ErrorMessage = "Поле Наименование должности обязателено к заполнению!")]
        public string PositionName { get; set; }

        [Required(ErrorMessage = "Поле Тарифная Ставка обязателено к заполнению!")]
        public decimal TariffRate { get; set; }

        [Required(ErrorMessage = "Поле Коэффициент/Надбавка обязателено к заполнению!")]
        public decimal Coefficient { get; set; }
    }
}

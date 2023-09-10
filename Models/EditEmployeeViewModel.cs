using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee.Models
{
    public class EditEmployeeViewModel
    {
        public int StaffId { get; set; }

        [Required(ErrorMessage = "Поле 'Имя' обязательно для заполнения")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Поле 'Фамилия' обязательно для заполнения")]
        public string LastName { get; set; }
        public string MiddleName { get; set; } = null!;

        public int Age { get; set; }

        public int PositionId { get; set; }

        public string Description { get; set; }

        public int Experience { get; set; }

        public string? PhoneNumber { get; set; }

        public int? PassportData { get; set; }
        public int? DivisionId { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Hiring Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? HiringDate { get; set; }
    }
}

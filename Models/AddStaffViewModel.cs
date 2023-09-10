using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Packt.Shared;

namespace Employee.Models
{
    public class AddStaffViewModel
    {
        [Required(ErrorMessage = "Last Name is required.")]
        public int StaffId { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name must not exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name must not exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Middle Name is required.")]
        [StringLength(50, ErrorMessage = "Middle Name must not exceed 50 characters.")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(18, 99, ErrorMessage = "Age must be between 18 and 99.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Position ID is required.")]
        public int PositionId { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Experience is required.")]
        public int Experience { get; set; }

        [Required(ErrorMessage = "Photo is required")]
        public IFormFile PhotoFile { get; set; }

        [StringLength(20, ErrorMessage = "Phone Number must not exceed 20 characters.")]
        public string PhoneNumber { get; set; }

        public int? PassportData { get; set; }

        public int? DivisionId { get; set; }

        [Required(ErrorMessage = "Birth Date is required.")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Document")]
        public IFormFile DocumentFile { get; set; }

        [Required(ErrorMessage = "Hiring date is required")]
        [DataType(DataType.Date)]
        public DateTime HiringDate { get; set; }

    }
}

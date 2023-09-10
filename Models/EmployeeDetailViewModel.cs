
using Packt.Shared;
using System.ComponentModel.DataAnnotations;

namespace Employee.Models
{
    public class EmployeeDetailViewModel
    {       

        public Staff? Staff { get; set; }
        public Post? Post { get; set; }
        public Division? Division { get; set; }
        public int DaysSinceHiring { get; set; }


    }

}

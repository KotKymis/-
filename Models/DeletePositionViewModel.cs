using Microsoft.AspNetCore.Mvc.Rendering;
using Packt.Shared;

namespace Employee.Models
{
    public class DeletePositionViewModel
    {
        public Staff? Staff { get; set; }
        public Post? Post { get; set; }
        public Division? Division { get; set; }
    }
}

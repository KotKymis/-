using Packt.Shared;

namespace Employee.Models
{
    public record HomeIndexViewModel
    (
        int EmployeesCount,
        IList<Staff> Staffs,
        IList<Post> Posts,
        IList<Division> Divisions
    );
}

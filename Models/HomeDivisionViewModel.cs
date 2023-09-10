using Packt.Shared;
namespace Employee.Models
{
    public record HomeDivisionViewModel
    (
        IList<Staff> Staffs,
        IList<Post> Posts,
        IList<Division> Divisions,
        int EmployeesCount

     );
}

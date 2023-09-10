using Packt.Shared;
namespace Employee.Models
{
    public record HomePrivacyViewModel 
    (
        IList<Staff> Staffs,
       IList<Post> Posts 

    );
        
    
}

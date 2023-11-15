using Microsoft.EntityFrameworkCore;

namespace CRUS_Emp.Models
{
    public class Employees
    {
       
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}

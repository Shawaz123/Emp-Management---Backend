using Microsoft.EntityFrameworkCore;

namespace CRUS_Emp.Models
{
    public class EmpContext:DbContext
    {
        public EmpContext(DbContextOptions<EmpContext>options):base(options)
        {
            
        }

       public DbSet<Employees> DbSet { get; set; }

    }
}

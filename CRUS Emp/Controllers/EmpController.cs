using CRUS_Emp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Web.Http.Cors;

namespace CRUS_Emp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    

    public class EmpController : ControllerBase
    {
        private readonly EmpContext? _empContext;


        public EmpController(EmpContext dbContext)
        {
            _empContext = dbContext;
        }

        //Create a CRUD (Create/Read/Update/Delete) operations below.
        // Use try catch block
        // Display record on the basis of Id.
        [HttpGet]
        

        public async Task<ActionResult<IEnumerable<Employees>>> GetEmployees()
        {
            if (_empContext?.DbSet == null)
            {
                return NotFound();
            }

            return await _empContext.DbSet.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employees>> GetEmployees(int id)
        {
            if (_empContext?.DbSet == null)
            {
                return NotFound();
            }

            var Employees = await _empContext.DbSet.FindAsync(id);
            if (Employees == null)
            {
                return NotFound();
            }
            return Employees;
        }


        [HttpPost]
        public async Task<ActionResult<Employees>> PostEmployees(Employees employees)
        {
            _empContext?.DbSet.Add(employees);
            await _empContext?.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployees), new { id = employees.Id}, employees);

        }

        [HttpPut]

        public async Task<IActionResult>PutEmployees(int id,Employees employees)
        {
            if (id != employees.Id)
            {
                return BadRequest();
            }

            _empContext.Entry(employees).State = EntityState.Modified;

            try
            {
                await _empContext.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeesAvailable(id))
                {
                    return NotFound();
                }

                else
                {
                    throw;
                }

            } return Ok();

            
        }

        private bool EmployeesAvailable(int id)
        {
            return (_empContext.DbSet?.Any(x => x.Id == id)).GetValueOrDefault();
        }

        [HttpDelete]
        public async Task<IActionResult>DeleteEmployee(int id)
        {
            if (_empContext.DbSet == null)
            {
                return NotFound();
            }

            var emp = await _empContext.DbSet.FindAsync(id);
            {
                if(emp==null) {
                return NotFound();}
            }
            _empContext.DbSet.Remove(emp);
            await _empContext.SaveChangesAsync();
            return Ok();
        }







    }
}
 
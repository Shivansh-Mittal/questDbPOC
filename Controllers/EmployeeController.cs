using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QuestDbPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController
    {
        [HttpGet("[action]")]
        public IActionResult GetUsers()
        {
            var emp = db.Employees.ToList();
            return Ok(emp);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            Employee emp = db.Employees.SingleOrDefault(x => x.EmployeeId == id);
            return Ok(emp);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Employee model)
        {
            if (model.EmployeeId == 0)
            {
                await db.Employees.AddAsync(model);
            }
            else
            {
                db.Employees.Update(model);
            }
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Employee> Delete(int id)
        {
            var res = db.Employees.SingleOrDefault(x => x.EmployeeId == id);
            if (res == null)
            {
                return NotFound();
            }

            db.Employees.Remove(res);
            db.SaveChanges();

            return res;
        }
    }
}

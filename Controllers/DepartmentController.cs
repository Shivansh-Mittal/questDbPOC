using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QuestDbPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : BaseController
    {
        [HttpGet("[action]")]
        public IActionResult GetDepartments()
        {
            var depart = db.Departments.ToList();
            return Ok(depart);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Department model)
        {
            if (model.DepartmentId == 0)
            {
                await db.Departments.AddAsync(model);
            }
            else
            {
                db.Departments.Update(model);
            }
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Department> Delete(int id)
        {
            var res = db.Departments.SingleOrDefault(x => x.DepartmentId == id);
            if (res == null)
            {
                return NotFound();
            }
            db.Departments.Remove(res);
            db.SaveChanges();
            return res;
        }
    }
}

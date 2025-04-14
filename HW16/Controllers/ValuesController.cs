using HW16.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HW16.Controllers
{
    [ApiController]
    [Route("Teachers")]
    public class TeachersController : ControllerBase
    {
        private readonly TeacherContext _context;

        public TeachersController(TeacherContext context)
        {
            _context = context;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            return await _context.Teachers.ToListAsync();
        }

        // GET: api/Teachers/3
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            var Teacher = await _context.Teachers.SingleOrDefaultAsync(m => m.Id == id);
            if (Teacher == null)
            {
                return NotFound();
            }
            return new ObjectResult(Teacher);
        }

        // PUT: api/Teachers
        [HttpPut]
        public async Task<ActionResult<Teacher>> PutStudent(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_context.Teachers.Any(e => e.Id == teacher.Id))
            {
                return NotFound();
            }

            _context.Update(teacher);
            await _context.SaveChangesAsync();
            return Ok(teacher);
        }

        // POST: api/Teachers
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return Ok(teacher);
        }

        // DELETE: api/Teachers/3
        [HttpDelete("{id}")]
        public async Task<ActionResult<Teacher>> DeleteTeacher(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teacher = await _context.Teachers.SingleOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return Ok(teacher);
        }
    }
}
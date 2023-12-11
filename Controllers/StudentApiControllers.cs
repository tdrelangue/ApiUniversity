using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiUniversity.Models;
using ApiUniversity.Data;
namespace ApiUniversity.Controllers;


[Route("api/[controller]")]
[ApiController]
public class StudentApiController : ControllerBase
{
   private readonly UniversityContext _context;


   public StudentApiController(UniversityContext context)
   {
       _context = context;
   }

    // le reste du code sera ici
    // GET: api/StudentApi
    [HttpGet]
   public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
   { 
        var students = _context.Students.OrderBy(s=>s.LastName);
        var studentsDto = students.Select(x => new StudentDTO(x)); 
        // var studentEnrolled = student.Include(s=>s.Enrollments);
        return await studentsDto.ToListAsync();
   }

   // GET: api/StudentApi/5
   [HttpGet("{id}")]
   public async Task<ActionResult<StudentDTO>> GetStudent(int id)
   {
        var student =  await _context.Students.FirstOrDefaultAsync(); 
                                            //.Where(s => s.Id == id);
        if (student == null)
        {
            return NotFound();
        }

        // var studentDto = student.Select(x => new StudentDTO(student));

        return new StudentDTO(student);
   }

    //POST: api/StudentApi
    [HttpPost]
    public async Task<ActionResult<Student>> PostStudent(StudentDTO studentDto)
    {
        var student = new Student(){
            FirstName = studentDto.FirstName,
            LastName = studentDto.LastName,
            EnrollmentDate = studentDto.EnrollmentDate,
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return CreatedAtAction("PostStudent", new { id = student.Id }, student);
    }

    //how the fuck do you delete this with a DTO
    // DELETE: api/StudentApi/5
   [HttpDelete("{id}")]
   public async Task<ActionResult<Student>> DeleteStudent(int id)
   {
       var student = await _context.Students.FindAsync(id);
       if (student == null)
       {
           return NotFound();
       }


       _context.Students.Remove(student);
       await _context.SaveChangesAsync();


       return student;  
   }


   // PUT: api/StudentApi/5
   [HttpPut("{id}")]
   public async Task<IActionResult> PutStudent(int id, StudentDTO studentDto)
   {

        if (id != studentDto.Id)
        {
            return BadRequest();
        }

        Student student = new(studentDto);


        _context.Entry(student).State = EntityState.Modified;


        try
        {
           await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudentExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }


        return NoContent();
    }
   private bool StudentExists(int id)
   {
       return _context.Students.Any(e => e.Id == id);
   }


}
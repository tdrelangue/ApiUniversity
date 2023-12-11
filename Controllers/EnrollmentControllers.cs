using ApiUniversity.Data;
using ApiUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiUniversity.Controllers;

[ApiController]
[Route("api/enrollmentApi")]
public class EnrollmentController : ControllerBase
{
    private readonly UniversityContext _context;

    public EnrollmentController(UniversityContext context)
    {
        _context = context;
    }


    [HttpGet("id")]
    public async Task<ActionResult<DetailedEnrollmentDTO>> GetEnrollment(int id)
    {
        var enrollment = await _context.Enrollments
                                       .Include(x => x.Student)
                                       .Include(x => x.Course)
                                       .SingleOrDefaultAsync(s => s.Id == id);
        
        if (enrollment == null)
        {
            return NotFound();
        }

        //var enrollmentDto = enrollment.Select(x => new DetailedEnrollmentDTO(x));

        return new DetailedEnrollmentDTO(enrollment);
    }

    // POST: api/EnrollmentApi
    [HttpPost]
    public async Task<ActionResult<DetailedEnrollmentDTO>> CreateEnrollment(
        EnrollmentDTO enrollmentDTO
    )
    {
        Enrollment enrollment = new Enrollment(enrollmentDTO);
        var student = await _context.Students.SingleOrDefaultAsync(
            t => t.Id == enrollmentDTO.StudentId
        );
        var course = await _context.Courses.SingleOrDefaultAsync(
            t => t.Id == enrollmentDTO.CourseId
        );

        if ((course == null) || (student == null))
        {
            return NotFound();
        }

        enrollment.Student = student;
        enrollment.Course = course;
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            System.Console.WriteLine("prout");
            return BadRequest();
        }

        return CreatedAtAction(
            nameof(GetEnrollment),
            new { id = enrollment.Id },
            new DetailedEnrollmentDTO(enrollment)
        );
    }
}
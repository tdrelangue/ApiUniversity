namespace ApiUniversity.Models;

public class DetailedEnrollmentDTO
{
    public int Id { get; set; }
    public Grade Grade { get; set; }

    public StudentDTO StudentDto { get; set; }
    public CourseDTO CourseDto { get; set; } = null!;

    public DetailedEnrollmentDTO(Enrollment enrollment) 
    { 
        Id = enrollment.Id;
        Grade = enrollment.Grade;
        StudentDto = new StudentDTO(enrollment.Student);
        CourseDto = new CourseDTO(enrollment.Course);
    }
}
namespace ApiUniversity.Models;

public class EnrollmentDTO
{
    public int Id { get; set; }
    public Grade Grade { get; set; }

    public int StudentId { get; set; }
    public int CourseId { get; set; } 

    public EnrollmentDTO(Enrollment enrollment) 
    { 
        Id = enrollment.Id;
        Grade = enrollment.Grade;
        StudentId = enrollment.StudentId;
        CourseId = enrollment.Course.Id;
    }
}
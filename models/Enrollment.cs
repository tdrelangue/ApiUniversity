namespace ApiUniversity.Models;

public class Enrollment
{
    public int Id { get; set; }
    public Grade Grade { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public Student Student { get; set; } = null!;
    public Course Course { get; set; } = null!;

    public Enrollment() { }

    public Enrollment(DetailedEnrollmentDTO enrollmentDTO)
    {
        Grade = enrollmentDTO.Grade;
        Student = new Student (enrollmentDTO.StudentDto);
        Course = new Course (enrollmentDTO.CourseDto);
    }

    public Enrollment(EnrollmentDTO enrollmentDTO)
    {
        Grade = enrollmentDTO.Grade;
        StudentId = enrollmentDTO.StudentId;
        CourseId = enrollmentDTO.CourseId;
    }
}
namespace DapperApi.Models;

public class StudentWithCourses
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string? Email { get; set; }
    public List<Course> Courses { get; set; } = new();
}

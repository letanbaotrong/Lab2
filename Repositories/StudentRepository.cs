using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using DapperApi.Models;

namespace DapperApi.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly string _connStr;

    public StudentRepository(IConfiguration config)
    {
        _connStr = config.GetConnectionString("DefaultConnection")!;
    }

    private IDbConnection NewConnection()
        => new SqlConnection(_connStr);

    public IEnumerable<Student> GetAll()
    {
        const string sql = "SELECT Id, Name, Age, Email FROM Students ORDER BY Id";

        using var db = NewConnection();
        return db.Query<Student>(sql);
    }

    public Student? GetById(int id)
    {
        const string sql = "SELECT Id, Name, Age, Email FROM Students WHERE Id = @Id";

        using var db = NewConnection();
        return db.QuerySingleOrDefault<Student>(sql, new { Id = id });
    }

    public IEnumerable<Student> SearchByName(string name)
    {
        const string sql = @"
            SELECT Id, Name, Age, Email
            FROM Students
            WHERE Name LIKE @Keyword
            ORDER BY Id";

        using var db = NewConnection();
        return db.Query<Student>(sql, new { Keyword = $"%{name}%" });
    }

    public void Create(Student student)
    {
        const string sql = @"
            INSERT INTO Students (Name, Age, Email)
            VALUES (@Name, @Age, @Email);
            SELECT CAST(SCOPE_IDENTITY() AS int);";

        using var db = NewConnection();
        student.Id = db.ExecuteScalar<int>(sql, student);
    }

    public bool Update(Student student)
    {
        const string sql = @"
            UPDATE Students
            SET Name = @Name,
                Age = @Age,
                Email = @Email
            WHERE Id = @Id";

        using var db = NewConnection();
        var affectedRows = db.Execute(sql, student);
        return affectedRows > 0;
    }

    public bool Delete(int id)
    {
        const string sql = "DELETE FROM Students WHERE Id = @Id";

        using var db = NewConnection();
        var affectedRows = db.Execute(sql, new { Id = id });
        return affectedRows > 0;
    }

    public IEnumerable<StudentWithCourses> GetAllWithCourses()
    {
        const string sql = @"
            SELECT s.Id, s.Name, s.Age, s.Email,
                   c.Id, c.CourseName
            FROM Students s
            JOIN StudentCourses sc ON s.Id = sc.StudentId
            JOIN Courses c ON sc.CourseId = c.Id
            ORDER BY s.Id, c.Id";

        using var db = NewConnection();
        var dict = new Dictionary<int, StudentWithCourses>();

        db.Query<StudentWithCourses, Course, StudentWithCourses>(
            sql,
            (student, course) =>
            {
                if (!dict.TryGetValue(student.Id, out var existingStudent))
                {
                    existingStudent = student;
                    dict[student.Id] = existingStudent;
                }

                existingStudent.Courses.Add(course);
                return existingStudent;
            },
            splitOn: "Id");

        return dict.Values;
    }
}

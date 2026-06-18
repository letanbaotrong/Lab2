using DapperApi.Models;

namespace DapperApi.Repositories;

public interface IStudentRepository
{
    IEnumerable<Student> GetAll();
    Student? GetById(int id);
    IEnumerable<Student> SearchByName(string name);
    void Create(Student student);
    bool Update(Student student);
    bool Delete(int id);
    IEnumerable<StudentWithCourses> GetAllWithCourses();
}

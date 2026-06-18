using Microsoft.AspNetCore.Mvc;
using DapperApi.Models;
using DapperApi.Repositories;

namespace DapperApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentRepository _repo;

    public StudentsController(IStudentRepository repo)
    {
        _repo = repo;
    }

    // GET /api/students
    [HttpGet]
    public IActionResult GetAll()
        => Ok(_repo.GetAll());

    // GET /api/students/1
    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        var student = _repo.GetById(id);
        return student is null ? NotFound() : Ok(student);
    }

    // GET /api/students/search?name=An
    [HttpGet("search")]
    public IActionResult Search([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest("Name keyword is required.");
        }

        return Ok(_repo.SearchByName(name));
    }

    // GET /api/students/courses
    [HttpGet("courses")]
    public IActionResult GetAllWithCourses()
        => Ok(_repo.GetAllWithCourses());

    // POST /api/students
    [HttpPost]
    public IActionResult Create([FromBody] Student student)
    {
        _repo.Create(student);
        return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
    }

    // PUT /api/students
    [HttpPut]
    public IActionResult Update([FromBody] Student student)
    {
        var updated = _repo.Update(student);
        return updated ? NoContent() : NotFound();
    }

    // DELETE /api/students/1
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var deleted = _repo.Delete(id);
        return deleted ? NoContent() : NotFound();
    }
}

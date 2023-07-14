using Microsoft.AspNetCore.Mvc;
using Sipay.RestfulApi.Controllers.Models;
using System.Collections.Generic;

namespace Sipay.RestfulApi.Controllers
{  
	[ApiController]
	[Route("Student/api/[controller]")]
	
	public class StudentController :ControllerBase
	{
		// Fake bir öğrenci listesi oluştur 5 kayıt içeren
		// İd isim soyisim yaş ve depeartment


		private static List<Student> students = new List<Student>()
	{
		new Student { Id = 1, Name = "Ali", LastName = "Yılmaz", Age = 22, Department = "Bilgisayar Mühendisliği" },
		new Student { Id = 2, Name = "Ayşe", LastName = "Kaya", Age = 23, Department = "Yazılım Mühendisliği" },
		new Student { Id = 3, Name = "Ahmet", LastName = "Demir", Age = 24, Department = "Endüstri Mühendisliği" },
		new Student { Id = 4, Name = "Selen", LastName = "Yıldırım", Age = 26, Department = "İnşaat Mühendisliği" },
		new Student { Id = 5, Name = "Zeynep", LastName = "Aydın", Age = 27, Department = "Yönetim Bilişim Sistemleri" }
	    };

		[HttpGet]
		public IActionResult GetAllStudents()
		{
			return Ok(students);
		}

		[HttpGet("{id}")]
		public IActionResult GetStudentById(int id)
		{
			var student = students.FirstOrDefault(s => s.Id == id);
			if (student == null)
			{
				return NotFound();
			}
			return Ok(student);
		}

		[HttpPost]
		public IActionResult CreateStudent([FromBody] Student student)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			student.Id = students.Max(s => s.Id) + 1;
			students.Add(student);

			return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
		}
		[HttpPut("{id}")]
		public IActionResult UpdateStudent(int id, [FromBody] Student updatedStudent)
		{
			var student = students.FirstOrDefault(s => s.Id == id);
			if (student == null)
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			student.Name = updatedStudent.Name;
			student.LastName = updatedStudent.LastName;
			student.Age = updatedStudent.Age;
			student.Department = updatedStudent.Department;

			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteStudent(int id)
		{
			var student = students.FirstOrDefault(s => s.Id == id);
			if (student == null)
			{
				return NotFound();
			}

			students.Remove(student);
			return NoContent();
		}

	}
}

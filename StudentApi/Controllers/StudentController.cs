using Laborator_19___Exercitiu.Models;
using Laborator_19___Exercitiu1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApi.DTOs;
using StudentApi.Extensions;
using System.Data;

namespace StudentApi.Controllers
{
    [Route("API/[controller]")]
    [ApiController]

    public class StudentController : ControllerBase
    {
        /// <summary>
        /// Finding all the students.
        /// </summary>
        /// <returns>All students</returns>
        [HttpGet("All Students")]
        public IEnumerable<Student> Get()
        {
            using var ctx = new StudentDbContext();
            return ctx.Students.ToList();
        }

        /// <summary>
        /// Finds all the students.
        /// </summary>
        /// <returns>All students</returns>
        [HttpGet("All Students DTO")]
        public IEnumerable<GetAllStudents> GetAllStudents()
        {
            using var ctx = new StudentDbContext();
            return ctx.Students.Select(p => p.GetStudentDto()).ToList();
        }

        /// <summary>
        /// Gets students by their ids.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Student specified by Id given</returns>
        [HttpGet("Get Student By Id")]
        public Student Get([FromRoute] int id)
        {
            using var ctx = new StudentDbContext();
            return ctx.Students.Where(p => p.StudentId == id).First();
        }

        /// <summary>
        /// Gets students by their ids.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Student specified by Id given</returns>
        [HttpGet("Get Student by Id DTO")]
        public IEnumerable<GetStudentById> GetStudentById([FromBody] int id)
        {
            using var ctx = new StudentDbContext();
            return ctx.Students.Where(p => p.StudentId == id).Select(p => p.GetStudentsById()).ToList();
        }

        /// <summary>
        /// Adding a student.
        /// </summary>
        /// <param name="addStudent"></param>
        /// <returns>New Created Student</returns>
        [HttpPost("Add Student DTO")]
        public Student AddStudent([FromBody] AddStudent addStudent)
        {
            return DataAccessLayer.Instance.AddStudents(addStudent.FirstName, addStudent.LastName, addStudent.Age, addStudent.Address, addStudent.StudentId);
        }

        /// <summary>
        /// Deletes a student.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("Delete Student")]
        public void DeleteStudent([FromRoute] int id)
        {
            using var ctx = new StudentDbContext();
            var student = ctx.Students.Where(p => p.StudentId == id).FirstOrDefault();
            if (student != null)
            {
                ctx.Students.Remove(student);
            }
            ctx.SaveChanges();
        }

        /// <summary>
        /// Deletes a student.
        /// </summary>
        /// <param name="deleteStudent"></param>
        /// <returns>Removed Student</returns>
        [HttpDelete("Delete Student DTO")]
        public Student DeleteStudent([FromBody] DeleteStudent deleteStudent)
        {
            return DataAccessLayer.Instance.DeleteStudents(deleteStudent.FirstName, deleteStudent.LastName, deleteStudent.Age, deleteStudent.Address, deleteStudent.StudentId);
        }

        /// <summary>
        /// Modifies the student's data.
        /// </summary>
        /// <param name="modifyStudent"></param>
        /// <returns>Modified student data</returns>
        [HttpPut("Modify Student Data DTO")]
        public Student ModifyStudentDataDTO([FromBody] ModifyStudent modifyStudent)
        {
            return DataAccessLayer.Instance.ModifyStudentsData(modifyStudent.FirstName, modifyStudent.LastName, modifyStudent.Age, modifyStudent.Address, modifyStudent.StudentId);
        }

        /// <summary>
        /// Modifies a student's address.
        /// </summary>
        /// <param name="addressId"></param>
        /// <param name="city"></param>
        /// <param name="street"></param>
        /// <param name="number"></param>
        /// <exception cref="DuplicateNameException"></exception>
        [HttpPut("Modify Student Address")]
        public void ModifyStudentAddress([FromRoute] int addressId, [FromBody] string city, string street, int number)
        {
            using var ctx = new StudentDbContext();

            var studentAddress = ctx.Addresses.Where(ctx => ctx.AddressId == addressId).FirstOrDefault();

            if (studentAddress == null)
            {
                studentAddress.City = city;
                studentAddress.Street = street;
                studentAddress.Number = number;

                ctx.SaveChanges();
            }
            else
            {
                throw new DuplicateNameException($"Adress {addressId} already exists!!!");
            }
        }

        /// <summary>
        /// Modify student's address. DTO Style
        /// </summary>
        /// <param name="modifyStudentAddress"></param>
        /// <returns>Modified student data.</returns>
        [HttpPut("Modify Student Address DTO")]
        public Address ModifyStudentAddress([FromBody] ModifyStudentAddress modifyStudentAddress)
        {
            return DataAccessLayer.Instance.ModifyStudentAddress(modifyStudentAddress.AddressId, modifyStudentAddress.City, modifyStudentAddress.Street, modifyStudentAddress.Number);
        }
        /// <summary>
        /// Delete student with address.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="addressId"></param>
        [HttpDelete("Delete Student With Address")]
        public void DeleteStudent([FromRoute] int id, [FromRoute] int addressId)
        {
            bool addressRemoved = false;
            var ctx = new StudentDbContext();

            var student = ctx.Students.Where(p => p.StudentId == id).FirstOrDefault();
            var studentAddress = ctx.Addresses.Where(p => p.AddressId == addressId).FirstOrDefault();

            if (student != null)
            {
                ctx.Students.Remove(student);
                ctx.Addresses.Remove(studentAddress);
                addressRemoved = true;
                Console.WriteLine($"Did address get removed for student: {addressRemoved}");
            }
            ctx.SaveChanges();
        }
        /// <summary>
        /// Delete student with address.
        /// </summary>
        /// <param name="deleteStudentWithAddress"></param>
        [HttpDelete("Delete Student With Address DTO")]
        public void DeleteStudent([FromBody] DeleteStudentWithAddress deleteStudentWithAddress)
        {
            DataAccessLayer.Instance.DeleteStudentWithAddress(deleteStudentWithAddress.StudentId, deleteStudentWithAddress.AddressId);
        }
    }
}

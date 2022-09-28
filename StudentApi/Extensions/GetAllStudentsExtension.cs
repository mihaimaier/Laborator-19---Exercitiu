using Laborator_19___Exercitiu.Models;
using Microsoft.AspNetCore.SignalR;
using StudentApi.DTOs;

namespace StudentApi.Extensions
{
    public static class GetAllStudentsExtension
    {
        public static GetAllStudents GetStudentDto(this Student student)
        {
            return
                new GetAllStudents
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Age = student.Age,
                    Address = student.Address,
                    StudentId = student.StudentId
                };
        }
    }
}

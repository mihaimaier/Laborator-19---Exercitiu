using Laborator_19___Exercitiu.Models;
using StudentApi.DTOs;

namespace StudentApi.Extensions
{
    public static class GetStudentByIdExtension
    {
        public static GetStudentById GetStudentsById(this Student student)
        {
            return
            new GetStudentById { 
                FirstName = student.FirstName, 
                LastName = student.LastName, 
                Age = student.Age, 
                Address = student.Address };
        }
    }
}

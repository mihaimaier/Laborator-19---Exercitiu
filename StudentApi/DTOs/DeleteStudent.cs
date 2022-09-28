using Laborator_19___Exercitiu.Models;

namespace StudentApi.DTOs
{
    public class DeleteStudent
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Address Address { get; set; }
        public int StudentId { get; set; }
    }
}

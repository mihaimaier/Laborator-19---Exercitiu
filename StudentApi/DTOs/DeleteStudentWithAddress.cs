using Laborator_19___Exercitiu.Models;

namespace StudentApi.DTOs
{
    public class DeleteStudentWithAddress
    {
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }
    }
}

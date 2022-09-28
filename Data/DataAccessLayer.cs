using Data.Exceptions;
using Laborator_19___Exercitiu.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Loader;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Laborator_19___Exercitiu1
{
    public class DataAccessLayer
    {
        private static DataAccessLayer instance = null;

        public static DataAccessLayer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataAccessLayer();
                }
                return instance;
            }
        }
        private DataAccessLayer()
        {
        }
        // Adaugare Student
        public Student AddStudents(string firstName, string lastName, int age, Address address, int studentId)
        {
            using var ctx = new StudentDbContext();

            if (!ctx.Students.Any(p => p.StudentId == studentId))
            {
                throw new NotFoundException($"Studentul {studentId} nu exita!!!");
            }
            Console.Clear();
            var newStudent = new Student { FirstName = firstName, LastName = lastName, Age = age, Address = address, StudentId = studentId };
            ctx.Students.Add(newStudent);
            ctx.SaveChanges();
            return newStudent;
        }
        // Stergere Student
        public Student DeleteStudents(string firstName, string lastName, int age, Address address, int studentId)
        {
            using var ctx = new StudentDbContext();

            if (!ctx.Students.Any(p => p.StudentId == studentId))
            {
                throw new NotFoundException($"Studentul {studentId} nu exita!!!");
            }
            Console.Clear();
            var deleteStudent = new Student { FirstName = firstName, LastName = lastName, Age = age, Address = address, StudentId = studentId };
            ctx.Students.Remove(deleteStudent);
            ctx.SaveChanges();
            return deleteStudent;
        }
        //Modify Student Data
        public Student ModifyStudentsData(string firstName, string lastName, int age, Address address, int id)
        {
            using var ctx = new StudentDbContext();
            var student = ctx.Students.Where(p => p.StudentId == id).FirstOrDefault();
            
            student.FirstName = firstName;
            student.LastName = lastName;
            student.Age = age;
            student.Address = address;

            ctx.SaveChanges();
            return student;
        }
        //Modify Student Data Address
        public Address ModifyStudentAddress(int addressId, string city, string street, int number)
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
            return studentAddress;
        }
        // Delete Student With Address
        public void DeleteStudentWithAddress(int id, int addressId)
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
    }
}

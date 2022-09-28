using Laborator_19___Exercitiu.Models;


ResetDb();

static void ResetDb()
{
    using var ctx = new StudentDbContext();

    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();

    var student1 = new Student
    {
        FirstName = "Mihai",
        LastName = "Popescu",
        Age = 32,
        Address = new Address { City = "Iasi", Number = 3, Street = "STR. BARIŢIU GEORGE" }
    };
    var student2 = new Student
    {
        FirstName = "Andrei",
        LastName = "Vlad",
        Age = 21,
        Address = new Address { City = "Vrancea", Number = 2, Street = "Strada Smardan" }
    };
    var student3 = new Student
    {
        FirstName = "Constantin",
        LastName = "Maier",
        Age = 28,
        Address = new Address { City = "Cluj", Number = 44, Street = "Matei Corvin" }
    };
    var student4 = new Student
    {
        FirstName = "Ana",
        LastName = "Miches",
        Age = 44,
        Address = new Address { City = "Bucuresti", Number = 7, Street = "ŞOS. VITAN BÂRZEŞTI" }
    };
    var student5 = new Student
    {
        FirstName = "Ana",
        LastName = "Miches",
        Age = 33,
        Address = new Address { City = "Galati", Number = 2, Street = "STR. STRUNGARILOR" }
    };
    ctx.Students.Add(student1);
    ctx.Students.Add(student2);
    ctx.Students.Add(student3);
    ctx.Students.Add(student4);
    ctx.Students.Add(student5);
    ctx.SaveChanges();
}





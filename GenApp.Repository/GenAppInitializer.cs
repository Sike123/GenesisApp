using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GenApp.Repository
{
    public class GenAppInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<GenAppContext>
    {
        protected override void Seed(GenAppContext context)
        {

            //  This code doesnt work so its done in configuration.cs
            /*
            var books = new List<Book>
            {
                new Book() {Id = Guid.NewGuid(), Isbn = "TestIsbn1", Name = "Dan Brown",Publisher = "Pawan Publishing",Edition = "1st Edition"},
                new Book() {Id = Guid.NewGuid(), Isbn = "TestIsbn2", Name = "Lord of the Rings",Publisher = "Soni Publishing",Edition = "2nd Edition"},
                new Book() {Id = Guid.NewGuid(), Isbn = "TestIsbn3", Name = "Harry Potter",Publisher = "Gurung Publishing",Edition = "3rd Edition"},
            };
            books.ForEach(x => context.Books.Add(x));
            context.SaveChanges();

            var users = new List<User>
            {
                new User() {Id=Guid.NewGuid(),Name="Pawan",Age=21 },
                new User() {Id=Guid.NewGuid(),Name="James",Age=44},
                new User() {Id=Guid.NewGuid(),Name="Sam",Age=33}

            };
            users.ForEach(x=>context.Users.Add(x));
            context.SaveChanges();
            */
        }


    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace GenApp.Repository.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<GenAppContext>
    {
        public Configuration()
        {
            //DbMigrationsConfiguration.AutomaticMigrationsEnabled  is set to false 
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GenAppContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var books = new List<Book>
            {
                 new Book {Id = Guid.NewGuid(), Isbn = "TestIsbn1", Name = "Dan Brown",Publisher = "Pawan Publishing",Edition = "1st Edition"},
                new Book {Id = Guid.NewGuid(), Isbn = "TestIsbn2", Name = "Lord of the Rings",Publisher = "Soni Publishing",Edition = "2nd Edition"},
                new Book {Id = Guid.NewGuid(), Isbn = "TestIsbn3", Name = "Harry Potter",Publisher = "Gurung Publishing",Edition = "3rd Edition"}
            };
            books.ForEach(x => context.Books.AddOrUpdate(x));
            context.SaveChanges();

            var users = new List<User>
            {
                new User {Id=Guid.NewGuid(),Name="test1@hotmail.com",Age=21 },
                new User {Id=Guid.NewGuid(),Name="test2@hotmail.com",Age=44},
                new User {Id=Guid.NewGuid(),Name="test3@hotmail.com",Age=33}

            };
            users.ForEach(x => context.Users.AddOrUpdate(x));
            context.SaveChanges();
            
                       

        }
    }
}

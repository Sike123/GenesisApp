using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace GenApp.Dump
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<Person> persons = new List<Person>()
            {
                new Person() {Name = "Bob",DeparmentId= 1 },
                new Person() {Name = "Sam",DeparmentId = 1 },
                new Person() {Name = "John",DeparmentId =  1 },
                new Person() {Name = "Pawan",DeparmentId =2 },
                new Person() {Name="Ramesh",DeparmentId=1 },
                new Person() {Name="Sudhir" }
            };

            IList<Department> departments = new List<Department>()
            {
                new Department() {Id=2,Name = "HR"},
            new Department() {Id=1,Name="IT"},
            
            };


            var result = from person in persons
                join department in departments 
                    on person.DeparmentId equals department.Id 
                select new
                {
                    Name = person.Name,
                    Department=department.Name??"No Deparment"
                };

            Console.WriteLine("-----------Person and thier departments-------------");
            foreach (var person in result)
            {
                Console.WriteLine(person.Name + " :" + person.Department);
            }

          


            Console.WriteLine("---------------Person grouped by departments -------------");
            var output = from p in persons
                join d in departments
                    on p.DeparmentId equals d.Id into dGroup
                from d in dGroup.DefaultIfEmpty()
                select new
                {
                    Name = p.Name,
                    Department = d.Name??"No Department"

                };


            foreach (var temp in output)
            {
                Console.WriteLine(temp.Name+": "+temp.Department);
            }

            Console.ReadLine();


            




        }


    }

    internal class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DeparmentId { get; set; }
        public virtual Department Department { get; set; }

    }

    internal class Department
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public Person Person { get; set; }

    }
}

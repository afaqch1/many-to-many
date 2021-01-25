
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;



namespace many_to_many
{
    public class Student
    {
        public int id { get; set; }
        public string student_name { get; set; }
        public int student_age { get; set; }

        public ICollection<Course> course { get; set; }
    }

    public class Course
    {
       
        public int id { get; set; }
        public string name { get; set; }
        public int c_hours { get; set; }

        public bool IsDeleted { get; set; }
        public ICollection<Student> student { get; set; }


    }

    public class ClassRoom : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=.;Database=Db4;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Course>().HasQueryFilter(c => c.IsDeleted);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ClassRoom())
            {
                var std = new Student()
                {
                    student_name = "Afaq",
                    student_age = 23



                };
                var std1 = new Student()
                {
                    student_name = "Yasim",
                    student_age = 24

                };
                var std2 = new Student()
                {
                    student_name = "Wamik",
                    student_age = 22

                };
                var std3 = new Student()
                {
                    student_name = "Umair",
                    student_age = 99

                };

                var adr = new Course()
                {

                    name = "EAD",
                    c_hours = 3


                };
                var adr1 = new Course()
                {
                    name = "Lab of OOP",
                    c_hours = 1
                };
                var adr2 = new Course()
                {
                    name = "Cloud Computing",
                    c_hours = 3
                };

                /* context.Students.Add(std);
                 context.Students.Add(std1);
                 context.Students.Add(std2);
                 context.Students.Add(std3);


                context.Courses.Add(adr);
                context.Courses.Add(adr1);
                context.Courses.Add(adr2);*/

                context.SaveChanges();

                Console.WriteLine("============using Global Filter=================");
                var con = context.Courses.ToList();
                foreach (var r in con)
                    Console.WriteLine(r.name);

                Console.WriteLine("============Ignoring Global Filter=================");
                var con1 = context.Courses.IgnoreQueryFilters().ToList();
                foreach (var r1 in con1)
                    Console.WriteLine(r1.name);


            }
        }
    }

}


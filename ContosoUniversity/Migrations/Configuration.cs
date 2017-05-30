namespace ContosoUniversity.Migrations
{
    using ContosoUniversity.DAL;
    using ContosoUniversity.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SchoolContext context)
        {
            var students = CreateStudents(context);
            var courses = CreateCourses(context);

            CreateEnrollments(context, students, courses);
        }

        private IEnumerable<Student> CreateStudents(SchoolContext context)
        {
            var students = new List<Student>
            {
                new Student{FirstName="Carson", LastName="Alexander", PhoneNumber="555-555-5555", EnrollmentDate=DateTime.Parse("2005-09-01")},
                new Student{FirstName="Meredith", LastName="Alonso", PhoneNumber="555-555-5555", EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstName="Arturo", LastName="Anand", PhoneNumber="555-555-5555", EnrollmentDate=DateTime.Parse( "2003-09-01")},
                new Student{FirstName="Gytis", LastName="Barzdukas", PhoneNumber="555-555-5555", EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstName="Yan", LastName="Li", PhoneNumber="555-555-5555", EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstName="Peggy", LastName="Justice", PhoneNumber="555-555-5555", EnrollmentDate=DateTime.Parse ("2001-09-01")},
                new Student{FirstName="Laura", LastName="Norman", PhoneNumber="555-555-5555", EnrollmentDate=DateTime.Parse( "2003-09-01")},
                new Student{FirstName="Nino", LastName="Olivetto", PhoneNumber="555-555-5555", EnrollmentDate=DateTime.Parse ("2005-09-01")}
            };

            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            return students;
        }

        private IEnumerable<Course> CreateCourses(SchoolContext context)
        {
            var courses = new List<Course>
            {
                new Course { CourseID = 1050, Title = "Chemistry", Credits = 3, },
                new Course { CourseID = 4022, Title = "Microeconomics", Credits = 3, },
                new Course { CourseID = 4041, Title = "Macroeconomics", Credits = 3, },
                new Course { CourseID = 1045, Title = "Calculus", Credits = 4, },
                new Course { CourseID = 3141, Title = "Trigonometry", Credits = 4, },
                new Course { CourseID = 2021, Title = "Composition", Credits = 3, },
                new Course { CourseID = 2042, Title = "Literature", Credits = 4, }
            };

            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.Title, s));
            context.SaveChanges();

            return courses;
        }

        private static void CreateEnrollments(SchoolContext context, IEnumerable<Student> students, IEnumerable<Course> courses)
        {
            var enrollments = new List<Enrollment>
            {
                new Enrollment
                {   StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    Grade = Grade.A
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                    Grade = Grade.C
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                    Grade = Grade.B
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                    Grade = Grade.B
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                    Grade = Grade.B
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                    Grade = Grade.B
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.LastName == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.LastName == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                    Grade = Grade.B
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.LastName == "Barzdukas").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                    Grade = Grade.B
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.LastName == "Li").ID,
                    CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                    Grade = Grade.B
                },
                new Enrollment
                {
                    StudentID = students.Single(s => s.LastName == "Justice").ID,
                    CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                    Grade = Grade.B
                }
            };

            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(s => s.Student.ID == e.StudentID && s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }

            context.SaveChanges();
        }
    }
}

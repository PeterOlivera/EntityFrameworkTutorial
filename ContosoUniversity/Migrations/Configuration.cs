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
            var instructors = CreateInstructors(context);
            var departments = CreateDepartments(context, instructors);
            var courses = CreateCourses(context, departments);

            CreateOfficeAssigments(context, instructors);
            UpdateInstructorsInCourses(context);
            CreateEnrollments(context, students, courses);
        }

        private IEnumerable<Student> CreateStudents(SchoolContext context)
        {
            var students = new List<Student>
            {
                new Student{FirstName="Carson", LastName="Alexander", PhoneNumber="555-555-5001", EnrollmentDate=DateTime.Parse("2010-09-01")},
                new Student{FirstName="Meredith", LastName="Alonso", PhoneNumber="555-555-5002", EnrollmentDate=DateTime.Parse("2012-09-01")},
                new Student{FirstName="Arturo", LastName="Anand", PhoneNumber="555-555-5003", EnrollmentDate=DateTime.Parse( "2013-09-01")},
                new Student{FirstName="Gytis", LastName="Barzdukas", PhoneNumber="555-555-5004", EnrollmentDate=DateTime.Parse("2012-09-01")},
                new Student{FirstName="Yan", LastName="Li", PhoneNumber="555-555-5005", EnrollmentDate=DateTime.Parse("2012-09-01")},
                new Student{FirstName="Peggy", LastName="Justice", PhoneNumber="555-555-5006", EnrollmentDate=DateTime.Parse ("2011-09-01")},
                new Student{FirstName="Laura", LastName="Norman", PhoneNumber="555-555-5007", EnrollmentDate=DateTime.Parse( "2013-09-01")},
                new Student{FirstName="Nino", LastName="Olivetto", PhoneNumber="555-555-5008", EnrollmentDate=DateTime.Parse ("2005-09-01")}
            };

            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            return students;
        }

        private IEnumerable<Instructor> CreateInstructors(SchoolContext context)
        {
            var instructors = new List<Instructor>
            {
                new Instructor { FirstName = "Kim", LastName = "Abercrombie", HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { FirstName = "Fadi", LastName = "Fakhouri", HireDate = DateTime.Parse("2002-07-06") },
                new Instructor { FirstName = "Roger", LastName = "Harui", HireDate = DateTime.Parse("1998-07-01") },
                new Instructor { FirstName = "Candace", LastName = "Kapoor", HireDate = DateTime.Parse("2001-01-15") },
                new Instructor { FirstName = "Roger", LastName = "Zheng", HireDate = DateTime.Parse("2004-02-12") }
            };

            instructors.ForEach(s => context.Instructors.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            return instructors;
        }

        private IEnumerable<Department> CreateDepartments(SchoolContext context, IEnumerable<Instructor> instructors)
        {
            var departments = new List<Department>
            {
                new Department
                {
                    Name = "English",     Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Abercrombie").ID
                },
                new Department
                {
                    Name = "Mathematics", Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Fakhouri").ID
                },
                new Department
                {
                    Name = "Engineering", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Harui").ID
                },
                new Department
                {
                    Name = "Economics",   Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Kapoor").ID
                }
            };

            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            return departments;
        }

        private IEnumerable<Course> CreateCourses(SchoolContext context, IEnumerable<Department> departments)
        {
            var courses = new List<Course>
            {
                new Course
                {
                    CourseID = 1050, Title = "Chemistry",
                    Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Engineering").DepartmentID,
                    Instructors = new List<Instructor>()
                },
                new Course
                {
                    CourseID = 4022,
                    Title = "Microeconomics",
                    Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID,
                    Instructors = new List<Instructor>()
                },
                new Course
                {
                    CourseID = 4041,
                    Title = "Macroeconomics",
                    Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID,
                    Instructors = new List<Instructor>()
                },
                new Course
                {
                    CourseID = 1045,
                    Title = "Calculus",
                    Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID,
                    Instructors = new List<Instructor>()
                },
                new Course
                {
                    CourseID = 3141,
                    Title = "Trigonometry",
                    Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID,
                    Instructors = new List<Instructor>()
                },
                new Course
                {
                    CourseID = 2021, Title = "Composition",
                    Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "English").DepartmentID,
                    Instructors = new List<Instructor>()
                 },
                new Course
                {
                    CourseID = 2042,
                    Title = "Literature",
                    Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "English").DepartmentID,
                    Instructors = new List<Instructor>()
                },
            };

            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.CourseID, s));
            context.SaveChanges();

            return courses;
        }

        private void CreateOfficeAssigments(SchoolContext context, IEnumerable<Instructor> instructors)
        {
            var officeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment
                {
                    InstructorID = instructors.Single(i => i.LastName == "Fakhouri").ID,
                    Location = "Smith 17"
                },
                new OfficeAssignment
                {
                    InstructorID = instructors.Single(i => i.LastName == "Harui").ID,
                    Location = "Gowan 27"
                },
                new OfficeAssignment
                {
                    InstructorID = instructors.Single(i => i.LastName == "Kapoor").ID,
                    Location = "Thompson 304"
                },
            };

            officeAssignments.ForEach(s => context.OfficeAssignments.AddOrUpdate(p => p.InstructorID, s));
            context.SaveChanges();
        }

        private void UpdateInstructorsInCourses(SchoolContext context)
        {
            AddOrUpdateInstructor(context, "Chemistry", "Kapoor");
            AddOrUpdateInstructor(context, "Chemistry", "Harui");

            AddOrUpdateInstructor(context, "Microeconomics", "Zheng");
            AddOrUpdateInstructor(context, "Macroeconomics", "Zheng");

            AddOrUpdateInstructor(context, "Calculus", "Fakhouri");

            AddOrUpdateInstructor(context, "Trigonometry", "Harui");

            AddOrUpdateInstructor(context, "Composition", "Abercrombie");

            AddOrUpdateInstructor(context, "Literature", "Abercrombie");

            context.SaveChanges();
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
                var enrollmentInDataBase = context.Enrollments
                                                  .Where(s => s.Student.ID == e.StudentID && s.Course.CourseID == e.CourseID)
                                                  .SingleOrDefault();

                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }

            context.SaveChanges();
        }

        void AddOrUpdateInstructor(SchoolContext context, string courseTitle, string instructorName)
        {
            var crs = context.Courses.SingleOrDefault(c => c.Title == courseTitle);
            var inst = crs.Instructors.SingleOrDefault(i => i.LastName == instructorName);
            if (inst == null)
                crs.Instructors.Add(context.Instructors.Single(i => i.LastName == instructorName));
        }
    }
}

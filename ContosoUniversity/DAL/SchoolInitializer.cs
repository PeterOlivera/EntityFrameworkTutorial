﻿using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace ContosoUniversity.DAL
{
    public class SchoolInitializer : DropCreateDatabaseIfModelChanges<SchoolContext> 
    {
        // Other possible base classes for testing: DropCreateDatabaseAlways

        protected override void Seed(SchoolContext context)
        {
            CreateStudents(context);

            CreateCourses(context);

            CreateEnrollments(context);
        }

        private static void CreateStudents(SchoolContext context)
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

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();
        }

        private static void CreateCourses(SchoolContext context)
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

            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();
        }

        private static void CreateEnrollments(SchoolContext context)
        {
            var enrollments = new List<Enrollment>
            {
                new Enrollment { StudentID = 1, CourseID = 1050, Grade = Grade.A },
                new Enrollment { StudentID = 1, CourseID = 4022, Grade = Grade.C },
                new Enrollment { StudentID = 1, CourseID = 4041, Grade = Grade.B },
                new Enrollment { StudentID = 2, CourseID = 1045, Grade = Grade.B },
                new Enrollment { StudentID = 2, CourseID = 3141, Grade = Grade.F },
                new Enrollment { StudentID = 2, CourseID = 2021, Grade = Grade.F },
                new Enrollment { StudentID = 3, CourseID = 1050 },
                new Enrollment { StudentID = 4, CourseID = 1050, },
                new Enrollment { StudentID = 4, CourseID = 4022, Grade = Grade.F },
                new Enrollment { StudentID = 5, CourseID = 4041, Grade = Grade.C },
                new Enrollment { StudentID = 6, CourseID = 1045 },
                new Enrollment { StudentID = 7, CourseID = 3141, Grade = Grade.A }
            };

            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }

    }
}
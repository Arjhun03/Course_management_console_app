using System;
using System.Collections.Generic;
using courceses;

namespace CourseServices
{
    public class CourseService
    {
        public List<Course> courses = new List<Course>
        {
            new Course("C101", "CSharp", 5000, 30, 2),
            new Course("C102", "Java", 4500, 25, 3),
            
            new Course("C103", "Python", 4000, 20, 2),
            new Course("C104", "Web Development", 6000, 40, 3),
            new Course("C105", "SQL", 3000, 15, 2)
        };

        public void ListCourses()
        {
            foreach (Course c in courses)
            {
                Console.WriteLine(
                    $"{c.Code} | {c.Title} | Fee: {c.Fee:F2} | " +
                    $"Duration: {c.Duration} days | Seats: {c.Seats}");
            }
        }

        public void SearchCourse()
        {
            Console.Write("Enter course code or title: ");
            string search = (Console.ReadLine() ?? "").Trim();

            bool found = false;

            foreach (Course c in courses)
            {
                if (c.Code.Contains(
                        search, StringComparison.OrdinalIgnoreCase) ||
                    c.Title.Contains(
                        search, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(
                        $"{c.Code} | {c.Title} | Fee: {c.Fee:F2}");

                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Course not found!");
            }
        }

        public Course? FindCourseByCode(string code)
        {
            foreach (Course c in courses)
            {
                if (c.Code.Equals(
                        code, StringComparison.OrdinalIgnoreCase))
                {
                    return c;
                }
            }

            return null;
        }
        
        
        
        
    }
}
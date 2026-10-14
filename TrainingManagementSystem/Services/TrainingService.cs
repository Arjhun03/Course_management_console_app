using System;
using TrainingManagementSystem.Models;
namespace TrainingManagementSystem.Services
{
    public class TrainingService
    {
        public Course[] Courses { get; private set; } =
        {
            new Course("C101", "C# Programming", 5000m, 30, 25),
            new Course("C102", "Java Programming", 4500m, 25, 20),
            new Course("C103", "Web Development", 6000m, 40, 30),
            new Course("C104", "Python Programming", 4000m, 20, 25),
            new Course("C105", "Data Science", 8000m, 45, 15)
        };

        public void ListCourses()
        {
            Console.WriteLine(
                "\n{0,-10} {1,-25} {2,-12} {3,-12} {4,-12}",
                "Code", "Title", "Fee", "Duration", "Available");

            Console.WriteLine(
                "--------------------------------------------------------------------------");

            foreach (Course course in Courses)
            {
                Console.WriteLine(
                    "{0,-10} {1,-25} {2,-12:F2} {3,-12} {4,-12}",
                    course.CourseCode,
                    course.Title,
                    course.Fee,
                    course.DurationDays,
                    course.AvailableSeats);
            }
        }

        public Course? FindCourseByCode(string courseCode)
        {
            foreach (Course course in Courses)
            {
                if (course.CourseCode.Equals(
                    courseCode,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return course;
                }
            }

            return null;
        }

        public void SearchCourse()
        {
            Console.Write("Enter course code or title: ");
            string search = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(search))
            {
                Console.WriteLine("Search input cannot be empty!");
                return;
            }

            bool found = false;

            foreach (Course course in Courses)
            {
                if (course.CourseCode.Equals(
                        search,
                        StringComparison.OrdinalIgnoreCase) ||
                    course.Title.Contains(
                        search,
                        StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Code: {course.CourseCode}");
                    Console.WriteLine($"Title: {course.Title}");
                    Console.WriteLine($"Fee: {course.Fee:F2}");
                    Console.WriteLine($"Duration: {course.DurationDays} days");
                    Console.WriteLine($"Available seats: {course.AvailableSeats}");
                    Console.WriteLine("-----------------------------");

                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Course not found!");
            }
        }

        public void SortCoursesByFee()
        {
            Console.WriteLine("1. Fee ascending");
            Console.WriteLine("2. Fee descending");
            Console.Write("Choose sorting order: ");

            if (!int.TryParse(Console.ReadLine(), out int choice) ||
                (choice != 1 && choice != 2))
            {
                Console.WriteLine("Invalid choice!");
                return;
            }

            Course[] sortedCourses = (Course[])Courses.Clone();

            for (int i = 0; i < sortedCourses.Length - 1; i++)
            {
                for (int j = 0; j < sortedCourses.Length - i - 1; j++)
                {
                    bool shouldSwap = choice == 1
                        ? sortedCourses[j].Fee > sortedCourses[j + 1].Fee
                        : sortedCourses[j].Fee < sortedCourses[j + 1].Fee;

                    if (shouldSwap)
                    {
                        Course temp = sortedCourses[j];
                        sortedCourses[j] = sortedCourses[j + 1];
                        sortedCourses[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine("\nCourses sorted by fee:");

            foreach (Course course in sortedCourses)
            {
                Console.WriteLine(
                    $"{course.CourseCode} | {course.Title} | " +
                    $"Fee: {course.Fee:F2} | " +
                    $"Available seats: {course.AvailableSeats}");
            }
        }

        public CourseSummary GetCourseSummary(string courseCode)
        {
            Course? course = FindCourseByCode(courseCode);

            if (course == null)
            {
                throw new ArgumentException("Course not found!");
            }

            return new CourseSummary(
                course.CourseCode,
                course.Title,
                course.Fee,
                course.AvailableSeats);
        }
    }
}
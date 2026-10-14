using System;
using System.Collections.Generic;
using courceses;
using leaners;
using CourseServices;

namespace LeanersService
{
    public class LearnerService
    {
        private CourseService cs = new CourseService();

        private static Dictionary<int, Learner> learners =
            new Dictionary<int, Learner>();

        public void RegisterLearner()
        {
            Console.Write("Enter learner ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
            {
                Console.WriteLine("Invalid learner ID!");
                return;
            }

            if (learners.ContainsKey(id))
            {
                Console.WriteLine("Learner ID already exists!");
                return;
            }

            Console.Write("Enter name: ");
            string name = (Console.ReadLine() ?? "").Trim();

            Console.Write("Enter contact: ");
            string contact = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(contact))
            {
                Console.WriteLine("Name and contact cannot be empty!");
                return;
            }

            learners.Add(id, new Learner(id, name, contact));

            Console.WriteLine("Learner registered successfully!");
        }

        public void ListLearners()
        {
            if (learners.Count == 0)
            {
                Console.WriteLine("No learners registered.");
                return;
            }

            foreach (Learner l in learners.Values)
            {
                Console.WriteLine($"Name: {l.Name}");
                Console.WriteLine($"ID: {l.Id}");
                Console.WriteLine($"Contact: {l.Contact}");
                Console.WriteLine("-------------------");
            }
        }

        public void SearchLearner()
        {
            Console.Write("Enter learner ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid learner ID!");
                return;
            }

            if (learners.TryGetValue(id, out Learner? l))
            {
                Console.WriteLine($"Name: {l.Name}");
                Console.WriteLine($"ID: {l.Id}");
                Console.WriteLine($"Contact: {l.Contact}");
            }
            else
            {
                Console.WriteLine("Learner not found.");
            }
        }

        public Learner? FindLearnerById(int id)
        {
            if (learners.TryGetValue(id, out Learner? learner))
            {
                return learner;
            }

            return null;
        }

        public void EnrollLearner()
        {
            Console.Write("Enter learner ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid learner ID!");
                return;
            }

            if (!learners.TryGetValue(id, out Learner? learner))
            {
                Console.WriteLine("Learner not found!");
                return;
            }

            if (!string.IsNullOrEmpty(learner.CourseCode))
            {
                Console.WriteLine("Learner is already enrolled!");
                return;
            }

            cs.ListCourses();

            Console.Write("Enter course code: ");
            string code = (Console.ReadLine() ?? "").Trim();

            Course? selectedCourse = cs.FindCourseByCode(code);

            if (selectedCourse == null)
            {
                Console.WriteLine("Course not found!");
                return;
            }

            if (selectedCourse.Seats <= 0)
            {
                Console.WriteLine("Course is full!");
                return;
            }

            Console.WriteLine("1. Online (+200)");
            Console.WriteLine("2. Classroom (+500)");
            Console.Write("Choose session: ");

            if (!int.TryParse(Console.ReadLine(), out int session) ||
                (session != 1 && session != 2))
            {
                Console.WriteLine("Invalid session!");
                return;
            }

            decimal fee = selectedCourse.Fee;

            if (session == 1)
            {
                fee += 200;
            }
            else
            {
                fee += 500;
            }

            Console.Write("Early-bird discount? (y/n): ");

            if ((Console.ReadLine() ?? "").Trim()
                .Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                fee -= selectedCourse.Fee * 0.10m;
            }

            Console.Write("Group discount? (y/n): ");

            if ((Console.ReadLine() ?? "").Trim()
                .Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                fee -= selectedCourse.Fee * 0.05m;
            }

            learner.CourseCode = selectedCourse.Code;
            learner.FinalFee = fee;
            selectedCourse.Seats--;

            Console.WriteLine("Enrollment successful!");
            Console.WriteLine($"Course: {selectedCourse.Title}");
            Console.WriteLine($"Final fee: {fee:F2}");
        }

        public void CancelEnrollment()
        {
            Console.Write("Enter learner ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid learner ID!");
                return;
            }

            if (!learners.TryGetValue(id, out Learner? learner))
            {
                Console.WriteLine("Learner not found!");
                return;
            }

            if (string.IsNullOrEmpty(learner.CourseCode))
            {
                Console.WriteLine("Learner is not enrolled!");
                return;
            }

            if (learner.IsPaid)
            {
                Console.WriteLine("Refund required before cancellation!");
                return;
            }

            Course? course = cs.FindCourseByCode(learner.CourseCode);

            if (course != null)
            {
                course.Seats++;
            }

            learner.CourseCode = "";
            learner.FinalFee = 0;

            Console.WriteLine("Enrollment cancelled!");
        }
    }
}
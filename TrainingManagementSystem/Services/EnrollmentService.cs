using System;
using System.Collections.Generic;
using TrainingManagementSystem.Models;
namespace TrainingManagementSystem.Services
{
    public class EnrollmentService
    {
        private List<Enrollment> enrollments =
            new List<Enrollment>();

        private TrainingService trainingService;
        private LearnerService learnerService;
        private FeeCalculator feeCalculator;

        private int nextEnrollmentId = 1;

        public List<Enrollment> Enrollments
        {
            get { return enrollments; }
        }

        public EnrollmentService(
            TrainingService trainingService,
            LearnerService learnerService,
            FeeCalculator feeCalculator)
        {
            this.trainingService = trainingService;
            this.learnerService = learnerService;
            this.feeCalculator = feeCalculator;
        }

        public void EnrollLearner()
        {
            Console.Write("Enter learner ID: ");
            string learnerId = (Console.ReadLine() ?? "").Trim();

            Learner? learner =
                learnerService.FindLearnerById(learnerId);

            if (learner == null)
            {
                Console.WriteLine("Learner not found!");
                return;
            }

            trainingService.ListCourses();

            Console.Write("Enter course code: ");
            string courseCode = (Console.ReadLine() ?? "").Trim();

            Course? course =
                trainingService.FindCourseByCode(courseCode);

            if (course == null)
            {
                Console.WriteLine("Course not found!");
                return;
            }

            foreach (Enrollment enrollment in enrollments)
            {
                if (enrollment.LearnerId == learnerId &&
                    enrollment.CourseCode.Equals(
                        course.CourseCode,
                        StringComparison.OrdinalIgnoreCase) &&
                    enrollment.Status == EnrollmentStatus.Active)
                {
                    Console.WriteLine(
                        "Learner is already enrolled in this course!");
                    return;
                }
            }

            if (course.AvailableSeats <= 0)
            {
                Console.WriteLine("Course is full!");
                return;
            }

            Console.WriteLine("1. Online");
            Console.WriteLine("2. Classroom");
            Console.Write("Choose session: ");

            if (!int.TryParse(
                    Console.ReadLine(),
                    out int sessionChoice) ||
                (sessionChoice != 1 && sessionChoice != 2))
            {
                Console.WriteLine("Invalid session choice!");
                return;
            }

            Console.Write("Enter course start date (yyyy-MM-dd): ");

            if (!DateTime.TryParse(
                    Console.ReadLine(),
                    out DateTime startDate))
            {
                Console.WriteLine("Invalid start date!");
                return;
            }

            if (startDate.Date < DateTime.Today)
            {
                Console.WriteLine("Start date cannot be in the past!");
                return;
            }

            bool earlyBird =
                (startDate.Date - DateTime.Today).TotalDays >= 14;

            Console.Write("Enter number of learners in the group: ");

            if (!int.TryParse(
                    Console.ReadLine(),
                    out int groupSize) ||
                groupSize < 1)
            {
                Console.WriteLine("Invalid group size!");
                return;
            }

            bool isOnline = sessionChoice == 1;

            var feeDetails = feeCalculator.CalculateFee(
                course.Fee,
                earlyBird,
                groupSize,
                isOnline);

            decimal surcharge = isOnline ? 200m : 800m;

            feeCalculator.DisplayFeeBreakdown(
                feeDetails.BaseFee,
                feeDetails.DiscountAmount,
                surcharge,
                feeDetails.FinalFee);

            Enrollment newEnrollment = new Enrollment(
                nextEnrollmentId,
                learnerId,
                course.CourseCode,
                DateTime.Now,
                feeDetails.BaseFee,
                feeDetails.DiscountAmount,
                feeDetails.FinalFee);

            enrollments.Add(newEnrollment);
            course.EnrolledCount++;
            nextEnrollmentId++;

            Console.WriteLine("\nEnrollment successful!");
            Console.WriteLine(
                $"Enrollment ID: {newEnrollment.EnrollmentId}");
            Console.WriteLine($"Learner: {learner.Name}");
            Console.WriteLine($"Course: {course.Title}");
            Console.WriteLine($"Start Date: {startDate:yyyy-MM-dd}");
            Console.WriteLine(
                $"Session: {(isOnline ? "Online" : "Classroom")}");
            Console.WriteLine(
                $"Final Fee: ₹{newEnrollment.FinalFee:F2}");
        }

        public void ListEnrollments()
        {
            if (enrollments.Count == 0)
            {
                Console.WriteLine("No enrollment records found!");
                return;
            }

            foreach (Enrollment enrollment in enrollments)
            {
                Console.WriteLine(
                    $"Enrollment ID: {enrollment.EnrollmentId}");
                Console.WriteLine(
                    $"Learner ID: {enrollment.LearnerId}");
                Console.WriteLine(
                    $"Course Code: {enrollment.CourseCode}");
                Console.WriteLine(
                    $"Enrollment Date: {enrollment.EnrollmentDate}");
                Console.WriteLine(
                    $"Base Fee: ₹{enrollment.BaseFee:F2}");
                Console.WriteLine(
                    $"Discount: ₹{enrollment.DiscountAmount:F2}");
                Console.WriteLine(
                    $"Final Fee: ₹{enrollment.FinalFee:F2}");
                Console.WriteLine($"Status: {enrollment.Status}");
                Console.WriteLine("-----------------------------");
            }
        }

        public Enrollment? FindEnrollmentById(int enrollmentId)
        {
            foreach (Enrollment enrollment in enrollments)
            {
                if (enrollment.EnrollmentId == enrollmentId)
                {
                    return enrollment;
                }
            }

            return null;
        }

        public void CancelEnrollment()
        {
            Console.Write("Enter enrollment ID: ");

            if (!int.TryParse(
                    Console.ReadLine(),
                    out int enrollmentId))
            {
                Console.WriteLine("Invalid enrollment ID!");
                return;
            }

            Enrollment? enrollment =
                FindEnrollmentById(enrollmentId);

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found!");
                return;
            }

            if (enrollment.Status == EnrollmentStatus.Cancelled)
            {
                Console.WriteLine("Enrollment is already cancelled!");
                return;
            }

            if (enrollment.Status == EnrollmentStatus.Completed)
            {
                Console.WriteLine(
                    "Completed enrollment cannot be cancelled!");
                return;
            }

            Course? course =
                trainingService.FindCourseByCode(enrollment.CourseCode);

            if (course != null && course.EnrolledCount > 0)
            {
                course.EnrolledCount--;
            }

            enrollment.Status = EnrollmentStatus.Cancelled;

            Console.WriteLine("Enrollment cancelled successfully!");
            Console.WriteLine(
                $"Enrollment ID: {enrollment.EnrollmentId}");
        }
    }
}

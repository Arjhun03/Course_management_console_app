using System;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class ReportService
    {
        private TrainingService trainingService;
        private EnrollmentService enrollmentService;
        private PaymentService paymentService;

        public ReportService(
            TrainingService trainingService,
            EnrollmentService enrollmentService,
            PaymentService paymentService)
        {
            this.trainingService = trainingService;
            this.enrollmentService = enrollmentService;
            this.paymentService = paymentService;
        }

        public void DisplayCourseOccupancyReport()
        {
            Console.WriteLine("\n--- Course Occupancy Report ---");

            Console.WriteLine(
                "{0,-10} {1,-25} {2,-12} {3,-12} {4,-12} {5,-12}",
                "Code", "Course", "Total Seats",
                "Occupied", "Available", "Occupancy %");

            foreach (Course course in trainingService.Courses)
            {
                int totalSeats = course.TotalSeats;
                int occupiedSeats = course.EnrolledCount;
                int availableSeats = course.AvailableSeats;

                decimal occupancyPercentage = totalSeats > 0
                    ? (decimal)occupiedSeats / totalSeats * 100
                    : 0;

                Console.WriteLine(
                    "{0,-10} {1,-25} {2,-12} {3,-12} {4,-12} {5,-12:F2}",
                    course.CourseCode,
                    course.Title,
                    totalSeats,
                    occupiedSeats,
                    availableSeats,
                    occupancyPercentage);
            }
        }

        public void DisplayExpectedFeesReport()
        {
            decimal expectedFees = 0m;

            foreach (Enrollment enrollment in enrollmentService.Enrollments)
            {
                if (enrollment.Status == EnrollmentStatus.Active)
                {
                    expectedFees += enrollment.FinalFee;
                }
            }

            Console.WriteLine("\n--- Expected Fees Report ---");
            Console.WriteLine($"Expected Fees: ₹{expectedFees:F2}");
        }

        public void DisplayPaymentSummaryReport()
        {
            Console.WriteLine("\n--- Payment Summary Report ---");

            Console.WriteLine(
                $"Successful Payments: {paymentService.GetSuccessfulPaymentCount()}");

            Console.WriteLine(
                $"Failed Payments: {paymentService.GetFailedPaymentCount()}");

            Console.WriteLine(
                $"Total Collected: ₹{paymentService.GetTotalCollected():F2}");
        }

        public void DisplayCourseEnrollmentSummary()
        {
            Console.WriteLine("\n--- Course Enrollment Summary ---");

            foreach (Course course in trainingService.Courses)
            {
                Console.WriteLine($"Course Code: {course.CourseCode}");
                Console.WriteLine($"Course Title: {course.Title}");
                Console.WriteLine($"Active Enrollments: {course.EnrolledCount}");
                Console.WriteLine($"Available Seats: {course.AvailableSeats}");

                if (course.AvailableSeats == 0)
                {
                    Console.WriteLine("Status: Course is full");
                }
                else
                {
                    Console.WriteLine("Status: Seats available");
                }

                Console.WriteLine("-----------------------------");
            }
        }

        public void DisplayAllReports()
        {
            DisplayCourseOccupancyReport();
            DisplayExpectedFeesReport();
            DisplayPaymentSummaryReport();
            DisplayCourseEnrollmentSummary();
        }
    }
}
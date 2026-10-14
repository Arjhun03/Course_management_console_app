using System;
using TrainingManagementSystem.Services;

namespace TrainingManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TrainingService trainingService = new TrainingService();

            LearnerService learnerService = new LearnerService();

            FeeCalculator feeCalculator = new FeeCalculator();

            EnrollmentService enrollmentService = new EnrollmentService(
                trainingService,
                learnerService,
                feeCalculator);

            PaymentService paymentService = new PaymentService(
                enrollmentService);

            ReportService reportService = new ReportService(
                trainingService,
                enrollmentService,
                paymentService);

            bool running = true;

            while (running)
            {
                Console.WriteLine("\n======================================");
                Console.WriteLine("   TRAINING MANAGEMENT SYSTEM");
                Console.WriteLine("======================================");
                Console.WriteLine("1.  List Courses");
                Console.WriteLine("2.  Search Course");
                Console.WriteLine("3.  Sort Courses by Fee");
                Console.WriteLine("4.  Register Learner");
                Console.WriteLine("5.  List Learners");
                Console.WriteLine("6.  Search Learner");
                Console.WriteLine("7.  Enroll Learner");
                Console.WriteLine("8.  List Enrollments");
                Console.WriteLine("9.  Cancel Enrollment");
                Console.WriteLine("10. Process Payment");
                Console.WriteLine("11. View Payment History");
                Console.WriteLine("12. View Reports");
                Console.WriteLine("13. Exit");
                Console.WriteLine("======================================");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input! Enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        trainingService.ListCourses();
                        break;

                    case 2:
                        trainingService.SearchCourse();
                        break;

                    case 3:
                        trainingService.SortCoursesByFee();
                        break;

                    case 4:
                        learnerService.RegisterLearner();
                        break;

                    case 5:
                        learnerService.ListLearners();
                        break;

                    case 6:
                        learnerService.SearchLearner();
                        break;

                    case 7:
                        enrollmentService.EnrollLearner();
                        break;

                    case 8:
                        enrollmentService.ListEnrollments();
                        break;

                    case 9:
                        enrollmentService.CancelEnrollment();
                        break;

                    case 10:
                        paymentService.ProcessPayment();
                        break;

                    case 11:
                        paymentService.ListPaymentHistory();
                        break;

                    case 12:
                        reportService.DisplayAllReports();
                        break;

                    case 13:
                        running = false;
                        Console.WriteLine(
                            "Thank you for using Training Management System!");
                        break;

                    default:
                        Console.WriteLine(
                            "Invalid choice! Select a number from 1 to 13.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                }
            }
        }
    }
}
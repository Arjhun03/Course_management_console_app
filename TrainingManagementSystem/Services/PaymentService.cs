using System;
using System.Collections.Generic;
using TrainingManagementSystem.Models;
using TrainingManagementSystem.Payments;

namespace TrainingManagementSystem.Services
{
    public class PaymentService
    {
        private List<Payment> payments = new List<Payment>();

        private EnrollmentService enrollmentService;
        private IPaymentProcessor onlineProcessor;
        private IPaymentProcessor classroomProcessor;

        private int nextPaymentId = 1;

        public List<Payment> Payments
        {
            get { return payments; }
        }

        public PaymentService(EnrollmentService enrollmentService)
        {
            this.enrollmentService = enrollmentService;

            onlineProcessor = new OnlinePaymentProcessor();

            classroomProcessor = new ClassroomPaymentProcessor();
        }

        public void ProcessPayment()
        {
            Console.Write("Enter enrollment ID: ");

            if (!int.TryParse(Console.ReadLine(), out int enrollmentId))
            {
                Console.WriteLine("Invalid enrollment ID!");
                return;
            }

            Enrollment? enrollment =
                enrollmentService.FindEnrollmentById(enrollmentId);

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found!");
                return;
            }

            if (enrollment.Status == EnrollmentStatus.Cancelled)
            {
                Console.WriteLine("Cannot pay for a cancelled enrollment!");
                return;
            }

            Console.WriteLine("1. Online Payment");
            Console.WriteLine("2. Classroom Payment");
            Console.Write("Choose payment method: ");

            if (!int.TryParse(Console.ReadLine(), out int choice) ||
                (choice != 1 && choice != 2))
            {
                Console.WriteLine("Invalid payment method!");
                return;
            }

            Payment payment = new Payment(
                nextPaymentId++,
                enrollment.EnrollmentId,
                enrollment.FinalFee,
                PaymentStatus.Pending);

            bool success;

            if (choice == 1)
            {
                success = onlineProcessor.ProcessPayment(payment);
            }
            else
            {
                success = classroomProcessor.ProcessPayment(payment);
            }

            if (success)
            {
                payment.Status = PaymentStatus.Successful;
                Console.WriteLine("Payment completed successfully!");
            }
            else
            {
                payment.Status = PaymentStatus.Failed;
                Console.WriteLine("Payment failed!");
            }

            payments.Add(payment);
        }

        public void ListPaymentHistory()
        {
            if (payments.Count == 0)
            {
                Console.WriteLine("No payment history found!");
                return;
            }

            foreach (Payment payment in payments)
            {
                Console.WriteLine($"Payment ID: {payment.PaymentId}");
                Console.WriteLine($"Enrollment ID: {payment.EnrollmentId}");
                Console.WriteLine($"Amount: ₹{payment.Amount:F2}");
                Console.WriteLine($"Payment Date: {payment.PaymentDate}");
                Console.WriteLine($"Status: {payment.Status}");
                Console.WriteLine("-----------------------------");
            }
        }

        public decimal GetTotalCollected()
        {
            decimal total = 0m;

            foreach (Payment payment in payments)
            {
                if (payment.Status == PaymentStatus.Successful)
                {
                    total += payment.Amount;
                }
            }

            return total;
        }

        public int GetSuccessfulPaymentCount()
        {
            int count = 0;

            foreach (Payment payment in payments)
            {
                if (payment.Status == PaymentStatus.Successful)
                {
                    count++;
                }
            }

            return count;
        }

        public int GetFailedPaymentCount()
        {
            int count = 0;

            foreach (Payment payment in payments)
            {
                if (payment.Status == PaymentStatus.Failed)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
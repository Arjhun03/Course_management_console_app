using System;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Payments
{
    public class ClassroomPaymentProcessor : IPaymentProcessor
    {
        bool IPaymentProcessor.ProcessPayment(Payment payment)
        {
            Console.WriteLine("Processing classroom payment...");

            if (payment.Amount <= 0)
            {
                Console.WriteLine("Invalid payment amount!");
                return false;
            }

            payment.Status = PaymentStatus.Successful;

            Console.WriteLine("Classroom payment successful!");
            return true;
        }
    }
}
using System;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Payments
{
    public class OnlinePaymentProcessor : IPaymentProcessor
    {
        public bool ProcessPayment(Payment payment)
        {
            Console.WriteLine("Processing online payment...");

            if (payment.Amount <= 0)
            {
                Console.WriteLine("Invalid payment amount!");
                return false;
            }

            payment.Status = PaymentStatus.Successful;

            Console.WriteLine("Online payment successful!");
            return true;
        }
    }
}

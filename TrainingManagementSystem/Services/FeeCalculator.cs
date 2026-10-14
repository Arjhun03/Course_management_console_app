using System;

namespace TrainingManagementSystem.Services
{
    public class FeeCalculator
    {
        private const decimal OnlineSurcharge = 200m;
        private const decimal ClassroomSurcharge = 800m;

        public (decimal BaseFee, decimal DiscountAmount, decimal FinalFee)
            CalculateFee(decimal baseFee)
        {
            if (baseFee < 0)
            {
                throw new ArgumentException("Fee cannot be negative!");
            }

            return (baseFee, 0m, baseFee);
        }

        public (decimal BaseFee, decimal DiscountAmount, decimal FinalFee)
            CalculateFee(
                decimal baseFee,
                bool earlyBird,
                int groupSize,
                bool isOnline)
        {
            if (baseFee < 0)
            {
                throw new ArgumentException("Fee cannot be negative!");
            }

            if (groupSize < 1)
            {
                throw new ArgumentException(
                    "Group size must be at least one!");
            }

            decimal discount = 0m;

            if (earlyBird)
            {
                discount += baseFee * 0.10m;
            }

            if (groupSize >= 3)
            {
                discount += baseFee * 0.05m;
            }

            decimal surcharge = isOnline
                ? OnlineSurcharge
                : ClassroomSurcharge;

            decimal finalFee = baseFee - discount + surcharge;

            return (baseFee, discount, finalFee);
        }

        public void DisplayFeeBreakdown(
            decimal baseFee,
            decimal discountAmount,
            decimal surcharge,
            decimal finalFee)
        {
            Console.WriteLine("\n--- Fee Breakdown ---");
            Console.WriteLine($"Base Fee: ₹{baseFee:F2}");
            Console.WriteLine($"Discount: ₹{discountAmount:F2}");
            Console.WriteLine($"Session Surcharge: ₹{surcharge:F2}");
            Console.WriteLine($"Final Fee: ₹{finalFee:F2}");
        }
    }
}
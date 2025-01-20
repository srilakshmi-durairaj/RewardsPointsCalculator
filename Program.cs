using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardsPointCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            List<(string CustomerID, string Month, decimal Amount)> transactions = new List<(string, string, decimal)>
        {
            ("C001", "January", 120),
            ("C002", "January", 75),
            ("C001", "February", 200),
            ("C003", "February", 50),
            ("C002", "March", 150),
            ("C001", "March", 80),
            ("C003", "March", 130)
        };

            // Calculate rewards points
            var customerRewards = new Dictionary<string, Dictionary<string, int>>();

            foreach (var transaction in transactions)
            {
                string customerId = transaction.CustomerID;
                string month = transaction.Month;
                decimal amount = transaction.Amount;

                // Calculate points for the current transaction
                int points = CalculatePoints(amount);

                // Initialize data for the customer and month if not already present
                if (!customerRewards.ContainsKey(customerId))
                    customerRewards[customerId] = new Dictionary<string, int>();
                if (!customerRewards[customerId].ContainsKey(month))
                    customerRewards[customerId][month] = 0;

                // Add points for the transaction
                customerRewards[customerId][month] += points;
            }

            // Output the results
            foreach (var customer in customerRewards)
            {
                Console.WriteLine($"Customer: {customer.Key}");
                int totalPoints = 0;

                foreach (var monthData in customer.Value)
                {
                    Console.WriteLine($"  {monthData.Key}: {monthData.Value} points");
                    totalPoints += monthData.Value;
                }

                Console.WriteLine($"  Total: {totalPoints} points\n");
            }
            Console.ReadLine();

        }

        // Function to calculate points for a single transaction
        private static int CalculatePoints(decimal amount)
        {
            int points = 0;
            if (amount > 100)
                points += (int)((amount - 100) * 2);
            if (amount > 50)
                points += (int)(Math.Min(amount, 100) - 50);
            return points;
        }

    }
}

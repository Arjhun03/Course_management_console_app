using System;
using System.Collections.Generic;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class LearnerService
    {
        private Dictionary<string, Learner> learners =
            new Dictionary<string, Learner>();

        public Dictionary<string, Learner> Learners
        {
            get { return learners; }
        }

        public void RegisterLearner()
        {
            Console.Write("Enter learner ID: ");
            string learnerId = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(learnerId))
            {
                Console.WriteLine("Learner ID cannot be empty!");
                return;
            }

            if (learners.ContainsKey(learnerId))
            {
                Console.WriteLine("Learner ID already exists!");
                return;
            }

            Console.Write("Enter learner name: ");
            string name = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Learner name cannot be empty!");
                return;
            }

            Console.Write("Enter contact number: ");
            string contact = (Console.ReadLine() ?? "").Trim();

            if (!IsValidContact(contact))
            {
                Console.WriteLine(
                    "Invalid contact number! Enter 10 digits.");
                return;
            }

            Console.Write("Enter email: ");
            string email = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(email) ||
                !email.Contains('@') ||
                !email.Contains('.'))
            {
                Console.WriteLine("Invalid email address!");
                return;
            }

            Learner learner = new Learner(
                learnerId,
                name,
                contact,
                email);

            learners.Add(learnerId, learner);

            Console.WriteLine("Learner registered successfully!");
        }

        public void ListLearners()
        {
            if (learners.Count == 0)
            {
                Console.WriteLine("No learners registered!");
                return;
            }

            foreach (Learner learner in learners.Values)
            {
                Console.WriteLine($"Learner ID: {learner.LearnerId}");
                Console.WriteLine($"Name: {learner.Name}");
                Console.WriteLine($"Contact: {learner.ContactNumber}");
                Console.WriteLine($"Email: {learner.Email}");
                Console.WriteLine("-----------------------------");
            }
        }

        public Learner? FindLearnerById(string learnerId)
        {
            if (learners.TryGetValue(learnerId, out Learner? learner))
            {
                return learner;
            }

            return null;
        }

        public void SearchLearner()
        {
            Console.Write("Enter learner ID: ");
            string learnerId = (Console.ReadLine() ?? "").Trim();

            Learner? learner = FindLearnerById(learnerId);

            if (learner == null)
            {
                Console.WriteLine("Learner not found!");
                return;
            }

            Console.WriteLine($"Learner ID: {learner.LearnerId}");
            Console.WriteLine($"Name: {learner.Name}");
            Console.WriteLine($"Contact: {learner.ContactNumber}");
            Console.WriteLine($"Email: {learner.Email}");
        }

        private bool IsValidContact(string contact)
        {
            if (contact.Length != 10)
            {
                return false;
            }

            foreach (char digit in contact)
            {
                if (!char.IsDigit(digit))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
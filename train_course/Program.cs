using courceses;
using leaners;
using System;
using LeanersService;
using CourseServices;

namespace MyNamespace
{
    class program
    {
        public static void Main(string[] args)
        {
            LearnerService ls = new LearnerService();
            CourseService cs = new CourseService();
            while (true)
            {
                Console.WriteLine("\n--- TRAINING CENTRE ---");
                Console.WriteLine("1. List Courses");
                Console.WriteLine("2. Search Course");
                Console.WriteLine("3. Register Learner");
                Console.WriteLine("4. List Learners");
                Console.WriteLine("5. Search Learner");
                Console.WriteLine("6. Enroll Learner");
                Console.WriteLine("7. Cancel Enrollment");

                
                Console.WriteLine("9. Exit");

                
                Console.Write("Enter choice: ");
                
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1: 
                        cs.ListCourses();
                        break;
                    case 2:
                        cs.SearchCourse();
                        break; 
                        
                    case 3:
                        ls.RegisterLearner();
                        break; 
                        
                    case 4:
                        ls.ListLearners();
                        break; 
                    
                    case 5:
                        ls.SearchLearner();
                        break; 
                    
                    case 6:
                        ls.EnrollLearner();
                        break; 
                    
                    case 7:
                        ls.CancelEnrollment();
                        break; 

                        
                        
                    
                    default:
                        return;
                }
            }  
            
            
        }
        
    }
}


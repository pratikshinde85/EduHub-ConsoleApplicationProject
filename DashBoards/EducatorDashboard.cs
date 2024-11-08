using System;
using System.ComponentModel.Design;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.XPath;


namespace EduHub_Repository_Console_Project
{
    internal class EducatorDashboard1
    {
        public static void EducatorDashboard(Users user1)
        {
            //  UserRepository courseobj = new UserRepository();
            CourseRepository obj = new CourseRepository();
            Console.WriteLine("---Your login id:" + user1.UserId);
            Console.WriteLine("---Login as of Educator Name :" + user1.UserName);
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("----------Welcome to Educator Dashboard-----------");
                Console.WriteLine("....................Menu Items...................");
                Console.WriteLine(" Press 1 for All Courses \n Press 2 For MyCourses\n Press 3 For Add new Course\n Press 4 For Enrollments \n Press 5 For Feedback list\n Press 6 for Material(view/update)");
                int menu = int.Parse(Console.ReadLine());
                switch (menu)
                {
                    case 1:
                        DataSet ds = new DataSet();
                        ds = obj.GetAllCourses();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            Console.WriteLine($"CourseId: {row["CourseId"]} | Course Name: {row["Title"]} ");
                        }
                        break;
                    case 2:
                        DataSet ds1 = new DataSet();
                        // int courseid=
                        ds1 = obj.GetMyCourses(user1);
                        foreach (DataRow row in ds1.Tables[0].Rows)
                        {
                            Console.WriteLine($"userId: {row["UserId"]} | Course Name: {row["Title"]} ");
                        }
                        break;

                    case 3:
                        DataSet ds2 = new DataSet();
                        Console.WriteLine("Enter Course Title:");
                        string title = Console.ReadLine();

                        Console.WriteLine("Enter Course Description:");
                        string description = Console.ReadLine();

                        Console.WriteLine("Enter Course Start Date (yyyy-MM-dd):");
                        DateTime courseStartDate;
                        while (!DateTime.TryParse(Console.ReadLine(), out courseStartDate))
                        {
                            Console.WriteLine("Invalid date format. Please enter again (yyyy-MM-dd):");
                        }

                        Console.WriteLine("Enter Course End Date (yyyy-MM-dd):");
                        DateTime courseEndDate;
                        while (!DateTime.TryParse(Console.ReadLine(), out courseEndDate))
                        {
                            Console.WriteLine("Invalid date format. Please enter again (yyyy-MM-dd):");
                        }

                        int userId = user1.UserId;

                        Console.WriteLine("Enter Course Category:");
                        string category = Console.ReadLine();

                        Console.WriteLine("Enter Course Level:");
                        string level = Console.ReadLine();

                        obj.AddCourses(title, description, courseStartDate, courseEndDate, userId, category, level);
                        Console.WriteLine("Courses Added Succussfully...");
                        break;
                    case 4:
                        DataSet ds3 = new DataSet();

                        ds3 = obj.EducatorEnrollment(user1);
                        Console.WriteLine($"-------List of Enroll Student Under Educator {user1.UserName}:----------");
                        foreach (DataRow row in ds3.Tables[0].Rows)
                        {
                            Console.WriteLine($"Student Name: {row["StudentName"]}| Course Tittle:{row["title"]}");
                        }
                        break;
                    case 5:
                        DataSet ds4 = new DataSet();
                        ds4 = obj.EducatorFeedback(user1);
                        Console.WriteLine($"-------List of FeedBacks Under Educator {user1.UserName}:----------");
                        foreach (DataRow row in ds4.Tables[0].Rows)
                        {
                            Console.WriteLine($"Student Name: {row["studentName"]} | FeedBack : {row["studentFeedback"]} | Feedback Date : {row["feedbackDate"]}");
                        }
                        break;
                    case 6:
                        MaterialRepository.MaterailMenu(user1);
                        break;
                    default:
                        Console.WriteLine("Invaid Input");
                        break;
                }
                Console.WriteLine("Do You want to stay Login?(Y/N)");
                char choiceLogin = Convert.ToChar(Console.ReadLine());
                if (choiceLogin == 'Y' || choiceLogin == 'y')
                {
                    flag = true;
                }
                else
                {
                    Console.WriteLine("Logout SucussFully for the Educator..");
                    flag = false;
                }
            }
        }
    }
}
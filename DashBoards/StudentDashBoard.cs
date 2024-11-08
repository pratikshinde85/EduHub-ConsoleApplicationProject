using System;
using System.ComponentModel.Design;
using System.Data;
using System.Runtime.CompilerServices;
using System.Xml.XPath;


namespace EduHub_Repository_Console_Project
{
    internal class StudentDashBoard1
    {
        public static void StudentDashBoard(Users user1)
        {

            StudentRepository obj = new StudentRepository();
            Console.WriteLine("---Your login id:" + user1.UserId);
            Console.WriteLine("---Login as of Student Name :" + user1.UserName);
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("----------Welcome to Student Dashboard-----------");
                Console.WriteLine("....................Menu Items...................");
                Console.WriteLine(" Press 1 for Show All Courses \n Press 2 For My Courses\n Press 3 For My Enquiry\n Press 4 For Give Feedback\n Press 5 for view Materials");
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
                        ds1 = obj.StudentMyCourses(user1);
                        foreach (DataRow row in ds1.Tables[0].Rows)
                        {
                            Console.WriteLine($"Course Name : {row["CourseTitle"]} | Student Name : {row["StudentName"]} ");
                        }
                        break;

                    case 3:
                        DataSet ds2 = new DataSet();
                        ds2 = obj.StudentEnquiry();
                        Console.WriteLine("Here is the Equiry About Course : ");
                        foreach (DataRow row in ds2.Tables[0].Rows)
                        {
                            Console.WriteLine($"Course Name :{row["CourseTitle"]}| Description :{row["Description"]}| CourseStartDate:{row["CourseStartDate"]}| CourseEndDate :{row["CourseEndDate"]}| Feedback:{row["Feedback"]}");
                        }
                        break;
                    case 4:
                        DataSet ds3 = new DataSet();
                        obj.GiveFeedBack(user1);
                        Console.WriteLine("Sucussfully Submitted Feedback...");
                        break;
                    case 5:
                        MaterialRepository obj1 = new MaterialRepository();
                        ds = obj1.ViewAllMaterial();
                        Console.WriteLine("--------------All Materials--------------");
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            Console.WriteLine($"Course Id:{row["CourseId"]} |Title : {row["Title"]} | Description : {row["Description"]} | URL :{row["URL"]}");
                        }

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
                    Console.WriteLine("Logout SucussFully for the Student ..");
                    flag = false;
                }
            }
        }
    }
}
using System;
using System.ComponentModel.Design;
using System.Data;
using System.Runtime.CompilerServices;
using System.Xml.XPath;

namespace EduHub_Repository_Console_Project
{
    internal class Program
    {
        public static void Main()
        {
            UserRepository Repo = new UserRepository();
            Users user;
            DataSet ds = new DataSet();
            int entry;
            bool Continue = true;
            Console.WriteLine("--------Welcome to yash Technology Training PlatForm-------------------");
            Console.WriteLine("...........Register Yourself As Student or Educator-----------------");

            while (Continue)
            {
                Console.WriteLine(" Press 0 to Exit \n Press 1 For All Teachers \n Press 2 All Students \n Press 3 for Add users(Stundet/Educator)\n Press 4 for Delete Record \n Press 5 For Login\n");

                entry = Convert.ToInt32(Console.ReadLine());
                switch (entry)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        Console.WriteLine("------------Show All Teachers------------");
                        ds = Repo.GetAllTeachers();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            Console.WriteLine($"Id: {row["UserId"]} | Teacher Name: {row["UserName"]} ");
                        }
                        break;
                    case 2:
                        Console.WriteLine("Show All Students.......");
                        ds = Repo.GetAllStudents();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            Console.WriteLine($"Id: {row["UserId"]} | Student Name: {row["UserName"]} ");
                        }
                        break;
                    case 3:
                        System.Console.WriteLine("-----------Add new user------------");
                        System.Console.WriteLine("-----To Add new user Enter UserName----------");
                        user = new Users();
                        System.Console.WriteLine("Enter User Name");
                        String? name = Convert.ToString(Console.ReadLine());
                        bool res = Repo.UserExists(name);
                        if (res)
                        {
                            System.Console.WriteLine("User name is Already Exits\n");
                            break;

                        }
                        else
                        {
                            user.UserName = name;
                            System.Console.WriteLine("Enter Password");
                            user.Password = Console.ReadLine();

                            System.Console.WriteLine("Enter Email");
                            user.Email = Console.ReadLine();

                            System.Console.WriteLine("Enter MobileNumber");
                            user.MobileNumber = Console.ReadLine();

                            System.Console.WriteLine("Enter UserRole");
                            user.UserRole = Console.ReadLine();

                            System.Console.WriteLine("Enter ProfileImage");
                            user.ProfileImage = Console.ReadLine();

                            int noOfRows = Repo.AddUser(user);
                            if (noOfRows > 0)
                            {
                                System.Console.WriteLine("User added successfully");
                            }
                            else
                            {
                                System.Console.WriteLine("User add fail");

                            }
                        }
                        break;
                    case 4:
                        Console.WriteLine("Want to Delete Student Record? or Teacher (S/T)\n");
                        char ch = Convert.ToChar(Console.ReadLine());
                        if (ch == 'S' || ch == 's')
                        {
                            Repo.DeleteStudent();
                        }
                        else if (ch == 'T' || ch == 't')
                        {
                            Repo.DeleteTeacher();
                        }
                        break;

                    case 5:
                        Console.WriteLine("--------------Login Page--------------");
                        Console.WriteLine("Enter the Email : ");
                        string? userl = Console.ReadLine();
                        Console.WriteLine("Enter the Password : ");
                        string? pass = Console.ReadLine();

                        Users user1 = Repo.Login(userl, pass);
                        string role = string.Empty;
                        int id = user1.UserId;
                        if (user1.Email == userl && user1.Password == pass)
                        {
                            Console.WriteLine($"User Exits\n");
                            if (user1.UserRole == "Educator" || user1.UserRole == "Teacher")
                            {
                               EducatorDashboard1.EducatorDashboard(user1);
                            }
                            else
                            {
                              StudentDashBoard1.StudentDashBoard(user1);
                            }
                        }
                        else
                        {
                            Console.WriteLine("User Not Found please try another User...");
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }

                Console.WriteLine("Do you want to continue? (y/n)");
                Char? reply = Convert.ToChar(Console.ReadLine());
                if (reply == 'n' || reply == 'N')
                {
                    Continue = false;
                }
            }
        }
    }
}
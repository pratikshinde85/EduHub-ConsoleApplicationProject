

using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
namespace EduHub_Repository_Console_Project
{

    class MaterialRepository
    {

        static SqlConnection? con;
        static SqlCommand? cmd;
        static SqlDataAdapter? da;
        static DataSet? ds;
        static DataTable? dt;
        static SqlDataReader? dr;
        public MaterialRepository()
        {
            string connetion = "data source=YISC1101230LT\\SQLEXPRESS;initial catalog=EduhubDB;integrated security=true;TrustServerCertificate=true";
            con = new SqlConnection(connetion);
            cmd = new SqlCommand();
            cmd.Connection = con;
        }


        //MaterailMenu will gives the menu for Material related things
        public static void MaterailMenu(Users user)
        {
            bool flag = true;
            Console.WriteLine("---------Welcome to Material Menu------------");

            DataSet ds = new DataSet();
            MaterialRepository obj = new MaterialRepository();

            while (flag)
            {

                Console.WriteLine(" Press 1 for the View All Materails \n Press 2  for View Your Course Material \n Press 3 Edit Material");
                int menu = Convert.ToInt32(Console.ReadLine());

                switch (menu)
                {

                    case 1:
                        ds = obj.ViewAllMaterial();
                        Console.WriteLine("--------------All Materials--------------");
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            Console.WriteLine($"Course Id:{row["CourseId"]} |Title : {row["Title"]} | Description : {row["Description"]} | URL :{row["URL"]}");
                        }

                        break;
                    case 2:
                        ds = obj.ViewYourCourseMaterail(user);
                        Console.WriteLine("--------------Materials of Your Courses--------------");
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            Console.WriteLine($"Course Id:{row["CourseId"]} |Title : {row["Title"]} | Description : {row["Description"]} | URL :{row["URL"]}");
                        }

                        break;

                    case 3:
                        ds = obj.EditMaterial(user);
                        break;
                    default:
                        Console.WriteLine("Invalid Input try another one ..");
                        break;
                }

                Console.WriteLine("Want to Continue with Material?(Y/N)");
                string? input = Console.ReadLine();
                if (input == "Y" || input == "y")
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
        }
        //ViewAllMaterial method display all Materail
        public DataSet ViewAllMaterial()
        {

            cmd.CommandText = "select CourseId ,Title,Description,URL from Materials ";
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
            return ds;


        }
        //ViewYourCourseMaterail method display particular users Materail only
        public DataSet ViewYourCourseMaterail(Users user)
        {
            int id = user.UserId;
            cmd.CommandText = @"
                SELECT 
                M.CourseId,
                M.Title AS Title,
                M.Description AS Description,
                M.URL
                FROM 
                Materials M
                JOIN 
                Courses C ON M.CourseId = C.CourseId
                JOIN 
                Users U ON C.UserId = U.UserId
                WHERE U.UserId = @id";
            cmd.Parameters.AddWithValue("@id", id);
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
            return ds;


        }

        //EditMaterial method edit the Material 
        public DataSet EditMaterial(Users user)
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Enter the Course Id which you want to Edit:");
                int courseIdEdit = Convert.ToInt32(Console.ReadLine());
                if (courseIdExits(courseIdEdit))
                {
                    Console.WriteLine("Course Exits...");
                    Console.WriteLine("------------Edit Materials---------");
                    Console.WriteLine(" Press 1 for Edit Description \n Press 2 for Edit URl");
                    int entry = Convert.ToInt32(Console.ReadLine());
                    switch (entry)
                    {

                        case 1:
                            Console.WriteLine("Enter Description: ");
                            string? description = Console.ReadLine();
                            cmd.CommandText = @"UPDATE M
                            SET M.Description = @description
                            FROM Materials M
                            JOIN Courses C ON M.CourseId = C.CourseId
                            JOIN Users U ON C.UserId = U.UserId
                            WHERE U.UserId = 2 
                            AND M.CourseId = C.CourseId
                            AND C.CourseId = @courseIdEdit";

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@description", description);
                            //cmd.Parameters.AddWithValue("@userId", user.UserId); 
                            cmd.Parameters.AddWithValue("@courseIdEdit", courseIdEdit);
                            da = new SqlDataAdapter();
                            da.SelectCommand = cmd;
                            ds = new DataSet();
                            da.Fill(ds);
                            break;

                        case 2:
                            Console.WriteLine("Enter the URL: ");
                            string? url = Console.ReadLine();
                            cmd.CommandText = @"UPDATE M
                            SET M.URL = @url
                            FROM Materials M
                            JOIN Courses C ON M.CourseId = C.CourseId
                            JOIN Users U ON C.UserId = U.UserId
                            WHERE U.UserId = 2 
                            AND M.CourseId = C.CourseId
                            AND C.CourseId = @courseIdEdit";

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@url", url);
                            //cmd.Parameters.AddWithValue("@userId", user.UserId); 
                            cmd.Parameters.AddWithValue("@courseIdEdit", courseIdEdit);
                            da = new SqlDataAdapter();
                            da.SelectCommand = cmd;
                            ds = new DataSet();
                            da.Fill(ds);
                            break;
                        default:
                            Console.WriteLine("Invalid Input..");
                            break;
                    }
                    Console.WriteLine("Want to continue with the Edit Materail?(Y/N)");
                    char? ch = Convert.ToChar(Console.ReadLine());
                    if (ch == 'Y' || ch == 'y')
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                else Console.WriteLine("Invalid Course Id Try another one..");

            }
            return ds;
        }

        //courseIdExits checks the courseId present in course table or not
        bool courseIdExits(int courseIdEdit)
        {

            bool isdata;
            cmd.Parameters.Clear();
            cmd.CommandText = "select CourseId from Materials where CourseId=@courseIdEdit";

            cmd.Parameters.AddWithValue("@courseIdEdit", courseIdEdit);
            con.Open();
            dr = cmd.ExecuteReader();
            isdata = dr.Read();
            //System.Console.WriteLine(isdata);
            con.Close();
            if (isdata)
            {
                //System.Console.WriteLine(dr[0]);
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}
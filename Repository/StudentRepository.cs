using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
//using Models;
//using ServiceRepository;
namespace EduHub_Repository_Console_Project
{
    internal class StudentRepository : IStudentRepository
    {
        static SqlConnection? con;
        static SqlCommand? cmd;
        static SqlDataAdapter? da;
        static DataSet? ds;
        static DataTable? dt;
        static SqlDataReader? dr;
        public StudentRepository()
        {
            string connetion = "data source=YISC1101230LT\\SQLEXPRESS;initial catalog=EduhubDB;integrated security=true;TrustServerCertificate=true";
            con = new SqlConnection(connetion);
            Console.WriteLine("Connection established successfully!");
            cmd = new SqlCommand();
            cmd.Connection = con;
        }

        public DataSet GetAllCourses()
        {
            string sql = "select CourseId,Title,Description,CourseStartDate,CourseEndDate,UserId,Category,Level from Courses";
            da = new SqlDataAdapter(sql, con);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        //StudentMyCourses Method Will show the studnet course who is log in
        public DataSet StudentMyCourses(Users user1)
        {
            int id = user1.UserId;
            // Console.WriteLine("Debug:"+id);
            cmd.CommandText = @"
            SELECT 
            C.CourseId,
            C.Title AS CourseTitle,
            U.UserName AS StudentName  
            FROM 
            Enrollments E
            JOIN 
            Courses C ON E.CourseId = C.CourseId
            JOIN 
            Users U ON E.UserId = U.UserId
            WHERE 
            U.UserId =@id";


            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@id", id);

            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }


        // StudentEnquiry give the Enquiry
        public DataSet StudentEnquiry()
        {

            Console.WriteLine("Enter the Course Name : ");
            string TakeInputCourseName = Console.ReadLine();
            bool chechCourseId = ExitsCourseId(TakeInputCourseName);
            if (chechCourseId)
            {
                cmd.CommandText = @"SELECT  C.Title AS CourseTitle,C.Description,C.CourseStartDate,C.CourseEndDate,F.Feedback FROM Courses C JOIN Feedbacks F ON C.CourseId = F.CourseId ORDER BY C.Title";

                cmd.Parameters.Clear();
                //cmd.Parameters.AddWithValue("@id", id);

                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            else
            {
                Console.WriteLine("Course Not Availble.. Try Another one");
            }

            return new DataSet();
        }
        bool ExitsCourseId(string TakeInputCourseName)
        {

            bool isdata;
            cmd.Parameters.Clear();
            cmd.CommandText = "select Title from Courses where Title=@TakeInputCourseName";

            cmd.Parameters.AddWithValue("@TakeInputCourseName", TakeInputCourseName);
            con.Open();
            dr = cmd.ExecuteReader();
            isdata = dr.Read();
            System.Console.WriteLine(isdata);
            con.Close();
            if (isdata)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //GiveFeedBack Method Gives the Feedback for the particular Course...
        public void GiveFeedBack(Users user)
        {

            int FeedbackuserId = user.UserId;
            Console.WriteLine("Enter the Course ID : ");
            int FeedBackcourseId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the FeedBack: ");
            string? feedback = Console.ReadLine();
            Console.WriteLine("Enter  Date (yyyy-MM-dd):");
            DateTime FeedbackDate;
            while (!DateTime.TryParse(Console.ReadLine(), out FeedbackDate))
            {
                Console.WriteLine("Invalid date format. Please enter again (yyyy-MM-dd):");
            }
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO Feedbacks(UserID, CourseId,feedback, Date) " + "VALUES (@FeedbackuserId,@FeedBackcourseId, @feedback,@FeedbackDate)";


                cmd.Parameters.AddWithValue("@FeedbackuserId", FeedbackuserId);
                cmd.Parameters.AddWithValue("@FeedBackcourseId", FeedBackcourseId);
                cmd.Parameters.AddWithValue("@feedback", feedback);
                cmd.Parameters.AddWithValue("@FeedbackDate", FeedbackDate);

                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

    }

}






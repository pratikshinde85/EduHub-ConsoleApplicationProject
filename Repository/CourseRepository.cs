using System.Data;
using System.Data.SqlClient;
//using Models;
//using ServiceRepository;
namespace EduHub_Repository_Console_Project
{
    internal class CourseRepository : ICourseRepository
    {
        static SqlConnection? con;
        static SqlCommand? cmd;
        static SqlDataAdapter? da;
        static DataSet? ds;
        static DataTable? dt;
        static SqlDataReader? dr;
        public CourseRepository()
        {
            string connetion = "data source=YISC1101230LT\\SQLEXPRESS;initial catalog=EduhubDB;integrated security=true;TrustServerCertificate=true";
            con = new SqlConnection(connetion);
            cmd = new SqlCommand();
            cmd.Connection = con;
        }

        //GetAllCourses method which will gives the all the coourses list
        public DataSet GetAllCourses()
        {
            string sql = "select CourseId,Title,Description,CourseStartDate,CourseEndDate,UserId,Category,Level from Courses";
            da = new SqlDataAdapter(sql, con);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        //GetMyCourses will give the particular user login 
        public DataSet GetMyCourses(Users user1)
        {
            int id = user1.UserId;
            // Console.WriteLine("id from users id:" + id);
            cmd.CommandText = "SELECT CourseId, Title, Description, CourseStartDate, CourseEndDate, UserId, Category, Level FROM Courses WHERE UserId =@id";
            cmd.Parameters.AddWithValue("@id", id);
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
            return ds;

        }
        //AddCourses method will add Course in course table
        public void AddCourses(string title, string description, DateTime courseStartDate, DateTime courseEndDate, int userId, string category, string level)
        {
            // SQL command to insert a new course
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con; // Set the connection for the command
                cmd.CommandText = "INSERT INTO Courses (Title, Description, CourseStartDate, CourseEndDate, UserId, Category, Level) " + "VALUES (@Title, @Description, @CourseStartDate, @CourseEndDate, @UserId, @Category, @Level)";

                // Adding parameters to the command
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@CourseStartDate", courseStartDate);
                cmd.Parameters.AddWithValue("@CourseEndDate", courseEndDate);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@Category", category);
                cmd.Parameters.AddWithValue("@Level", level);

                // Open the connection if it's not already open
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
            }
        }

        //EducatorEnrollment Method will  display the enroll student list in particular user (educator)
        public DataSet EducatorEnrollment(Users user1)
        {
            int educatorId = user1.UserId;
            cmd.CommandText = @"SELECT U.UserName AS studentName, C.title as title FROM Enrollments E JOIN 
            Users U ON U.UserId = E.UserId JOIN 
            Courses C ON E.CourseId = C.CourseId WHERE 
            C.UserId = @EducatorId"; // Filter by the educator's UserId
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EducatorId", educatorId);

            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        //EducatorFeedback will display the Feedback for the educator course 
        public DataSet EducatorFeedback(Users user1)
        {
            int educatorId = user1.UserId; 
            cmd.CommandText = @"
            SELECT 
            U.UserName AS studentName, 
            F.Feedback AS studentFeedback, 
            F.Date AS feedbackDate 
                    FROM 
                Feedbacks F 
                    JOIN 
            Enrollments E ON F.CourseId = E.CourseId 
                    JOIN 
            Users U ON U.UserId = E.UserId 
                    JOIN 
            Courses C ON E.CourseId = C.CourseId 
                    WHERE 
            C.UserId = @EducatorId"; 
            cmd.Parameters.Clear(); 
            cmd.Parameters.AddWithValue("@EducatorId", educatorId);

            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

    }

}






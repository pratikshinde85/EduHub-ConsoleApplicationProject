using System.Data;
using System.Data.SqlClient;
//using Models;
//using ServiceRepository;
namespace EduHub_Repository_Console_Project
{
    internal class UserRepository : IUserRepository
    {
        static SqlConnection? con;
        static SqlCommand? cmd;
        static SqlDataAdapter? da;
        static DataSet? ds;
        static DataTable? dt;
        static SqlDataReader? dr;
        public UserRepository()
        {
            string connetion = "data source=YISC1101230LT\\SQLEXPRESS;initial catalog=EduhubDB;integrated security=true;TrustServerCertificate=true";
            con = new SqlConnection(connetion);
            Console.WriteLine("Connection established successfully!11");
            cmd = new SqlCommand();
            cmd.Connection = con;
        }

        //AddUser method will Add the users ( student or Educator)
        public int AddUser(Users newuser)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "Insert into users(username,password,email,mobilenumber,userrole,profileimage) values (@username,@password,@email,@mobilenumber,@userrole,@profileimage)";

                cmd.Parameters.AddWithValue("@username", newuser.UserName);
                cmd.Parameters.AddWithValue("@password", newuser.Password);
                cmd.Parameters.AddWithValue("@email", newuser.Email);
                cmd.Parameters.AddWithValue("@mobilenumber", newuser.MobileNumber);
                cmd.Parameters.AddWithValue("@userrole", newuser.UserRole);
                cmd.Parameters.AddWithValue("@profileimage", newuser.ProfileImage);

                con.Open();
                int result = cmd.ExecuteNonQuery();
                con.Close();
                return result;
            }

            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return 0;

        }

        //GetAllStudents give All Students list
        public DataSet GetAllStudents()
        {
            string sql = "select UserId,Username,Email,mobilenumber,userrole,profileimage from users where userrole='student' or  userrole='Student'";
            da = new SqlDataAdapter(sql, con);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

         //GetAllTeachers give All Teacher list
        public DataSet GetAllTeachers()
        {
            string sql = "select UserId,Username,Email,mobilenumber,userrole,profileimage from users where userrole='teacher' or userrole='Educator'";
            da = new SqlDataAdapter(sql, con);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public int GetNewUserId()
        {
            throw new NotImplementedException();
        }

        public int UpdateProfile(Users user)
        {
            throw new NotImplementedException();
        }

        public bool UserExists(string username)
        {
            bool isdata;
            cmd.Parameters.Clear();
            cmd.CommandText = "select Userid from users where username=@username";

            cmd.Parameters.AddWithValue("@username", username);
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

        public DataSet DeleteStudent()
        {

            string sql = "delete from Users where userrole ='student' or userrole='Student'";
            da = new SqlDataAdapter(sql, con);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        // DeleteTeacher from table
        public DataSet DeleteTeacher()
        {

            string sql = "delete from Users where userrole ='teacher' or userrole='Teacher' or userrole='Educator'";
            da = new SqlDataAdapter(sql, con);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        //Login page code 
        public Users Login(string Email, string Password)
        {
            Users? loginuser = new Users();
            cmd.Parameters.Clear();
            cmd.CommandText = "select Userid, username, Password,Email,MobileNumber,UserRole from users where Email=@Email and Password = @PassWord";
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Password", Password);
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dt = new DataTable();
            ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                loginuser.UserId = (int)dt.Rows[0]["UserId"];
                loginuser.UserName = dt.Rows[0]["UserName"].ToString();

                loginuser.Password = dt.Rows[0]["Password"].ToString();
                loginuser.Email = dt.Rows[0]["Email"].ToString();
                loginuser.MobileNumber = dt.Rows[0]["MobileNumber"].ToString();
                loginuser.UserRole = dt.Rows[0]["UserRole"].ToString();
                return loginuser;
            }
            else
            {
                return loginuser;

            }

        }

    }
}


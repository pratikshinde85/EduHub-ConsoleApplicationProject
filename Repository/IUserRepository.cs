

using System.Data;
namespace EduHub_Repository_Console_Project
{

        interface IUserRepository
        {
                DataSet GetAllTeachers();
                DataSet GetAllStudents();

                int UpdateProfile(Users user);

                bool UserExists(string username);
                int AddUser(Users newuser);
                int GetNewUserId();

                 Users Login(string Email,string password);

        }

}
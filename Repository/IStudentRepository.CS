

using System.Data;
namespace EduHub_Repository_Console_Project
{

        interface IStudentRepository
        {
                DataSet GetAllCourses();             
                DataSet StudentMyCourses(Users user1);

                DataSet StudentEnquiry();

                void GiveFeedBack(Users user);
                
        }

}
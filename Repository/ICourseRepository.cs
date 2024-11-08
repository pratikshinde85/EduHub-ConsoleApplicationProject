

using System.Data;
namespace EduHub_Repository_Console_Project
{

        interface ICourseRepository
        {
                DataSet GetAllCourses();
              
                DataSet GetMyCourses(Users user1);
                void AddCourses(string title, string description, DateTime courseStartDate, DateTime courseEndDate, int userId, string category, string level);

                DataSet EducatorEnrollment(Users user1);

                DataSet EducatorFeedback(Users user1);

        }

}
using AngularQuiz.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AngularQuiz.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MyQuizzesScreenController : ApiController
    {
        List<Quiz> quizObjs = new List<Quiz>();
        public List<Quiz> GetQuizzes(int id)
        {
           
            string connectionstring = "Data Source=localhost;Initial Catalog = quizzie; User ID = sa; Password = Vardhini@98!; MultipleActiveResultSets=True";
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string quizCommand = "SELECT * FROM quiz where user_id='"+id+"'";
                SqlCommand sqlQuizCommand = new SqlCommand(quizCommand, connection);
                SqlDataReader sqlDataReader = sqlQuizCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    Quiz quizObj = new Quiz();
                    quizObj.quizId = Convert.ToInt32(sqlDataReader["quiz_id"]);
                    quizObj.userId = Convert.ToInt32(sqlDataReader["user_id"]);
                    quizObj.quizName = sqlDataReader["quiz_name"].ToString();
                    quizObj.quizDesc = sqlDataReader["description"].ToString();
                    quizObj.quizSubject = sqlDataReader["subject"].ToString();
                    quizObj.isActive = Convert.ToBoolean(sqlDataReader["inactive"]);
                    quizObj.questionsCount = Convert.ToInt32(sqlDataReader["questions"]);
                    quizObjs.Add(quizObj);
                }

            }
            return quizObjs;
        }
    }
}
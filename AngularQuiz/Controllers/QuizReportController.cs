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
    public class QuizReportController : ApiController
    {
        List<QuizReport> quizReports = new List<QuizReport>();
        string connectionstring = "Data Source=localhost;Initial Catalog = quizzie; User ID = sa; Password = Vardhini@98!; MultipleActiveResultSets=True";
        public List<QuizReport> GetQuizReport(int id){
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string quizCommand = "SELECT * FROM quiz_attempt where quiz_id=" + id;
                SqlCommand sqlQuizCommand = new SqlCommand(quizCommand, connection);
                SqlDataReader sqlDataReader = sqlQuizCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    QuizReport quizObj = new QuizReport();
                    quizObj.quizId = Convert.ToInt32(sqlDataReader["quiz_id"]);
                    quizObj.userId = Convert.ToInt32(sqlDataReader["user_id"]);
                    quizObj.score = Convert.ToSingle(sqlDataReader["score"]);
                    quizReports.Add(quizObj);
                }

            }
            return quizReports;
        }
    }
}

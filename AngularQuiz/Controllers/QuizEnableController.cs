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
    public class QuizEnableController : ApiController
    {
        string connectionstring = "Data Source=localhost;Initial Catalog = quizzie; User ID = sa; Password = Vardhini@98!; MultipleActiveResultSets=True";
        public string GetEnableQuiz(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string quizCommand = "SELECT * FROM quiz where quiz_id=" + id;
                SqlCommand sqlQuizCommand = new SqlCommand(quizCommand, connection);
                SqlDataReader sqlDataReader = sqlQuizCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    if (Convert.ToBoolean(sqlDataReader["inactive"]) == false)
                    {
                        string Command = "update quiz set inactive='" + true+"' where quiz_id="+id;
                        SqlCommand sqlCommand = new SqlCommand(Command, connection);
                        sqlCommand.ExecuteNonQuery();
                        return "Changed to true";
                    }
                    else if(Convert.ToBoolean(sqlDataReader["inactive"]) == true)
                    {
                        string Command = "update quiz set inactive='" + false + "' where quiz_id=" + id;
                        SqlCommand sqlCommand = new SqlCommand(Command, connection);
                        sqlCommand.ExecuteNonQuery();
                        return "Changed to false";
                    }
                }
            }
            return "Changed";
        }
    }
}

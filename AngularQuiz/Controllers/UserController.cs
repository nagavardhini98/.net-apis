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
    public class UserController : ApiController
    {
        string connectionstring = "Data Source=localhost;Initial Catalog = quizzie; User ID = sa; Password = Vardhini@98!; MultipleActiveResultSets=True";
        public int GetUserId(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string quizCommand = "SELECT * FROM users where username='" + username+"'";
                SqlCommand sqlQuizCommand = new SqlCommand(quizCommand, connection);
                SqlDataReader sqlDataReader = sqlQuizCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["user_id"]);
                }
            }
            return -1;
        }
    }
}

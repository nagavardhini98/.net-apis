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
    public class WarnController : ApiController
    {
        string connectionstring = "Data Source=localhost;Initial Catalog = quizzie; User ID = sa; Password = Vardhini@98!; MultipleActiveResultSets=True";
        public bool GetEnableQuiz(int id,int id2)
        {
            int count;
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string quizCommand = "SELECT Count(*) FROM quiz_attempt where quiz_id=" + id + "and user_id=" +id2;
                SqlCommand sqlQuizCommand = new SqlCommand(quizCommand, connection);
                count = Convert.ToInt32(sqlQuizCommand.ExecuteScalar());

                if (count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

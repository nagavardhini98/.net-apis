using AngularQuiz.Models.QuizAnswers;
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
    public class QuizAnswersController : ApiController
    {
        public string Post(Quiz quiz)
        {
            string outputString = "All connections are well..";
            string connectionstring = "Data Source=localhost;Initial Catalog = quizzie; User ID = sa; Password = Vardhini@98!";
            int quizId = quiz.quizId;
            int ques_attempt = quiz.questionforms.Count;
            foreach (var ques in quiz.questionforms)
            {
                bool result = true;
                int ans_length = ques.answers.Count;
                int ans_attempted = 0;

                foreach (var ans in ques.answers)
                {
                    if (ques.answertype.Equals("text"))
                    {
                        outputString += "Type:Text; ";


                        try
                        {
                            using (SqlConnection connection = new SqlConnection(connectionstring))
                            {
                                connection.Open();
                                string ansCommand = "Select * from answers where answer_id=" + ans.ansId;
                                SqlCommand sqlAnsCommand = new SqlCommand(ansCommand, connection);
                                SqlDataReader sqlDataReader = sqlAnsCommand.ExecuteReader();
                                while (sqlDataReader.Read())
                                {
                                    outputString += "DB answer:" + sqlDataReader["answer"].ToString()+" Got Answer:"+ans.useransText.ToString();
                                    if (ans.useransText.ToLower().Equals(sqlDataReader["answer"].ToString().ToLower()))
                                    {
                                        result = true;
                                        ans_attempted += 1;
                                    }
                                    else
                                    {
                                        result = false;
                                        ans_attempted += 1;
                                    }
                                }

                            }
                            
                        }
                        catch(Exception e)
                        {
                            return e.ToString();

                        }
                    }
                    else
                    {
                        if (!ans.useransText.Equals(""))
                        {
                            try
                            {
                                using (SqlConnection connection = new SqlConnection(connectionstring))
                                {
                                    connection.Open();
                                    string ansCommand = "Select * from answers where answer_id=" + ans.ansId;
                                    SqlCommand sqlAnsCommand = new SqlCommand(ansCommand, connection);
                                    SqlDataReader sqlDataReader = sqlAnsCommand.ExecuteReader();
                                    while (sqlDataReader.Read())
                                    {

                                        result = result && Convert.ToBoolean(sqlDataReader["iscorrect"]);
                                        ans_attempted += 1;
                                    }

                                }
                               
                            }
                            catch (Exception e)
                            {
                                return e.ToString();

                            }
                        }
                    }
                }
                if (ans_attempted <= 0)
                {
                    result = false;
                    ques_attempt -= 1;
                }
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionstring))
                    {
                        connection.Open();
                        string ansCommand = "INSERT INTO user_result values(" + quiz.quizId + ","+quiz.userId+ "," + ques.questionId + ",'" + result + "')";
                        SqlCommand sqlAnsCommand = new SqlCommand(ansCommand, connection);
                        sqlAnsCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    return e.ToString();

                }

            }
            float count=0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string resCommand = "SELECT count(*) from user_result where quiz_id="+quiz.quizId+" and user_id="+quiz.userId+"and result='true'";
                    SqlCommand sqlresCommand = new SqlCommand(resCommand, connection);
                    count = Convert.ToSingle(sqlresCommand.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                return e.ToString();

            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    string ansCommand = "INSERT INTO quiz_attempt values(" + quiz.quizId + "," + quiz.userId + ","+count+")";
                    SqlCommand sqlAnsCommand = new SqlCommand(ansCommand, connection);
                    sqlAnsCommand.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                return e.ToString();

            }
            outputString += "##{Sc:"+ count+",Qa:"+ques_attempt+"}";
            return outputString;
        }  
    }
}

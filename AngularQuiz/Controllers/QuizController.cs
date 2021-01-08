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
    [EnableCors(origins:"*", headers:"*", methods:"*")]
    public class QuizController : ApiController
    {
        public Quiz Get(int id)
        {
            Quiz quizObj = new Quiz();
            string connectionstring = "Data Source=localhost;Initial Catalog = quizzie; User ID = sa; Password = Vardhini@98!; MultipleActiveResultSets=True";
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string quizCommand = "SELECT * FROM quiz where quiz_id=" + id + "";
                SqlCommand sqlQuizCommand = new SqlCommand(quizCommand, connection);
                SqlDataReader sqlDataReader = sqlQuizCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    quizObj.quizId = Convert.ToInt32(sqlDataReader["quiz_id"]);
                    quizObj.userId = Convert.ToInt32(sqlDataReader["user_id"]);
                    quizObj.quizName = sqlDataReader["quiz_name"].ToString();
                    quizObj.quizDesc = sqlDataReader["description"].ToString();
                    quizObj.quizSubject = sqlDataReader["subject"].ToString();
                    quizObj.isActive = Convert.ToBoolean(sqlDataReader["inactive"]);
                    quizObj.questionsCount = Convert.ToInt32(sqlDataReader["questions"]);
                    Console.WriteLine(quizObj.quizName, quizObj.questionsCount);
                    string questionsCommand = "SELECT * FROM question where quiz_id=" + id + "";
                    SqlCommand sqlQuestionsCommand = new SqlCommand(questionsCommand, connection);
                    SqlDataReader sqlDataReader1 = sqlQuestionsCommand.ExecuteReader();
                    List<QuestionForm> questionforms = new List<QuestionForm>();
                    while (sqlDataReader1.Read())
                    {
                        Console.WriteLine(sqlDataReader1);
                        QuestionForm quesObj = new QuestionForm();
                        quesObj.questionId = Convert.ToInt32(sqlDataReader1["question_id"]);
                        quesObj.question = sqlDataReader1["question_name"].ToString();

                        Console.WriteLine(quesObj.questionId.ToString(), quesObj.question);
                        quesObj.answertype = sqlDataReader1["answer_type"].ToString();
                        Console.WriteLine(quesObj.answertype);
                        string answersCommand = "SELECT * FROM answers where quiz_id=" + id + " and question_id=" + quesObj.questionId + "";
                        SqlCommand sqlAnswersCommand = new SqlCommand(answersCommand, connection);
                        SqlDataReader sqlDataReader2 = sqlAnswersCommand.ExecuteReader();
                        List<Answers> answers = new List<Answers>();

                        while (sqlDataReader2.Read())
                        {
                            Console.WriteLine(sqlDataReader2);
                            Answers ansObj = new Answers();
                            ansObj.ansId = Convert.ToInt32(sqlDataReader2["answer_id"]);
                            ansObj.ansText = sqlDataReader2["answer"].ToString();
                            ansObj.iscorrect = Convert.ToBoolean(sqlDataReader2["iscorrect"]);
                            answers.Add(ansObj);
                        }
                        quesObj.answers = answers;

                        questionforms.Add(quesObj);
                    }
                    quizObj.questionforms = questionforms;
                }
            }
            return quizObj;
        }
    }   
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularQuiz.Models.QuizAnswers
{
    public class User
    {
        public int userId { get; set; }
        public int userName { get; set; }
        public int fullName { get; set; }
        public int email { get; set; }
        public int password { get; set; }
        public int organisation { get; set; }
    }
    public class Quiz
    {
        public int userId { get; set; }
        public int quizId { get; set; }
        public string quizName { get; set; }
        public string quizDesc { get; set; }
        public string quizSubject { get; set; }
        public int questionsCount { get; set; }
        public bool isActive { get; set; }
        public List<QuestionForm> questionforms { get; set; }
    }
    public class QuestionForm
    {
        public int questionId { get; set; }
        public string question { get; set; }
        public string answertype { get; set; }

        public List<UserAnswers> answers { get; set; }

    }
    public class UserAnswers
    {
        public int ansId { get; set; }
        public string ansText { get; set; }
        public string useransText { get; set; }

    }
}
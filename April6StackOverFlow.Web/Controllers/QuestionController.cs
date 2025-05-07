using May6StackOverFlow.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace May6StackOverFlow.Web.Controllers
{
    public class QuestionController : Controller
    {
        private string _connectionString;

        public QuestionController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [Authorize]
        public IActionResult AskQuestion()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Add(Question question, List<string> tags)
        {
            question.DatePosted = DateTime.Now;
            question.UserId = GetCurrentUserId();
            var repo = new QuestionsAnswerRepository(_connectionString);
            repo.AddQuestion(question, tags);
            return Redirect("/home");
        }
        public IActionResult ViewQuestion(int id)
        {
            var repo = new QuestionsAnswerRepository(_connectionString);
            

        var question = repo.GetQuestionsById(id);
         if(question == null)
            {
                return Redirect("/home");
            }
            return View(question);
        }
        [Authorize]
        public IActionResult AddAnswer(Answer answer)
        {
            var repo = new QuestionsAnswerRepository(_connectionString);
            answer.Date = DateTime.Now;
            int userId = GetCurrentUserId();
            answer.UserId = userId;
            repo.AddAnswer(answer);
          return  Redirect($"/question/viewquestion?id={answer.QuestionId}");
        }
        private int GetCurrentUserId()
        {
            var repo = new UserRepository(_connectionString);
            
            var user = repo.GetByEmail(User.Identity.Name);
           
            return user.Id;
        }


    }
}

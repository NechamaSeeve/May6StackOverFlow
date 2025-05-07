using April6StackOverFlow.Web.Models;
using May6StackOverFlow.Data;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace April6StackOverFlow.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;
        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var repo = new QuestionsAnswerRepository(_connectionString);
           
            List<Question> questions = repo.GetAllQuestions();
            return View(questions);
        }

       
    }
}

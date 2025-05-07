using Microsoft.AspNetCore.Mvc;

namespace May6StackOverFlow.Web.Controllers
{
    public class Question : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

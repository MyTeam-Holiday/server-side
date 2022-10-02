using Microsoft.AspNetCore.Mvc;

namespace myteam.holiday.WebApi.Controllers
{
    public class VacationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

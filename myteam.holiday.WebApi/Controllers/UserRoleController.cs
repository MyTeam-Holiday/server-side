using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace myteam.holiday.WebApi.Controllers
{
    public class UserRole : Controller
    {
        // GET: UserRole
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserRole/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserRole/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRole/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserRole/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserRole/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserRole/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserRole/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

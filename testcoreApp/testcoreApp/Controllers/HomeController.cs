using Microsoft.AspNetCore.Mvc;
using testcoreApp.Models;


namespace testcoreApp.Controllers
{
    public class HomeController : Controller
    {
        //public string Myname()
        //{
        //    return "Asser yehia";
        //}
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Privacy()
        {
            return View();
        }
        public IActionResult Test()
        {
            return Content("Server is running!");
        }
        [HttpGet]
        public ViewResult employees()
        {
            return View();
        }
        [HttpPost]
        public ViewResult employees(Employee employee) //Employee is a class in controller
        {
            if (ModelState.IsValid) { 
            Repository.AddEmployee(employee);
            return View("ConfirmPage", employee);
            }
            else return View();
        }

        public ViewResult AllEmployees()
        {
            return View(Repository.GetEmployees().Where(emp => emp.IsActive == true));
        }
    }
}

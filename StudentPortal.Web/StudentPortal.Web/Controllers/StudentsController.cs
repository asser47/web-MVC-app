using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;

namespace StudentPortal.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentsViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed,
            };
            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync();
            return View(students);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await dbContext.Students.FindAsync(id);
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel); 
            }

            var student = await dbContext.Students.FindAsync(viewModel.Id);
            if (student is null)
            {
                return NotFound();
            }

            student.Name = viewModel.Name;
            student.Email = viewModel.Email;
            student.Phone = viewModel.Phone;
            student.Subscribed = viewModel.Subscribed;
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List", "Students");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Student viewmodel)
        {
            var student = await dbContext.Students.AsNoTracking().
                FirstOrDefaultAsync(x=> x.Id == viewmodel.Id);
            if (student is not null)
            {
                dbContext.Students.Remove(viewmodel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }
    }

    }

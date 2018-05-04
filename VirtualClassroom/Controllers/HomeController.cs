using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VirtualClassroom.CommonAbstractions;
using VirtualClassroom.Core.Shared;
using VirtualClassroom.Domain;
using VirtualClassroom.Models;

namespace VirtualClassroom.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthentication _authentication;
        private readonly IProfessorServices _professorServices;
        private readonly IStudentServices _studentServices;

        public HomeController(
            IAuthentication authentication,
            IProfessorServices professorServices,
            IStudentServices studentServices)
        {
            _authentication = authentication;
            _professorServices = professorServices;
            _studentServices = studentServices;
        }

        public IActionResult Index()
        {
            if (!_authentication.IsUserLoggedIn(User))
                return Redirect($"Account/Login");

            UserData user = _authentication.GetUserByAssociatedUser(User);
            if (_authentication.IsProfessor(User))
            {
                Professor professor = _professorServices.GetAllProfessors().ToList().Find(prof => prof.Email == user.Email);

                return Redirect($"Professors/{professor.Id}/Activities");
            }

            if (_authentication.IsStudent(User))
            {
                Student student = _studentServices.GetAllStudents().ToList().Find(stud => stud.Email == user.Email);

                return Redirect($"Students/{student.Id}/Activities");
            }

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

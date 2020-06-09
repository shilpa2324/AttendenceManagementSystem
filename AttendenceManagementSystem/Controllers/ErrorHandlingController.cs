using AttendenceManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace AttendenceManagementSystem.Controllers
{
    public class ErrorHandlingController : Controller
    {
        public IActionResult Error(ErrorViewModel errorView)
        {
            return View(errorView);
        }
    }
}
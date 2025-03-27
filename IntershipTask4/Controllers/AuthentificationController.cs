using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntershipTask4.Controllers
{
    public class AuthentificationController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View("Login");
        }
    }
}

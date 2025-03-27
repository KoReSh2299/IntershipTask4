using IntershipTask4.Application.Dtos;
using IntershipTask4.Application.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntershipTask4.Web.Controllers
{
    public class RegistrationController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        public IActionResult Index()
        {
            return View(new UserForCreationDto());
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserForCreationDto user)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _mediator.Send(new CreateUserCommand(user));
                    return RedirectToAction("Login", "Authentification");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(user);
        }
    }
}

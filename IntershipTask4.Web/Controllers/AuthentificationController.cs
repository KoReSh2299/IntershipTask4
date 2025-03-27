using IntershipTask4.Application.Dtos;
using IntershipTask4.Application.Requests.Commands;
using IntershipTask4.Application.Requests.Queries.JwtToken;
using IntershipTask4.Application.Requests.Queries.Users;
using IntershipTask4.Infrastructure.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace IntershipTask4.Web.Controllers
{
    public class AuthentificationController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        public IActionResult Login()
        {
            return View(new UserForLoginDto());
        }

        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("jwtToken");
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserForLoginDto dto)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var user = await _mediator.Send(new GetUserByEmailQuery(dto.Email, new NotDeletedUserSpecification())) ?? throw new Exception("Can't find your account.");

                    if (!user.IsActive)
                    {
                        throw new Exception("Sorry, your account is blocked.");
                    }
                    
                    await _mediator.Send(new UpdateUserCommand(new UserForUpdateDto() 
                    { 
                        LastLoginTime = DateTime.Now, 
                        IsActive = user.IsActive,
                        Id = user.Id
                    }));

                    var token = await _mediator.Send(new GetJwtTokenQuery(dto));
                    var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
                    HttpContext.Response.Cookies.Append("jwtToken", tokenStr, new CookieOptions()
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict
                    });
                    return RedirectToAction("Index", "Users");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(dto);
        }
    }
}

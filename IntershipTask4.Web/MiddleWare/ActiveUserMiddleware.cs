using IntershipTask4.Application.Requests.Queries.Users;
using IntershipTask4.Domain.abstractions;
using IntershipTask4.Infrastructure.Filters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IntershipTask4.Web.MiddleWare
{

    public class ActiveUserMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context, IMediator mediator)
        {
            var user = context.User;
            if (user.Identity?.IsAuthenticated == true)
            {
                var userEmailClaim = user.FindFirst(ClaimTypes.Email)?.Value;

                if (userEmailClaim != null)
                {
                    var userFromDb = await mediator.Send(new GetUserByEmailQuery(userEmailClaim, new AllUserSpecification()));

                    if (userFromDb == null || !userFromDb.IsActive) 
                    {
                        context.Response.Cookies.Delete("jwtToken");
                        context.Response.Redirect("/Authentification/Login");
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}

using AutoMapper;
using IntershipTask4.Application.Dtos;
using IntershipTask4.Application.Requests.Commands;
using IntershipTask4.Application.Requests.Queries.Users;
using IntershipTask4.Domain.Entities;
using IntershipTask4.Infrastructure.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace IntershipTask4.Web.Controllers
{
    [Authorize]
    public class UsersController(IMediator mediator, IMapper mapper) : Controller
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        public async Task<IActionResult> Index()
        {
            var users = await _mediator.Send(new GetUsersQuery(new AllUserSpecification()));
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Block(List<int> userIds, string selectAll)
        {
            try
            {
                if (!string.IsNullOrEmpty(selectAll) && selectAll == "on")
                {
                    var users = (await _mediator.Send(new GetUsersQuery(new NotBlockedUserSpecification() & new NotDeletedUserSpecification()))).Select(x => _mapper.Map<UserForUpdateDto>(x)).ToList();

                    foreach (var user in users)
                    {
                        user.IsActive = false;
                        await _mediator.Send(new UpdateUserCommand(user));
                    }
                }
                else if (userIds != null && userIds.Count != 0)
                {
                    foreach (var item in userIds)
                    {
                        var user = _mapper.Map<UserForUpdateDto>(await _mediator.Send(new GetUserByIdQuery(item, new AllUserSpecification())));
                        user.IsActive = false;
                        await _mediator.Send(new UpdateUserCommand(user));
                    }
                }

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var users = await _mediator.Send(new GetUsersQuery(new AllUserSpecification()));
                return View("Index", users);
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Unblock(List<int> userIds, string selectAll)
        {
            try
            {
                if (!string.IsNullOrEmpty(selectAll) && selectAll == "on")
                {
                    var users = (await _mediator.Send(new GetUsersQuery(new BlockedUserSpecification()))).Select(x => _mapper.Map<UserForUpdateDto>(x)).ToList();

                    foreach (var user in users)
                    {
                        user.IsActive = true;
                        await _mediator.Send(new UpdateUserCommand(user));
                    }
                }
                else if (userIds != null && userIds.Count != 0)
                {
                    foreach (var item in userIds)
                    {
                        var user = _mapper.Map<UserForUpdateDto>(await _mediator.Send(new GetUserByIdQuery(item, new BlockedUserSpecification())));
                        user.IsActive = true;
                        await _mediator.Send(new UpdateUserCommand(user));
                    }
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var users = await _mediator.Send(new GetUsersQuery(new AllUserSpecification()));
                return View("Index", users);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(List<int> userIds, string selectAll)
        {
            try
            {
                if (!string.IsNullOrEmpty(selectAll) && selectAll == "on")
                {
                    var users = await _mediator.Send(new GetUsersQuery(new NotDeletedUserSpecification()));
                    foreach (var user in users)
                    {
                        await _mediator.Send(new DeleteUserCommand(_mapper.Map<UserForDeleteDto>(user)));
                    }
                }
                else if (userIds != null && userIds.Count != 0)
                {
                    foreach(var item in userIds)
                    {
                        await _mediator.Send(new DeleteUserCommand(new UserForDeleteDto() { Id = item }));
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var users = await _mediator.Send(new GetUsersQuery(new AllUserSpecification()));
                return View("Index", users);
            }

            return RedirectToAction("Index");
        }
    }
}

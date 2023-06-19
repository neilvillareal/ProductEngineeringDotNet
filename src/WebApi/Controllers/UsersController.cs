using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Command;
using Core.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get user by providing a specific Id
        /// </summary>
        /// <param name="id">Id of the User</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));

            if (result is null)
                return NotFound("User do not exists");

            return Ok(result);
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            var result = await _mediator.Send(new CreateUserCommand(user));

            return Ok(result);
        }

        /// <summary>
        /// Update user data
        /// </summary>
        /// <param name="id">Id of the User</param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] User user, int id)
        {
            var result = await _mediator.Send(new UpdateUserCommand(id, user));

            return Ok(result);
        }

    }
}


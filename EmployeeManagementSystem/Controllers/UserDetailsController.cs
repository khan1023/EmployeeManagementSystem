using EMS.Application.User.Command;
using EMS.Application.User.Query;
using EMS.Application.Validators;
using EMS.Domain.Entities;
using EMS.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GetAllUsers it will Returns All Active Users
        [HttpGet("GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<UserDetails>> GetAllUsers()
        {
            return await _mediator.Send(new GetAllUserQuery());

        }

        // GetUser it will Returns Active Users based on ID
        [HttpGet("GetUser/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDetails>> GetUser(int id)
        {
            return await _mediator.Send(new GetUserByIdQuery(id));
        }

        // CreateUser it wil Create New User
        [HttpPost("CreateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateUser([FromForm] CreateUserCommand command)
        {
           
                var validator = new CreateUserValidator();
                var validationResult = await validator.ValidateAsync(command);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                var result = await _mediator.Send(command);
                if(result.isduplicate)
                    throw new ApiException(StatusCodes.Status400BadRequest, "Please Enter Unique User Name or Email Id.");
                return Ok();
            
;
            }
        

        // EditUser it wil Update User Details
        [HttpPut("EditUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> EditUser([FromForm] EditUserCommand command)
        {
 
                var validator = new UpdateUserValidator();
                var validationResult = await validator.ValidateAsync(command);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                var result = await _mediator.Send(command);
                if (result.isduplicate)
                    throw new ApiException(StatusCodes.Status400BadRequest, "Please Enter Unique User Name or Email Id.");
                return Ok(result);
            
        }
        // DeleteUser it wil Delete User Details
        [HttpDelete("DeleteUser/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteUserCommand(id));
                return Ok();
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }
        // GetAllUsers it will Returns All Active Users
        [HttpGet("GetAllLocations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<System.Web.Mvc.SelectListItem>> GetAllLocations()
        {
            return await _mediator.Send(new GetAllLocationsQuery());
        }
    }
}

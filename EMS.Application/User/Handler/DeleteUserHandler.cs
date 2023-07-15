using EMS.Application.Repository.User;
using EMS.Application.Response;
using EMS.Application.User.Command;
using EMS.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EMS.Application.User.Handler
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, UserResponse>
    {
        private readonly IUserCommandRepository _userCommandRepository; 
        public DeleteUserHandler(IUserCommandRepository userRepository)
        {
            _userCommandRepository = userRepository; 
        }

        public async Task<UserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            UserResponse response = new UserResponse();
            try
            {
                await _userCommandRepository.DeleteAsync(request.Id);
            }
            catch (Exception ex)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError, "Error While Delete User");
            }
            response.Message = "User information has been deleted!";
            return response;
        }
    }
}

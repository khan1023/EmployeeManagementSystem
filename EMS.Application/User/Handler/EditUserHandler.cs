using EMS.Application.Mapper;
using EMS.Application.Repository.User;
using EMS.Application.Response;
using EMS.Application.User.Command;
using EMS.Domain.Entities;
using EMS.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EMS.Application.User.Handler
{
    public class EditUserHandler : IRequestHandler<EditUserCommand, UserResponse>
    {
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private IHostingEnvironment _environment;
        public EditUserHandler(IUserCommandRepository userRepository, IUserQueryRepository userQueryRepository, IHostingEnvironment hostingEnvironment)
        {
            _userCommandRepository = userRepository;
            _userQueryRepository = userQueryRepository;
            _environment = hostingEnvironment;
            
        }
        public async Task<UserResponse> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                UserResponse response = new UserResponse();
                if (await _userQueryRepository.CheckDuplicateAsync(request.Name, request.Email, request.Id) == 0)
                {
                    var userObj = UserMapper.Mapper.Map<UserDetails>(request);

                    string docpath = Path.Combine(_environment.ContentRootPath, "Uploads/Documents");
                    if (!Directory.Exists(docpath))
                    {
                        Directory.CreateDirectory(docpath);
                    }
                    string imagepath = Path.Combine(_environment.ContentRootPath, "Uploads/UserImages");
                    if (!Directory.Exists(imagepath))
                    {
                        Directory.CreateDirectory(imagepath);
                    }
                    if (request.PassportFile != null)
                    {
                        string fileName = request.Name + "_Passport" + Path.GetExtension(request.PassportFile.FileName);
                        using (FileStream stream = new FileStream(Path.Combine(docpath, fileName), FileMode.Create))
                        {
                            request.PassportFile.CopyTo(stream);
                            userObj.PassportFile = "/Uploads/Documents/" + fileName;
                        }
                    }

                    if (request.EmirateIdFile != null)
                    {
                        string fileName = request.Name + "_EmirateId" + Path.GetExtension(request.EmirateIdFile.FileName);
                        using (FileStream stream = new FileStream(Path.Combine(docpath, fileName), FileMode.Create))
                        {
                            request.EmirateIdFile.CopyTo(stream);
                            userObj.EmirateIdFile = "/Uploads/Documents/" + fileName;
                        }
                    }

                    if (request.DrivingLicenceFile != null)
                    {
                        string fileName = request.Name + "_DrivingLicence" + Path.GetExtension(request.DrivingLicenceFile.FileName);
                        using (FileStream stream = new FileStream(Path.Combine(docpath, fileName), FileMode.Create))
                        {
                            request.DrivingLicenceFile.CopyTo(stream);
                            userObj.DrivingLicenceFile = "/Uploads/Documents/" + fileName;
                        }
                    }
                    if (request.PersonPhoto != null)
                    {
                        string fileName = request.Name + "_PersonPhoto" + Path.GetExtension(request.PersonPhoto.FileName);
                        using (FileStream stream = new FileStream(Path.Combine(imagepath, fileName), FileMode.Create))
                        {
                            request.PersonPhoto.CopyTo(stream);
                            userObj.PersonPhoto = "/Uploads/UserImages/" + fileName;
                        }
                    }
                    if (userObj is null)
                    {
                        throw new ApiException(StatusCodes.Status500InternalServerError, "There is a problem in mapper");
                    }

                    var userStatus = await _userCommandRepository.UpdateAsync(userObj);
                    if (userStatus.DataId > 0)
                    {
                        response.Message = "User Details has been Updated Successfully";
                    }
                    else
                    {
                        throw new ApiException(StatusCodes.Status400BadRequest, "User Details Not Updated.");
                    }
                }
                else
                {
                    response.Message = "Please Enter Unique User Name or Email Id.";
                    response.isduplicate = true;
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError, "Error While Updating User");
            }
        }
    }
}

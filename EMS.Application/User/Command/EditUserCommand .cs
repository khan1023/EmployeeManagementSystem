using EMS.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Application.User.Command
{
    public class EditUserCommand : IRequest<UserResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmployeeType { get; set; }
        public long MobileNo { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string LocationId { get; set; }
        public string Nationality { get; set; }
        public string Designation { get; set; }
        public string PassportNo { get; set; }
        public string PassportExpireDate { get; set; }
        public IFormFile PassportFile { get; set; }
        public IFormFile EmirateIdFile { get; set; }
        public IFormFile DrivingLicenceFile { get; set; }
        public IFormFile PersonPhoto { get; set; }
    }
}

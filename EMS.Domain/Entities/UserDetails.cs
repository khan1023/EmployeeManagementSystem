using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Domain.Entities
{
    public class UserDetails : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public long MobileNo { get; set; }
        public string Email { get; set; } = string.Empty;
        public string EmployeeType { get; set; } = string.Empty;
        public int LocationId { get; set; }
        public string Designation { get; set; } = string.Empty;
        public string LocationName { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string PassportNo { get; set; } = string.Empty;
        public string PassportExpireDate { get; set; } = string.Empty;
        public string PassportFile { get; set; } = string.Empty;
        public string EmirateIdFile { get; set; } = string.Empty;
        public string DrivingLicenceFile { get; set; } = string.Empty;
        public string PersonPhoto { get; set; } = string.Empty;
        public bool IsActive { get; set; }

    }
}

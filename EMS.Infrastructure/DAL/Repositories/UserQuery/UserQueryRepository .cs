using Dapper;
using EMS.Application.Repository.User;
using EMS.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EMS.Infrastructure.DAL.Repositories.UserQuery
{
    public class UserQueryRepository : IUserQueryRepository
    {
        private readonly IConfiguration _configuration;
        public UserQueryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<UserDetails>> GetAllAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("EMSConnection")))
                {
                    var query = "SP_GetAllUsers";
                    return (await connection.QueryAsync<UserDetails>(query,commandType:CommandType.StoredProcedure)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        public async Task<UserDetails> GetByIdAsync(int id)
        {
            try
            {
                var query = "SELECT UD.id AS Id, UD.name AS Name,UD.mobile_number AS MobileNo,UD.emailId AS EmailId,UD.employee_type AS EmployeeType,"
                                + "UD.nationality AS Nationality,UD.designation AS Designation,UD.passport_number AS PassportNo,UD.passport_file_url AS PassportFile,"
                                + "UD.emirate_id_file_url AS EmirateIdFile,UD.driving_licence_file_url AS DrivingLicenceFile,UD.passport_expire_date AS PassportExpireDate,"
                                + "UD.person_photo_file_url AS PersonPhoto,UD.location_id AS LocationId FROM user_details UD"
                                + "JOIN location_master LM ON LM.id = UD.location_id WHERE UD.id=@id AND UD.is_active = 1;";
                var parameters = new DynamicParameters();
                parameters.Add("id", id, DbType.Int32);

                using (var connection = new SqlConnection(_configuration.GetConnectionString("EMSConnection")))
                {
                    return (await connection.QueryFirstOrDefaultAsync<UserDetails>(query, parameters));

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SelectListItem>> GetAllLocations()
        {
            try
            {
                var query = "SELECT id AS Value ,location_name As Text FROM location_master where is_active =1;";


                using (var connection = new SqlConnection(_configuration.GetConnectionString("EMSConnection")))
                {
                    return (await connection.QueryAsync<SelectListItem>(query)).ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CheckDuplicateAsync(string name, string emailId, int id = 0)
        {
            try
            {
                var parameters = new DynamicParameters();
               
                var query = "SELECT COUNT(id) as val FROM user_details WHERE is_active = 1 AND name='" + name+"' AND emailId ='"+emailId+"'";
                
                if (id > 0)
                {
                    query = query + "AND id !="+id+"";
                }

                using (var connection = new SqlConnection(_configuration.GetConnectionString("EMSConnection")))
                {
                    return await connection.QueryFirstOrDefaultAsync<int>(query);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

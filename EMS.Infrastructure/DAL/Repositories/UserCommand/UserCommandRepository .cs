using Dapper;
using EMS.Application.Repository.User;
using EMS.Application.Response;
using EMS.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Infrastructure.DAL.Repositories.UserCommand
{
    public class UserCommandRepository : IUserCommandRepository
    {
        private readonly IConfiguration _configuration;
        public UserCommandRepository(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        private IDbConnection Connection 
        { 
            get { 
                return new SqlConnection(_configuration.GetConnectionString("EMSConnection")); 
            } 
        }
        public async Task<UserResponse> AddAsync(UserDetails obj)
        {
            UserResponse response = new UserResponse();
            using (var connection = Connection)
            {
                try
                {
                    //Set up DynamicParameters object to pass parameters
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Name", obj.Name, DbType.String);
                    parameters.Add("@Mobile_Number", obj.MobileNo, DbType.Int64);
                    parameters.Add("@Email_Id", obj.Email, DbType.String);
                    parameters.Add("@Employee_Type", obj.EmployeeType, DbType.String);
                    parameters.Add("@Nationality", obj.Nationality, DbType.String);
                    parameters.Add("@Designation", obj.Designation, DbType.String);
                    parameters.Add("@Passport_number", obj.PassportNo, DbType.String);
                    parameters.Add("@Passport_File_Url", obj.PassportFile, DbType.String);
                    parameters.Add("@Emirate_Id_File_Url", obj.EmirateIdFile, DbType.String);
                    parameters.Add("@Driving_Licence_File_Url", obj.DrivingLicenceFile, DbType.String);
                    parameters.Add("@Passport_Expire_Date", obj.PassportExpireDate, DbType.DateTime);
                    parameters.Add("@Person_Photo_Url",obj.PersonPhoto, DbType.String);
                    parameters.Add("@Created_Date",obj.CreatedDate, DbType.DateTime);
                    parameters.Add("@Location_Id", obj.LocationId);
                    parameters.Add("@out_Status", dbType: DbType.Int32, direction: ParameterDirection.Output);


                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            //Execute stored procedure  
                            await connection.ExecuteAsync("SP_AddUserDetails", parameters, transaction, commandType: CommandType.StoredProcedure);

                            transaction.Commit(); //transaction Commit 
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); //Or rollback 
                            throw ex;
                        }
                    }

                    response.DataId =  parameters.Get<int>("@out_Status");
                    return response;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                }
            }

        }

        public async Task<UserResponse> UpdateAsync(UserDetails obj)
        {
            UserResponse response = new UserResponse();
            using (var connection = Connection)
            {
                try
                {
                    //Set up DynamicParameters object to pass parameters
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@User_Id", obj.Id, DbType.Int32);
                    parameters.Add("@Name", obj.Name, DbType.String);
                    parameters.Add("@Mobile_Number", obj.MobileNo, DbType.Int64);
                    parameters.Add("@Email_Id", obj.Email, DbType.String);
                    parameters.Add("@Employee_Type", obj.EmployeeType, DbType.String);
                    parameters.Add("@Nationality", obj.Nationality, DbType.String);
                    parameters.Add("@Designation", obj.Designation, DbType.String);
                    parameters.Add("@Passport_number", obj.PassportNo, DbType.String);
                    parameters.Add("@Passport_File_Url", obj.PassportFile, DbType.String);
                    parameters.Add("@Emirate_Id_File_Url", obj.EmirateIdFile, DbType.String);
                    parameters.Add("@Driving_Licence_File_Url", obj.DrivingLicenceFile, DbType.String);
                    parameters.Add("@Passport_Expire_Date", obj.PassportExpireDate=="null"?null: obj.PassportExpireDate, DbType.DateTime);
                    parameters.Add("@Person_Photo_Url", obj.PersonPhoto, DbType.String);
                    parameters.Add("@Updated_Date",obj.ModifiedDate, DbType.DateTime);
                    parameters.Add("@Location_Id", obj.LocationId);
                    parameters.Add("@out_Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            //Execute stored procedure  
                            await connection.ExecuteAsync("SP_UpdateUserDetails", parameters, transaction, commandType: CommandType.StoredProcedure);
                            transaction.Commit(); //transaction Commit 
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();//Or rollback 
                            throw ex;
                        }

                    }
                    response.DataId = parameters.Get<int>("@out_Status");
                    return response;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                }
            }
        }

        public async Task<UserResponse> DeleteAsync(int id)
        {
            UserResponse response = new UserResponse();
            using (var connection = Connection)
            {
                try
                {
                    //Set up DynamicParameters object to pass parameters
                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@User_Id", id);
                    parameters.Add("@Updated_Date", DateTime.Now, dbType: DbType.DateTime);
                    parameters.Add("@outUserId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            //Execute stored procedure and map the returned result to a Customer object  
                            await connection.ExecuteAsync("SP_DeleteUserDetails", parameters, transaction, commandType: CommandType.StoredProcedure);
                            transaction.Commit(); //transaction Commit 
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();//Or rollback 
                            throw ex;
                        }

                    }


                    response.DataId = parameters.Get<int>("@outUserId");
                    return response;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                }
            }
        }
    }
}

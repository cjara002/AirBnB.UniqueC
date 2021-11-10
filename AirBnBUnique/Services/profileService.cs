using AirBnBUnique.Interfaces;
using AirBnBUnique.Models.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AirBnBUnique.Services
{
    public class ProfileService: IProfile
    {
        private readonly IConfiguration _configuration;

        public ProfileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<Profile> GetAll()
        {
            List<Profile> profileList = null;

            string connectionString = _configuration.GetConnectionString("Default");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();

                //Name of stored proc
                sqlCommand.CommandText = "AirBnBProfile_SelectAll";
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                using(IDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Profile model = Mapper(reader);
                        if(profileList == null)
                        {
                            profileList = new List<Profile>();
                        }
                        profileList.Add(model);
                    }
                }
            }

            return profileList;
        }

        private static Profile Mapper(IDataReader reader)
        {
            Profile model = new Profile();
            int startingIndex = 0;

            //look at video  for null values
            model.Id = reader.GetInt32(startingIndex++);
            model.UserId = reader.GetInt32(startingIndex++);
            model.Name = reader.GetString(startingIndex++);
            model.YearsInOperation = reader.GetInt32(startingIndex++);
            model.ImageUrl = reader.GetString(startingIndex++);
            model.DateCreated = reader.GetDateTime(startingIndex++);
            model.DateModified = reader.GetDateTime(startingIndex++);
            model.CreatedBy = reader.GetInt32(startingIndex++);
            model.ModifiedBy = reader.GetInt32(startingIndex++);
            model.City = reader.GetString(startingIndex++);
            model.Description = reader.GetString(startingIndex++);

            return model;
        }
    }
}

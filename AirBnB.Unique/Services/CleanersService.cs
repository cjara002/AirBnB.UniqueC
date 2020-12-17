using AirBnB.Unique.Interfaces;
using AirBnB.Unique.Models.Domain;
using AirBnB.Unique.Models.Request;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AirBnB.Unique.Services
{
    public class CleanersService: ICleaners
    {
        private readonly IConfiguration _configuration;

        public CleanersService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Update(CleanersUpdateRequest model, int Id)
        {
           string connectionString = _configuration.GetConnectionString("Default");
          using(SqlConnection connection = new SqlConnection(connectionString))
          {
              string sql = "AirBnBProfile_Update";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", Id);

                    command.Parameters.AddWithValue("@UserId", model.UserId);

                    command.Parameters.AddWithValue("@Name", model.Name);

                    command.Parameters.AddWithValue("@YearsInOperation", model.YearsInOperation);

                    command.Parameters.AddWithValue("@ImageUrl", model.ImageUrl);

                    command.Parameters.AddWithValue("@ModifiedBy", model.UserId);

                    command.Parameters.AddWithValue("@City", model.City);

                    command.Parameters.AddWithValue("@Description", model.Description);
                    connection.Open();
                    command.ExecuteNonQuery();
                   connection.Close();
                }
         }
         }
        public List<Cleaners> GetAll()
        {
            List<Cleaners> cleanersList = null;

            string connectionString = _configuration.GetConnectionString("Default");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();

                
                sqlCommand.CommandText = "AirBnBProfile_SelectAll";

                sqlCommand.CommandType = CommandType.StoredProcedure;

                using (IDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cleaners model = Mapper(reader);
                        if (cleanersList == null)
                        {
                            cleanersList = new List<Cleaners>();
                        }
                        cleanersList.Add(model);
                    }
                }
            }
                return cleanersList;
        }

        public Paged<Cleaners> Paginate(int pageIndex, int pageSize)
        {
            string connectionString = _configuration.GetConnectionString("Default");

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "AirBnBProfile_SelectAll_Paginated";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@pageIndex", pageIndex);
                    command.Parameters.AddWithValue("@pageSize", pageSize);
                    List<Cleaners> list = null;
                    Paged<Cleaners> paged = null;
                    int totalCount = 0;
                    connection.Open();

                    using(SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while(dataReader.Read())
                        {
                            Cleaners model = Mapper(dataReader);
                            if(totalCount == 0)
                            {
                                totalCount = dataReader.GetInt32(11);
                            }
                            if(list == null)
                            {
                                list = new List<Cleaners>();
                            }
                            list.Add(model);

                            if(list != null)
                            {
                                paged = new Paged<Cleaners> (list, pageIndex, pageSize, totalCount);
                            }
                        }
                    }
                    return paged;
                }
            }
        }

        public Paged<Cleaners> SearchPaginate(int pageIndex, int pageSize, string query)
        {
            string connectionString = _configuration.GetConnectionString("Default");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "AirBnBProfile_SelectBySearch_Paginated";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@pageIndex", pageIndex);
                    command.Parameters.AddWithValue("@pageSize", pageSize);
                    command.Parameters.AddWithValue("@query", query);
                    List<Cleaners> list = null;
                    Paged<Cleaners> paged = null;
                    int totalCount = 0;
                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Cleaners model = Mapper(dataReader);
                            if (totalCount == 0)
                            {
                                totalCount = dataReader.GetInt32(11);
                            }
                            if (list == null)
                            {
                                list = new List<Cleaners>();
                            }
                            list.Add(model);

                            if (list != null)
                            {
                                paged = new Paged<Cleaners>(list, pageIndex, pageSize, totalCount);
                            }
                        }
                    }
                    return paged;
                }
            }
        }
        public List<Cleaners> GetById(int Id)
        {

            string connectionString = _configuration.GetConnectionString("Default");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "AirBnBProfile_SelectById";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    //command.Parameters.Add(new SqlParameter ("@Id", Id));
                    command.Parameters.AddWithValue("@Id", Id);
                    List<Cleaners> singleCleaner = null;
                    connection.Open();

                //SqlCommand sqlCommand = connection.CreateCommand();
                //sqlCommand.CommandText = "AirBnBProfile_SelectById";
                //sqlCommand.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                        while (dataReader.Read())
                        {
                            Cleaners model = Mapper(dataReader);
                            if (singleCleaner == null)
                            {
                                singleCleaner = new List<Cleaners>();
                            }
                            singleCleaner.Add(model);
                        }
                    }
                    return singleCleaner;
                }
            }
          
        }

        public int Add(CleanersAddRequest model)
        {
            string connectionString = _configuration.GetConnectionString("Default");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();


                sqlCommand.CommandText = "AirBnBProfile_Insert";

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@UserId", model.UserId);

                sqlCommand.Parameters.AddWithValue("@Name", model.Name);

                sqlCommand.Parameters.AddWithValue("@YearsInOperation", model.YearsInOperation);

                sqlCommand.Parameters.AddWithValue("@ImageUrl", model.ImageUrl);

                sqlCommand.Parameters.AddWithValue("@CreatedBy", model.UserId);

                sqlCommand.Parameters.AddWithValue("@ModifiedBy", model.UserId);

                sqlCommand.Parameters.AddWithValue("@City", model.City);

                sqlCommand.Parameters.AddWithValue("@Description", model.Description);

                SqlParameter IdParam = sqlCommand.Parameters.Add("@Id", SqlDbType.Int);

               // UserIdParam.Direction = ParameterDirection.Output;
                IdParam.Direction = ParameterDirection.Output;


                sqlCommand.ExecuteNonQuery();

                return (int)IdParam.Value;
            }
        }

        public void DeleteById(int Id)
        {
            string connectionString = _configuration.GetConnectionString("Default");
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "AirBnBProfile_Delete";
               using(SqlCommand command = new SqlCommand(sql, connection))
               {
                    command.CommandType = CommandType.StoredProcedure;
                 command.Parameters.AddWithValue("@Id", Id);
                   connection.Open();
                  command.ExecuteNonQuery();
                    connection.Close();
               }
            }
        }


        private static Cleaners Mapper(IDataReader reader)
        {
            Cleaners model = new Cleaners();
            int startingIndex = 0;

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

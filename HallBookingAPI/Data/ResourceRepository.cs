﻿using HallBookingAPI.Models;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace HallBookingAPI.Data
{
    public class ResourceRepository
    {
        private readonly string _connectionString;
        private readonly IWebHostEnvironment _environment;

        public ResourceRepository(IConfiguration connectionString,IWebHostEnvironment environment)
        {
            _connectionString = connectionString.GetConnectionString("ConnectionString");
            _environment = environment;
        }

        public IEnumerable<ResourceDetailModel> GetAllResources()
        {
            List<ResourceDetailModel> resources = new List<ResourceDetailModel>();

            SqlConnection conn = new SqlConnection(_connectionString);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Resource_SelectAll";
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    resources.Add(new ResourceDetailModel()
                    {
                        ResourceID = Convert.ToInt32(sdr["ResourceID"]),
                        ResourceType = sdr["ResourceType"].ToString(),
                        Name = sdr["Name"].ToString(),
                        Location = sdr["Location"].ToString(),
                        PinCode= Convert.ToInt32(sdr["PinCode"]),
                        Capacity = Convert.ToDecimal(sdr["Capacity"]),
                        Description = sdr["Description"].ToString(),
                        PricePerDay = Convert.ToDecimal(sdr["PricePerDay"]),
                        OpenHours = sdr["OpenHours"].ToString(),
                        CloseHours = sdr["CloseHours"].ToString(),
                        IsAvailable = Convert.ToBoolean(sdr["IsAvailable"]),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"]),
                        Latitude = Convert.ToDouble(sdr["Latitude"]),
                        Longitude = Convert.ToDouble(sdr["Longitude"]),
                        UserName = sdr["FullName"].ToString(),
                        CityName = sdr["CityName"].ToString(),
                        StateName = sdr["StateName"].ToString(),
                        CountryName = sdr["CountryName"].ToString()

                    });
                }
            return resources;
        }


        public IEnumerable<ResourceDetailModel> GetResourcesByUserID(int UserID)
        {
            List<ResourceDetailModel> resources = new List<ResourceDetailModel>();

            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_Resource_SelectByUserID";
            cmd.Parameters.AddWithValue("@UserID", UserID);
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                resources.Add(new ResourceDetailModel()
                {
                    ResourceID = Convert.ToInt32(sdr["ResourceID"]),
                    ResourceType = sdr["ResourceType"].ToString(),
                    Name = sdr["Name"].ToString(),
                    Location = sdr["Location"].ToString(),
                    PinCode = Convert.ToInt32(sdr["PinCode"]),
                    Capacity = Convert.ToDecimal(sdr["Capacity"]),
                    Description = sdr["Description"].ToString(),
                    PricePerDay = Convert.ToDecimal(sdr["PricePerDay"]),
                    OpenHours = sdr["OpenHours"].ToString(),
                    CloseHours = sdr["CloseHours"].ToString(),
                    IsAvailable = Convert.ToBoolean(sdr["IsAvailable"]),
                    CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                    UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"]),
                    Latitude = Convert.ToDouble(sdr["Latitude"]),
                    Longitude = Convert.ToDouble(sdr["Longitude"]),
                    UserName = sdr["FullName"].ToString(),
                    CityName = sdr["CityName"].ToString(),
                    StateName = sdr["StateName"].ToString(),
                    CountryName = sdr["CountryName"].ToString()

                });
            }
            return resources;
        }

        public ResourceUploadModel SelectResourceByPK(int ResourceID)
        {
            ResourceUploadModel resource = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Resource_SelectByPK";
                cmd.Parameters.AddWithValue("@ResourceID", ResourceID);

                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    resource = new ResourceUploadModel
                    {
                        ResourceID = Convert.ToInt32(sdr["ResourceID"]),
                        ResourceType = sdr["ResourceType"].ToString(),
                        Name = sdr["Name"].ToString(),
                        Location = sdr["Location"].ToString(),
                        PinCode = Convert.ToInt32(sdr["PinCode"]),
                        CityID= Convert.ToInt32(sdr["CityID"]),
                        CountryID = Convert.ToInt32(sdr["CountryID"]),
                        StateID = Convert.ToInt32(sdr["StateID"]),
                        Capacity = Convert.ToDecimal(sdr["Capacity"]),
                        Description = sdr["Description"].ToString(),
                        PricePerDay = Convert.ToDecimal(sdr["PricePerDay"]),
                        OpenHours = sdr["OpenHours"].ToString(),
                        CloseHours = sdr["CloseHours"].ToString(),
                        IsAvailable = Convert.ToBoolean(sdr["IsAvailable"]),
                        Latitude = Convert.ToDouble(sdr["Latitude"]),
                        Longitude = Convert.ToDouble(sdr["Longitude"]),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"]),
                        UserID = Convert.ToInt32(sdr["UserID"])
                    };
                }
            }

            return resource;
        }

        public bool DeleteResource(int resourceID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Resource_Delete";
                cmd.Parameters.AddWithValue("@ResourceID", resourceID);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool InsertResource(ResourceUploadModel resource)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Resource_Insert";

                cmd.Parameters.AddWithValue("@ResourceType", resource.ResourceType);
                cmd.Parameters.AddWithValue("@Name", resource.Name);
                cmd.Parameters.AddWithValue("@Location", resource.Location);
                cmd.Parameters.AddWithValue("@CountryID", resource.CountryID);
                cmd.Parameters.AddWithValue("@StateID", resource.StateID);
                cmd.Parameters.AddWithValue("@CityID", resource.CityID);
                cmd.Parameters.AddWithValue("@PinCode", resource.PinCode);
                cmd.Parameters.AddWithValue("@Capacity", resource.Capacity);
                cmd.Parameters.AddWithValue("@Description", resource.Description);
                cmd.Parameters.AddWithValue("@PricePerDay", resource.PricePerDay);
                cmd.Parameters.AddWithValue("@OpenHours", resource.OpenHours);
                cmd.Parameters.AddWithValue("@CloseHours", resource.CloseHours);
                cmd.Parameters.AddWithValue("@IsAvailable", resource.IsAvailable);
                cmd.Parameters.AddWithValue("@Latitude", resource.Latitude);
                cmd.Parameters.AddWithValue("@Longitude", resource.Longitude);
                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@UserID", resource.UserID);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool UpdateResource(ResourceUploadModel resource)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Resource_Update";

                cmd.Parameters.AddWithValue("@ResourceID", resource.ResourceID);
                cmd.Parameters.AddWithValue("@ResourceType", resource.ResourceType);
                cmd.Parameters.AddWithValue("@Name", resource.Name);
                cmd.Parameters.AddWithValue("@Location", resource.Location);
                cmd.Parameters.AddWithValue("@CityID", resource.CityID);
                cmd.Parameters.AddWithValue("@CountryID", resource.CountryID);
                cmd.Parameters.AddWithValue("@StateID", resource.StateID);
                cmd.Parameters.AddWithValue("@PinCode", resource.PinCode);
                cmd.Parameters.AddWithValue("@Capacity", resource.Capacity);
                cmd.Parameters.AddWithValue("@Description", resource.Description);
                cmd.Parameters.AddWithValue("@PricePerDay", resource.PricePerDay);
                cmd.Parameters.AddWithValue("@OpenHours", resource.OpenHours);
                cmd.Parameters.AddWithValue("@CloseHours", resource.CloseHours);
                cmd.Parameters.AddWithValue("@IsAvailable", resource.IsAvailable);
                cmd.Parameters.AddWithValue("@Latitude", resource.Latitude);
                cmd.Parameters.AddWithValue("@Longitude", resource.Longitude);
                cmd.Parameters.Add("@UpdatedAt", SqlDbType.DateTime).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@UserID", resource.UserID);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public async Task UploadPropertyWithImage(ResourceUploadModel resource)
        {

            try
            {
                var imagePaths = new List<string>();
                //var propertyId;
                var uploadsFolder = Path.Combine(_environment.ContentRootPath, "Images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                foreach (var image in resource.Images)
                {
                    image.OpenReadStream().Seek(0, SeekOrigin.Begin);
                    var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                    if (string.IsNullOrEmpty(uploadsFolder) || string.IsNullOrEmpty(uniqueFileName))
                    {
                        throw new ArgumentNullException("Path components cannot be null.");
                    }
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }
                    
                    imagePaths.Add(Path.Combine("Images", uniqueFileName).Replace("\\", "/"));
                }

                var imagePathsJson = JsonConvert.SerializeObject(imagePaths);
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand("PR_Resource_AddResourceWithImages", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@ResourceType", resource.ResourceType);
                    cmd.Parameters.AddWithValue("@Name", resource.Name);
                    cmd.Parameters.AddWithValue("@Location", resource.Location);
                    cmd.Parameters.AddWithValue("@CountryID", resource.CountryID);
                    cmd.Parameters.AddWithValue("@StateID", resource.StateID);
                    cmd.Parameters.AddWithValue("@CityID", resource.CityID);
                    cmd.Parameters.AddWithValue("@PinCode", resource.PinCode);
                    cmd.Parameters.AddWithValue("@Capacity", resource.Capacity);
                    cmd.Parameters.AddWithValue("@Description", resource.Description);
                    cmd.Parameters.AddWithValue("@PricePerDay", resource.PricePerDay);
                    cmd.Parameters.AddWithValue("@OpenHours", resource.OpenHours);
                    cmd.Parameters.AddWithValue("@CloseHours", resource.CloseHours);
                    cmd.Parameters.AddWithValue("@IsAvailable", resource.IsAvailable);
                    cmd.Parameters.AddWithValue("@Latitude", resource.Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", resource.Longitude);
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UserID", resource.UserID);
                    cmd.Parameters.AddWithValue("@ImagePaths", imagePathsJson);

                    await connection.OpenAsync();
                    var propertyId = await cmd.ExecuteReaderAsync();
                    foreach (var image in imagePathsJson) { 
                        Console.Write(image.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}

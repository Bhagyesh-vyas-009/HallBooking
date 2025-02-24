using HallBookingAPI.Models;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace HallBookingAPI.Data
{
    public class ResourceRepository
    {
        #region Congfiguration
        private readonly string _connectionString;
        private readonly IWebHostEnvironment _environment;

        public ResourceRepository(IConfiguration connectionString,IWebHostEnvironment environment)
        {
            _connectionString = connectionString.GetConnectionString("ConnectionString");
            _environment = environment;
        }
        #endregion

        #region GetAllResources
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
                        Latitude = sdr["Latitude"].ToString(),
                        Longitude =sdr["Longitude"].ToString(),
                        UserName = sdr["FullName"].ToString(),
                        CityName = sdr["CityName"].ToString(),
                        StateName = sdr["StateName"].ToString(),
                        CountryName = sdr["CountryName"].ToString()

                    });
                }
            return resources;
        }
        #endregion

        #region GetTop10Resources
        public IEnumerable<ResourceDetailModel> GetTop10Resources()
            {
                List<ResourceDetailModel> resources = new List<ResourceDetailModel>();

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "PR_Resource_SelectTop";
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
                            Latitude = sdr["Latitude"].ToString(),
                            Longitude = sdr["Longitude"].ToString(),
                            UserName = sdr["FullName"].ToString(),
                            CityName = sdr["CityName"].ToString(),
                            StateName = sdr["StateName"].ToString(),
                            CountryName = sdr["CountryName"].ToString()
                        });
                    }
                }
                return resources;
            }
            #endregion

        #region GetResourcesByUserID
        public IEnumerable<ResourceDetailModel> GetResourcesByUserID(int UserID)
            {
                List<ResourceDetailModel> resources = new List<ResourceDetailModel>();

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
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
                            Latitude = sdr["Latitude"].ToString(),
                            Longitude = sdr["Longitude"].ToString(),
                            UserName = sdr["FullName"].ToString(),
                            CityName = sdr["CityName"].ToString(),
                            StateName = sdr["StateName"].ToString(),
                            CountryName = sdr["CountryName"].ToString()
                        });
                    }
                }
                return resources;
            }
        #endregion

        #region SelectResourceByPK
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
                            CityID = Convert.ToInt32(sdr["CityID"]),
                            CountryID = Convert.ToInt32(sdr["CountryID"]),
                            StateID = Convert.ToInt32(sdr["StateID"]),
                            Capacity = Convert.ToDecimal(sdr["Capacity"]),
                            Description = sdr["Description"].ToString(),
                            PricePerDay = Convert.ToDecimal(sdr["PricePerDay"]),
                            OpenHours = sdr["OpenHours"].ToString(),
                            CloseHours = sdr["CloseHours"].ToString(),
                            IsAvailable = Convert.ToBoolean(sdr["IsAvailable"]),
                            Latitude = sdr["Latitude"].ToString(),
                            Longitude = sdr["Longitude"].ToString(),
                            CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                            UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"]),
                            UserID = Convert.ToInt32(sdr["UserID"])
                        };
                    }
                }
                return resource;
            }
        #endregion

        #region DeleteResource
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
        #endregion

        #region UploadResourceWithImage
        public async Task UploadResourceWithImage(ResourceUploadModel resource)
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
                    foreach (var image in imagePathsJson)
                    {
                        Console.Write(image.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion

        #region UpdateResourceWithImage
        public async Task UpdateResourceWithImage(ResourceUploadModel resource)
        {
            try
            {
                var imagePaths = new List<string>();
                var uploadsFolder = Path.Combine(_environment.ContentRootPath, "Images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand("PR_ResourceImage_SelectAllByResourceID", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@ResourceID", resource.ResourceID);
                    await connection.OpenAsync();
                    var reader = await cmd.ExecuteReaderAsync();

                    var existingImages = new List<string>();
                    while (reader.Read())
                    {
                        existingImages.Add(reader["ImagePath"].ToString());
                    }
                    await reader.CloseAsync();

                    if (resource.Images != null && resource.Images.Count > 0)
                    {
                        foreach (var image in resource.Images)
                        {
                            image.OpenReadStream().Seek(0, SeekOrigin.Begin);
                            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await image.CopyToAsync(fileStream);
                            }
                            imagePaths.Add(Path.Combine("Images", uniqueFileName).Replace("\\", "/"));
                        }
                    }
                    else
                    {
                        imagePaths = existingImages;
                    }

                    var imagePathsJson = JsonConvert.SerializeObject(imagePaths);
                    var updateCmd = new SqlCommand("PR_Resource_UpdateResourceWithImages", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    updateCmd.Parameters.AddWithValue("@ResourceID", resource.ResourceID);
                    updateCmd.Parameters.AddWithValue("@ResourceType", resource.ResourceType);
                    updateCmd.Parameters.AddWithValue("@Name", resource.Name);
                    updateCmd.Parameters.AddWithValue("@Location", resource.Location);
                    updateCmd.Parameters.AddWithValue("@CountryID", resource.CountryID);
                    updateCmd.Parameters.AddWithValue("@StateID", resource.StateID);
                    updateCmd.Parameters.AddWithValue("@CityID", resource.CityID);
                    updateCmd.Parameters.AddWithValue("@PinCode", resource.PinCode);
                    updateCmd.Parameters.AddWithValue("@Capacity", resource.Capacity);
                    updateCmd.Parameters.AddWithValue("@Description", resource.Description);
                    updateCmd.Parameters.AddWithValue("@PricePerDay", resource.PricePerDay);
                    updateCmd.Parameters.AddWithValue("@OpenHours", resource.OpenHours);
                    updateCmd.Parameters.AddWithValue("@CloseHours", resource.CloseHours);
                    updateCmd.Parameters.AddWithValue("@IsAvailable", resource.IsAvailable);
                    updateCmd.Parameters.AddWithValue("@Latitude", resource.Latitude);
                    updateCmd.Parameters.AddWithValue("@Longitude", resource.Longitude);
                    updateCmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    updateCmd.Parameters.AddWithValue("@UserID", resource.UserID);
                    updateCmd.Parameters.AddWithValue("@ImagePaths", imagePathsJson);

                    await updateCmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #endregion
    }
}

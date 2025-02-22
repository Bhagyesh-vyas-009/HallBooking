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
        
    }
}

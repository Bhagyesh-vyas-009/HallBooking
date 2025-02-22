using HallBookingAPI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace HallBookingAPI.Data
{
    public class ResourceImageRepository
    {
        #region Congfiguration
        private readonly string _connectionString;

        public ResourceImageRepository(IConfiguration connectionString)
        {
            _connectionString = connectionString.GetConnectionString("ConnectionString");
        }
        #endregion

        #region GetAllImagesByOwnerID
        public IEnumerable<ResourceImageModel> GetAllImagesByOwnerID(int OwnerID)
        {
            List<ResourceImageModel> images = new List<ResourceImageModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_ResourceImage_SelectAllByOwner";
                cmd.Parameters.AddWithValue("@OwnerID", OwnerID);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    images.Add(new ResourceImageModel()
                    {
                        ImageID = Convert.ToInt32(sdr["ImageID"]),
                        ResourceID = Convert.ToInt32(sdr["ResourceID"]),
                        ResourceName = sdr["ResourceName"].ToString(),
                        ImageURL = sdr["ImageURL"].ToString(),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"])
                    });
                }
            }
            return images;
        }
        #endregion

        #region GetAllImagesByResourceID
        public IEnumerable<ResourceImageModel> GetAllImagesByResourceID(int ResourceID)
        {
            List<ResourceImageModel> images = new List<ResourceImageModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_ResourceImage_SelectAllByResourceID";
                cmd.Parameters.AddWithValue("@ResourceID", ResourceID);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    images.Add(new ResourceImageModel()
                    {
                        ImageID = Convert.ToInt32(sdr["ImageID"]),
                        ResourceID = Convert.ToInt32(sdr["ResourceID"]),
                        ResourceName = sdr["ResourceName"].ToString(),
                        ImageURL = sdr["ImageURL"].ToString(),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"])
                    });
                }
            }
            return images;
        }
        #endregion

        #region SelectImageByPK
        public ResourceImageModel SelectImageByPK(int ImageID)
        {
            ResourceImageModel image = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_ResourceImage_SelectByPK";
                cmd.Parameters.AddWithValue("@ImageID", ImageID);

                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    image = new ResourceImageModel
                    {
                        ImageID = Convert.ToInt32(sdr["ImageID"]),
                        ResourceID = Convert.ToInt32(sdr["ResourceID"]),
                        ResourceName = sdr["ResourceName"].ToString(),
                        ImageURL = sdr["ImageURL"].ToString(),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"])
                    };
                }
            }

            return image;
        }
        #endregion

        #region DeleteImage
        public bool DeleteImage(int ImageID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_ResourceImage_Delete";
                cmd.Parameters.AddWithValue("@ImageID", ImageID);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region InsertImage
        public bool InsertImage(ResourceImageModel resourceImage)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_ResourceImage_Insert";

                cmd.Parameters.AddWithValue("@ResourceID", resourceImage.ResourceID);
                cmd.Parameters.AddWithValue("@ImageURL", resourceImage.ImageURL);
                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region UpdateImage
        public bool UpdateImage(ResourceImageModel resourceImage)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_ResourceImage_Update";

                cmd.Parameters.AddWithValue("@ImageID", resourceImage.ImageID);
                cmd.Parameters.AddWithValue("@ResourceID", resourceImage.ResourceID);
                cmd.Parameters.AddWithValue("@ImageURL", resourceImage.ImageURL);
                cmd.Parameters.Add("@UpdatedAt", System.Data.SqlDbType.DateTime).Value = DBNull.Value;

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion
    }
}

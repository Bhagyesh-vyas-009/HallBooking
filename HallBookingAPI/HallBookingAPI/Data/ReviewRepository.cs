using HallBookingAPI.Models;
using System.Data.SqlClient;

namespace HallBookingAPI.Data
{
    public class ReviewRepository
    {
        #region Congfiguration
        private readonly string _connectionString;

        public ReviewRepository(IConfiguration connectionString)
        {
            _connectionString = connectionString.GetConnectionString("ConnectionString");
        }
        #endregion

        #region GetAllReviews
        public IEnumerable<ReviewModel> GetAllReviews()
        {
            List<ReviewModel> reviews = new List<ReviewModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Reviews_SelectAll";
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    reviews.Add(new ReviewModel()
                    {
                        ReviewID = Convert.ToInt32(sdr["ReviewID"]),
                        UserID = Convert.ToInt32(sdr["UserID"]),
                        UserName = sdr["FullName"].ToString(),
                        ResourceID = Convert.ToInt32(sdr["ResourceID"]),
                        ResourceName = sdr["Name"].ToString(),
                        Rating = Convert.ToInt32(sdr["Rating"]),
                        ReviewText = sdr["ReviewText"].ToString(),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"])
                    });
                }
            }

            return reviews;
        }
        #endregion

        #region GetAllReviewByResourceID
        public IEnumerable<ReviewModel> GetAllReviewByResourceID(int ResourceID)
        {
            List<ReviewModel> reviews = new List<ReviewModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ResourceID",ResourceID);
                cmd.CommandText = "PR_Reviews_SelectByResourceID";
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    reviews.Add(new ReviewModel()
                    {
                        ReviewID = Convert.ToInt32(sdr["ReviewID"]),
                        UserID = Convert.ToInt32(sdr["UserID"]),
                        UserName = sdr["FullName"].ToString(),
                        ResourceID = Convert.ToInt32(sdr["ResourceID"]),
                        ResourceName = sdr["Name"].ToString(),
                        Rating = Convert.ToInt32(sdr["Rating"]),
                        ReviewText = sdr["ReviewText"].ToString(),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"])
                    });
                }
            }

            return reviews;
        }
        #endregion

        #region GetAllReviewByUserID
        public IEnumerable<ReviewModel> GetAllReviewByUserID(int UserID)
        {
            List<ReviewModel> reviews = new List<ReviewModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.CommandText = "PR_Reviews_SelectByUserID";
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    reviews.Add(new ReviewModel()
                    {
                        ReviewID = Convert.ToInt32(sdr["ReviewID"]),
                        UserID = Convert.ToInt32(sdr["UserID"]),
                        UserName = sdr["FullName"].ToString(),
                        ResourceID = Convert.ToInt32(sdr["ResourceID"]),
                        ResourceName = sdr["Name"].ToString(),
                        Rating = Convert.ToInt32(sdr["Rating"]),
                        ReviewText = sdr["ReviewText"].ToString(),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"])
                    });
                }
            }
            return reviews;
        }
        #endregion

        #region GetAllReviewByOwnerID
        public IEnumerable<ReviewModel> GetAllReviewByOwnerID(int OwnerID)
        {
            List<ReviewModel> reviews = new List<ReviewModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OwnerID", OwnerID);
                cmd.CommandText = "PR_Reviews_SelectByOwnerID";
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    reviews.Add(new ReviewModel()
                    {
                        ReviewID = Convert.ToInt32(sdr["ReviewID"]),
                        UserID = Convert.ToInt32(sdr["UserID"]),
                        UserName = sdr["FullName"].ToString(),
                        ResourceID = Convert.ToInt32(sdr["ResourceID"]),
                        ResourceName = sdr["Name"].ToString(),
                        Rating = Convert.ToInt32(sdr["Rating"]),
                        ReviewText = sdr["ReviewText"].ToString(),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"])
                    });
                }
            }
            return reviews;
        }
        #endregion

        #region SelectReviewByPK
        public ReviewModel SelectReviewByPK(int ReviewID)
        {
            ReviewModel review = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Reviews_SelectByPK";
                cmd.Parameters.AddWithValue("@ReviewID", ReviewID);

                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    review = new ReviewModel
                    {
                        ReviewID = Convert.ToInt32(sdr["ReviewID"]),
                        UserID = Convert.ToInt32(sdr["UserID"]),
                        ResourceID = Convert.ToInt32(sdr["ResourceID"]),
                        Rating = Convert.ToInt32(sdr["Rating"]),
                        ReviewText = sdr["ReviewText"].ToString(),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"])
                    };
                }
            }

            return review;
        }
        #endregion

        #region InsertReview
        public bool InsertReview(ReviewModel review)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Reviews_INSERT";

                cmd.Parameters.AddWithValue("@UserID", review.UserID);
                cmd.Parameters.AddWithValue("@ResourceID", review.ResourceID);
                cmd.Parameters.AddWithValue("@Rating", review.Rating);
                cmd.Parameters.AddWithValue("@ReviewText", review.ReviewText);
                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region UpdateReview
        public bool UpdateReview(ReviewModel review)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Reviews_UPDATE";

                cmd.Parameters.AddWithValue("@ReviewID", review.ReviewID);
                cmd.Parameters.AddWithValue("@UserID", review.UserID);
                cmd.Parameters.AddWithValue("@ResourceID", review.ResourceID);
                cmd.Parameters.AddWithValue("@Rating", review.Rating);
                cmd.Parameters.AddWithValue("@ReviewText", review.ReviewText);
                cmd.Parameters.Add("@UpdatedAt", System.Data.SqlDbType.DateTime).Value = DBNull.Value;

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region DeleteReview
        public bool DeleteReview(int ReviewID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Reviews_Delete";
                cmd.Parameters.AddWithValue("@ReviewID", ReviewID);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

    }
}

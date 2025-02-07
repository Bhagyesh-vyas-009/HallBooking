using HallBookingAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace HallBookingAPI.Data
{
    public class PaymentRepository
    {
        private readonly string _connectionString;

        public PaymentRepository(IConfiguration connectionString)
        {
            _connectionString = connectionString.GetConnectionString("ConnectionString");
        }

        public IEnumerable<PaymentModel> GetAllPayments()
        {
            List<PaymentModel> payments = new List<PaymentModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Payments_SelectAll";
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    payments.Add(new PaymentModel()
                    {
                        PaymentID = Convert.ToInt32(sdr["PaymentID"]),
                        BookingDate = Convert.ToDateTime(sdr["BookingDate"]),
                        UserName = sdr["FullName"].ToString(),
                        PaymentDate = Convert.ToDateTime(sdr["PaymentDate"]),
                        PaymentMethod = sdr["PaymentMethod"].ToString(),
                        Amount = Convert.ToDecimal(sdr["Amount"]),
                        Status = sdr["Status"].ToString(),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"])
                    });
                }
            }

            return payments;
        }

        public PaymentModel SelectPaymentByPK(int PaymentID)
        {
            PaymentModel payment = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Payments_SelectByPK";
                cmd.Parameters.AddWithValue("@PaymentID", PaymentID);

                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    payment = new PaymentModel
                    {
                        PaymentID = Convert.ToInt32(sdr["PaymentID"]),
                        BookingID = Convert.ToInt32(sdr["BookingID"]),
                        UserID = Convert.ToInt32(sdr["UserID"]),
                        PaymentDate = Convert.ToDateTime(sdr["PaymentDate"]),
                        PaymentMethod = sdr["PaymentMethod"].ToString(),
                        Amount = Convert.ToDecimal(sdr["Amount"]),
                        Status = sdr["Status"].ToString(),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"])
                    };
                }
            }

            return payment;
        }

<<<<<<< HEAD
=======
        public IEnumerable<PaymentModel> GetAllPaymentByBookingID(int BookingID)
        {
            List<PaymentModel> payments = new List<PaymentModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Payments_SelectByBookingID";
                cmd.Parameters.AddWithValue("@BookingID", BookingID);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    payments.Add(new PaymentModel()
                    {
                        PaymentID = Convert.ToInt32(sdr["PaymentID"]),
                        BookingDate = Convert.ToDateTime(sdr["BookingDate"]),
                        UserName = sdr["FullName"].ToString(),
                        PaymentDate = Convert.ToDateTime(sdr["PaymentDate"]),
                        PaymentMethod = sdr["PaymentMethod"].ToString(),
                        Amount = Convert.ToDecimal(sdr["Amount"]),
                        Status = sdr["Status"].ToString(),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"])
                    });
                }
            }
            return payments;
        }

        public IEnumerable<UserPaymentModel> GetAllPaymentByUserID(int UserID)
        {
            List<UserPaymentModel> payments = new List<UserPaymentModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Payments_SelectByUserID";
                cmd.Parameters.AddWithValue("@UserID", UserID);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    payments.Add(new UserPaymentModel()
                    {
                        PaymentID = Convert.ToInt32(sdr["PaymentID"]),
                        BookingDate = Convert.ToDateTime(sdr["BookingDate"]),
                        FullName = sdr["FullName"].ToString(),
                        ResourceName = sdr["Name"].ToString(),
                        FromDate = Convert.ToDateTime(sdr["FromDate"]),
                        ToDate = Convert.ToDateTime(sdr["ToDate"]),
                        PaymentDate = Convert.ToDateTime(sdr["PaymentDate"]),
                        PaymentMethod = sdr["PaymentMethod"].ToString(),
                        Amount = Convert.ToDecimal(sdr["Amount"]),
                        Status = sdr["Status"].ToString(),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"])
                    });
                }
            }
            return payments;
        }

>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
        public bool DeletePayment(int PaymentID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Payments_Delete";
                cmd.Parameters.AddWithValue("@PaymentID", PaymentID);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool InsertPayment(PaymentModel payment)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Payments_INSERT";

                cmd.Parameters.AddWithValue("@BookingID", payment.BookingID);
                cmd.Parameters.AddWithValue("@UserID", payment.UserID);
<<<<<<< HEAD
                cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
=======
                cmd.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
                cmd.Parameters.AddWithValue("@PaymentMethod", payment.PaymentMethod);
                cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                cmd.Parameters.AddWithValue("@Status", payment.Status);
                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool UpdatePayment(PaymentModel payment)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Payments_UPDATE";

                cmd.Parameters.AddWithValue("@PaymentID", payment.PaymentID);
                cmd.Parameters.AddWithValue("@BookingID", payment.BookingID);
                cmd.Parameters.AddWithValue("@UserID", payment.UserID);
<<<<<<< HEAD
                cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
=======
                cmd.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
>>>>>>> a6d1194 (JWT authentication and Image Upload Added)
                cmd.Parameters.AddWithValue("@PaymentMethod", payment.PaymentMethod);
                cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                cmd.Parameters.AddWithValue("@Status", payment.Status);
                cmd.Parameters.Add("@UpdatedAt", SqlDbType.DateTime).Value = DBNull.Value;

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}

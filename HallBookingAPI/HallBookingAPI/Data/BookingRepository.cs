using HallBookingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HallBookingAPI.Data
{
    public class BookingRepository
    {
        #region Congfiguration
        private readonly string _connectionString;

        public BookingRepository(IConfiguration connectionString)
        {
            _connectionString = connectionString.GetConnectionString("ConnectionString");
        }
        #endregion

        #region GetAllBookings
        public IEnumerable<UserBookingModel> GetAllBookings()
        {
            List<UserBookingModel> bookings = new List<UserBookingModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Bookings_SelectAll";
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    bookings.Add(new UserBookingModel()
                    {
                        BookingID = Convert.ToInt32(sdr["BookingID"]),
                        UserID = Convert.ToInt32(sdr["UserID"]),
                        FullName = sdr["FullName"].ToString(),
                        Email = sdr["Email"].ToString(),
                        ResourceName = sdr["Name"].ToString(),
                        ResourceType = sdr["ResourceType"].ToString(),
                        Location = sdr["Location"].ToString(),
                        PricePerDay = Convert.ToInt32(sdr["PricePerDay"]),
                        BookingDate = Convert.ToDateTime(sdr["BookingDate"]),
                        FromDate = Convert.ToDateTime(sdr["FromDate"]),
                        ToDate = Convert.ToDateTime(sdr["ToDate"]),
                        TotalPrice = Convert.ToDecimal(sdr["TotalPrice"]),
                        Status = sdr["Status"].ToString(),
                        PaymentStatus = sdr["PaymentStatus"].ToString(),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"])
                    });
                }
            }

            return bookings;
        }
        #endregion

        #region GetAllBookingsByUserID
        public IEnumerable<UserBookingModel> GetAllBookingByUserID(int UserID)
        {
            List<UserBookingModel> bookings = new List<UserBookingModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Bookings_SelectBookingByUserID";
                cmd.Parameters.AddWithValue("@UserID", UserID);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    bookings.Add(new UserBookingModel()
                    {
                        BookingID = Convert.ToInt32(sdr["BookingID"]),
                        UserID = Convert.ToInt32(sdr["UserID"]),
                        FullName = sdr["FullName"].ToString(),
                        Email = sdr["Email"].ToString(),
                        ResourceName = sdr["Name"].ToString(),
                        ResourceType = sdr["ResourceType"].ToString(),
                        Location = sdr["Location"].ToString(),
                        PricePerDay = Convert.ToInt32(sdr["PricePerDay"]),
                        BookingDate = Convert.ToDateTime(sdr["BookingDate"]),
                        FromDate = Convert.ToDateTime(sdr["FromDate"]),
                        ToDate = Convert.ToDateTime(sdr["ToDate"]),
                        TotalPrice = Convert.ToDecimal(sdr["TotalPrice"]),
                        Status = sdr["Status"].ToString(),
                        PaymentStatus = sdr["PaymentStatus"].ToString(),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"])
                    });
                }
            }
            return bookings;
        }
        #endregion

        #region GetAllBookingsByOwnerID
        public IEnumerable<UserBookingModel> GetAllBookingByOwnerID(int OwnerID)
        {
            List<UserBookingModel> bookings = new List<UserBookingModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Bookings_SelectBookingByOwner";
                cmd.Parameters.AddWithValue("@OwnerID", OwnerID);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    bookings.Add(new UserBookingModel()
                    {
                        BookingID = Convert.ToInt32(sdr["BookingID"]),
                        UserID = Convert.ToInt32(sdr["UserID"]),
                        FullName = sdr["FullName"].ToString(),
                        Email = sdr["Email"].ToString(),
                        ResourceName = sdr["Name"].ToString(),
                        ResourceType = sdr["ResourceType"].ToString(),
                        Location = sdr["Location"].ToString(),
                        PricePerDay = Convert.ToInt32(sdr["PricePerDay"]),
                        BookingDate = Convert.ToDateTime(sdr["BookingDate"]),
                        FromDate = Convert.ToDateTime(sdr["FromDate"]),
                        ToDate = Convert.ToDateTime(sdr["ToDate"]),
                        TotalPrice = Convert.ToDecimal(sdr["Amount"]),
                        Status = sdr["Status"].ToString(),
                        PaymentStatus = sdr["PaymentStatus"].ToString(),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"])
                    });
                }
            }
            return bookings;
        }
        #endregion

        #region GetAllBookingsByResourceID
        public IEnumerable<UserBookingModel> GetAllBookingByResourceID(int ResourceID)
        {
            List<UserBookingModel> bookings = new List<UserBookingModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Bookings_SelectBookingByResourceID";
                cmd.Parameters.AddWithValue("@ResourceID", ResourceID);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    bookings.Add(new UserBookingModel()
                    {
                        BookingID = Convert.ToInt32(sdr["BookingID"]),
                        UserID = Convert.ToInt32(sdr["UserID"]),
                        ResourceName = sdr["Name"].ToString(),
                        FullName = sdr["FullName"].ToString(),
                        Email = sdr["Email"].ToString(),
                        ResourceType = sdr["ResourceType"].ToString(),
                        Location = sdr["Location"].ToString(),
                        PricePerDay = Convert.ToInt32(sdr["PricePerDay"]),
                        BookingDate = Convert.ToDateTime(sdr["BookingDate"]),
                        FromDate = Convert.ToDateTime(sdr["FromDate"]),
                        ToDate = Convert.ToDateTime(sdr["ToDate"]),
                        TotalPrice = Convert.ToDecimal(sdr["TotalPrice"]),
                        Status = sdr["Status"].ToString(),
                        PaymentStatus = sdr["PaymentStatus"].ToString(),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"])
                    });
                }
            }
            return bookings;
        }
        #endregion

        #region GetBookingDetail
        public UserBookingModel GetBookingDetail(int BookingID)
        {
            UserBookingModel bookings = new UserBookingModel();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Bookings_BookingDetail";
                cmd.Parameters.AddWithValue("@bookingID", BookingID);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    bookings = new UserBookingModel
                    {
                        ResourceName = sdr["Name"].ToString(),
                        ResourceType = sdr["ResourceType"].ToString(),
                        FullName = sdr["FullName"].ToString(),
                        Location = sdr["Location"].ToString(),
                        PinCode = Convert.ToInt32(sdr["PinCode"]),
                        PricePerDay = Convert.ToInt32(sdr["PricePerDay"]),
                        BookingDate = Convert.ToDateTime(sdr["BookingDate"]),
                        FromDate = Convert.ToDateTime(sdr["FromDate"]),
                        ToDate = Convert.ToDateTime(sdr["ToDate"]),
                        PaymentDate = Convert.ToDateTime(sdr["PaymentDate"]),
                        PaymentMethod = sdr["PaymentMethod"].ToString(),
                        PaymentStatus = sdr["Status"].ToString(),
                        Amount = Convert.ToInt32(sdr["Amount"])
                    };
                }
            }
            return bookings;
        }
        #endregion

        #region SelectBookingByPK
        public BookingModel SelectBookingByPK(int BookingID)
        {
            BookingModel booking = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Bookings_SelectByPK";
                cmd.Parameters.AddWithValue("@BookingID", BookingID);

                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    booking = new BookingModel
                    {
                        BookingID = Convert.ToInt32(sdr["BookingID"]),
                        UserID = Convert.ToInt32(sdr["UserID"]),
                        ResourceID = Convert.ToInt32(sdr["ResourceID"]),
                        BookingDate = Convert.ToDateTime(sdr["BookingDate"]),
                        FromDate = Convert.ToDateTime(sdr["FromDate"]),
                        ToDate = Convert.ToDateTime(sdr["ToDate"]),
                        TotalPrice = Convert.ToDecimal(sdr["TotalPrice"]),
                        Status = sdr["Status"].ToString(),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"])
                    };
                }
            }

            return booking;
        }
        #endregion

        #region DeleteBooking
        public bool DeleteBooking(int bookingID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Bookings_Delete";
                cmd.Parameters.AddWithValue("@BookingID", bookingID);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region InsertBooking
        public bool InsertBooking(BookingModel booking)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Bookings_INSERT";

                cmd.Parameters.AddWithValue("@UserID", booking.UserID);
                cmd.Parameters.AddWithValue("@ResourceID", booking.ResourceID);
                //cmd.Parameters.Add("@BookingDate",SqlDbType.DateTime).Value=DBNull.Value;
                cmd.Parameters.AddWithValue("@BookingDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@FromDate", booking.FromDate);
                cmd.Parameters.AddWithValue("@ToDate", booking.ToDate);
                cmd.Parameters.AddWithValue("@TotalPrice", booking.TotalPrice);
                cmd.Parameters.AddWithValue("@Status", booking.Status);
                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region UpdateBooking
        public bool UpdateBooking(BookingModel booking)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Bookings_UPDATE";

                cmd.Parameters.AddWithValue("@BookingID", booking.BookingID);
                cmd.Parameters.AddWithValue("@UserID", booking.UserID);
                cmd.Parameters.AddWithValue("@ResourceID", booking.ResourceID);
                cmd.Parameters.AddWithValue("@BookingDate", booking.BookingDate);
                cmd.Parameters.AddWithValue("@FromDate", booking.FromDate);
                cmd.Parameters.AddWithValue("@ToDate", booking.ToDate);
                cmd.Parameters.AddWithValue("@TotalPrice", booking.TotalPrice);
                cmd.Parameters.AddWithValue("@Status", booking.Status);
                cmd.Parameters.Add("@UpdatedAt", SqlDbType.DateTime).Value = DBNull.Value;

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region CancelBooking
        public void CancelBooking(int BookingID, string status)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("PR_Bookings_Cancel", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BookingId", BookingID);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.Add("@UpdatedAt", SqlDbType.DateTime).Value = DBNull.Value;

                command.ExecuteNonQuery();
            }
        }
        #endregion

        #region CreateBooking
        public async Task<int> CreateBookingAsync(BookingModel booking)
        {
            int bookingId;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Bookings_CreateBooking";

                cmd.Parameters.AddWithValue("@UserID", booking.UserID);
                cmd.Parameters.AddWithValue("@ResourceID", booking.ResourceID);
                //cmd.Parameters.Add("@BookingDate",SqlDbType.DateTime).Value=DBNull.Value;
                cmd.Parameters.AddWithValue("@BookingDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@FromDate", booking.FromDate);
                cmd.Parameters.AddWithValue("@ToDate", booking.ToDate);
                cmd.Parameters.AddWithValue("@TotalPrice", booking.TotalPrice);
                cmd.Parameters.AddWithValue("@Status", booking.Status);
                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                var bookingIdParam = new SqlParameter("@BookingId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(bookingIdParam);

                await cmd.ExecuteNonQueryAsync();
                bookingId = (int)bookingIdParam.Value;
            }

            return bookingId;
        }
        #endregion

        #region GetBookedDateRangesAsync
        public async Task<List<BookedDateRangeDto>> GetBookedDateRangesAsync(int ResourceID)
        {
            var bookedDateRanges = new List<BookedDateRangeDto>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var command = new SqlCommand("GetBookedDateRanges", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@ResourceID", ResourceID);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // Check for DBNull and parse values safely
                            var fromDate = reader["FromDate"] != DBNull.Value
                                ? Convert.ToDateTime(reader["FromDate"])
                                : throw new Exception("FromDate is NULL in the result set.");

                            var toDate = reader["ToDate"] != DBNull.Value
                                ? Convert.ToDateTime(reader["ToDate"])
                                : throw new Exception("ToDate is NULL in the result set.");

                            bookedDateRanges.Add(new BookedDateRangeDto
                            {
                                FromDate = fromDate,
                                ToDate = toDate
                            });
                            Console.WriteLine($"FromDate: {fromDate}, ToDate: {toDate}");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Log SQL-specific exceptions
                throw new Exception($"SQL error: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                // Log other exceptions
                throw new Exception($"An error occurred while fetching booked date ranges: {ex.Message}", ex);
            }

            return bookedDateRanges;
        }

        public async Task<List<BookedDateRangeDto>> GetBookedDateRanges(int ResourceID)
        {
            var bookedDates = new List<BookedDateRangeDto>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GetBookedDateRanges", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ResourceID", ResourceID);

                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var fromDate = reader.GetDateTime(reader.GetOrdinal("FromDate"));
                            var toDate = reader.GetDateTime(reader.GetOrdinal("ToDate"));

                            bookedDates.Add(new BookedDateRangeDto
                            {
                                FromDate = fromDate,
                                ToDate = toDate
                            });
                        }
                    }
                }
            }

            return bookedDates;
        }
        #endregion

        public async Task<bool> SendHallBookingConfirmation(string email, string userName, string bookingStatus, string hallName, string bookingDate, string fromDate, string toDate, string location, int amount, string paymentStatus)
        {
			try
			{
				using (var smtpClient = new SmtpClient("smtp.gmail.com"))
				{
					smtpClient.Port = 587;
					smtpClient.Credentials = new NetworkCredential("mr.toxic781@gmail.com", "yplu apca urum hxid");
					smtpClient.EnableSsl = true;
                    //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    string emailBody = null;
					if (bookingStatus == "Confirmed")
					{
						emailBody = $@"
				<html>
<body>
	<p>Dear {userName},</p>

	<p>We are pleased to confirm your booking for <strong>{hallName}</strong>. Below are the details of your reservation:</p>

	<h3>Booking Details:</h3>
	<ul>
		<li><strong>Hall Name:</strong> {hallName}</li>
		<li><strong>Booking Date:</strong> {bookingDate}</li>
		<li><strong>Date:</strong> {fromDate} - {toDate}</li>
		<li><strong>Location:</strong> {location}</li>
		<li><strong>Total Amount:</strong> {amount}</li>
		<li><strong>Payment Status:</strong> {paymentStatus}</li>
	</ul>

	<p><strong>Next Steps:</strong></p>
	<p>✔️ Please arrive at least <strong>30 minutes before</strong> your booking time.</p>
	<p>✔️ Carry a valid <strong>ID proof</strong> for verification.</p>
	<p>✔️ For any modifications, contact us at least <strong>24 hours in advance</strong>.</p>

	<p>If you need any assistance, feel free to contact us at <a href='mailto:support@example.com'>mr.toxic781@gmail.com</a>.</p>

	<p>Thank you for choosing us! We look forward to hosting you.</p>

	<p>Best Regards,<br/><strong>Your Company/Hall Name</strong></p>
</body>
</html>
";
					}
					else
					{
						emailBody = $@"
				<html>
<body>
	<p>Dear {userName},</p>

	<p>We regret to inform you that your booking for <strong>{hallName}</strong> has been <strong>cancelled</strong>. Below are the details of your cancelled reservation:</p>

	<h3>Cancelled Booking Details:</h3>
	<ul>
		<li><strong>Hall Name:</strong> {hallName}</li>
		<li><strong>Booking Date:</strong> {bookingDate}</li>
		<li><strong>Date:</strong> {fromDate} - {toDate}</li>
		<li><strong>Location:</strong> {location}</li>
		<li><strong>Total Amount:</strong> {amount}</li>
		<li><strong>Payment Status:</strong> {paymentStatus}</li>
	</ul>


	<p>We sincerely apologize for any inconvenience this may have caused. If this cancellation was unexpected or if you would like to rebook, please contact us as soon as possible.</p>

	<p>If you have already made a payment, we will process your refund (if applicable) within the next <strong>5-7 business days</strong>. For any refund-related inquiries, please reach out to us.</p>

	<p>If you have any questions or need further assistance, feel free to contact us at <a href='mailto:support@example.com'>mr.toxic781@gmail.com</a>.</p>

	<p>Best Regards,<br/><strong>Your Company/Hall Name</strong></p>
</body>
</html>"
	;
					}

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("mr.toxic781@gmail.com"),
                        Subject = "Hall Booking Confirmation - " + hallName,
                        Body = emailBody,
                        IsBodyHtml = true,
                    };
                    mailMessage.To.Add(email);

                    await smtpClient.SendMailAsync(mailMessage);
                } // SmtpClient is disposed of automatically here
                return true;

            }
			catch (Exception ex)
			{
				Console.WriteLine("Error Sending Email: " + ex.Message);
				return false;
			}
        }
    }
}

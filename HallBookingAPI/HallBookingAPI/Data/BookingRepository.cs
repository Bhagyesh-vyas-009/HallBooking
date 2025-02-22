using HallBookingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
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
                            ResourceName = sdr["Name"].ToString(),
                            ResourceType = sdr["ResourceType"].ToString(),
                            Location = sdr["Location"].ToString(),
                            PricePerDay = Convert.ToInt32(sdr["PricePerDay"]),
                            BookingDate = Convert.ToDateTime(sdr["BookingDate"]),
                            FromDate = Convert.ToDateTime(sdr["FromDate"]),
                            ToDate = Convert.ToDateTime(sdr["ToDate"]),
                            TotalPrice = Convert.ToDecimal(sdr["TotalPrice"]),
                            Status = sdr["Status"].ToString(),
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
                cmd.Parameters.AddWithValue("@UserID",UserID);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    bookings.Add(new UserBookingModel()
                    {
                        BookingID = Convert.ToInt32(sdr["BookingID"]),
                        UserID = Convert.ToInt32(sdr["UserID"]),
                        FullName= sdr["FullName"].ToString(),
                        ResourceName = sdr["Name"].ToString(),
                        ResourceType = sdr["ResourceType"].ToString(),
                        Location = sdr["Location"].ToString(),
                        PricePerDay = Convert.ToInt32(sdr["PricePerDay"]),
                        BookingDate = Convert.ToDateTime(sdr["BookingDate"]),
                        FromDate = Convert.ToDateTime(sdr["FromDate"]),
                        ToDate = Convert.ToDateTime(sdr["ToDate"]),
                        TotalPrice = Convert.ToDecimal(sdr["TotalPrice"]),
                        Status = sdr["Status"].ToString(),
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
                        ResourceName = sdr["Name"].ToString(),
                        ResourceType = sdr["ResourceType"].ToString(),
                        Location = sdr["Location"].ToString(),
                        PricePerDay = Convert.ToInt32(sdr["PricePerDay"]),
                        BookingDate = Convert.ToDateTime(sdr["BookingDate"]),
                        FromDate = Convert.ToDateTime(sdr["FromDate"]),
                        ToDate = Convert.ToDateTime(sdr["ToDate"]),
                        TotalPrice = Convert.ToDecimal(sdr["TotalPrice"]),
                        Status = sdr["Status"].ToString(),
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
                        ResourceType = sdr["ResourceType"].ToString(),
                        Location = sdr["Location"].ToString(),
                        PricePerDay = Convert.ToInt32(sdr["PricePerDay"]),
                        BookingDate = Convert.ToDateTime(sdr["BookingDate"]),
                        FromDate = Convert.ToDateTime(sdr["FromDate"]),
                        ToDate = Convert.ToDateTime(sdr["ToDate"]),
                        TotalPrice = Convert.ToDecimal(sdr["TotalPrice"]),
                        Status = sdr["Status"].ToString(),
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
                    bookings=new UserBookingModel
                    {
                        ResourceName = sdr["Name"].ToString(),
                        ResourceType = sdr["ResourceType"].ToString(),
                        FullName = sdr["FullName"].ToString(),
                        Location = sdr["Location"].ToString(),
                        PinCode= Convert.ToInt32(sdr["PinCode"]),
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
                command.Parameters.Add("@UpdatedAt",SqlDbType.DateTime).Value = DBNull.Value;

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
        #endregion
    }
}

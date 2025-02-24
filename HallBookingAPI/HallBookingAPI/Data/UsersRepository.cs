using HallBookingAPI.Models;
using System.Data;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Text;

namespace HallBookingAPI.Data
{
    public class UsersRepository
    {
        #region Configuration
        private readonly string _connectionString;

        public UsersRepository(IConfiguration connectionString)
        {
            _connectionString = connectionString.GetConnectionString("ConnectionString");
        }
        #endregion

        #region GetAllUsers
        public IEnumerable<UsersModel> GetAllUsers()
        {
            List<UsersModel> users = new List<UsersModel>();    

            SqlConnection conn=new SqlConnection(_connectionString );
            conn.Open();
            SqlCommand cmd= conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_Users_SelectAll";
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                users.Add(new UsersModel()
                {
                    UserID = Convert.ToInt32(sdr["UserID"]),
                    FullName = sdr["FullName"].ToString(),
                    Email = sdr["Email"].ToString(),
                    Password = sdr["Password"].ToString(),
                    PhoneNumber = sdr["PhoneNumber"].ToString(),
                    IsAdmin = Convert.ToBoolean(sdr["isAdmin"]),
                    //Address = sdr["Address"].ToString(),
                    Role = sdr["Role"].ToString(),
                    CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                    UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"])
                });
            }
            return users;
        }
        #endregion

        #region SelectUserByPK
        public UsersModel SelectUserByPK(int UserID)
        {
            UsersModel user = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Users_SelectByPK";
                cmd.Parameters.AddWithValue("@UserID", UserID);
                string passwordHash = null;
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user = new UsersModel
                    {
                        UserID = Convert.ToInt32(sdr["UserID"]),
                        FullName = sdr["FullName"].ToString(),
                        Email = sdr["Email"].ToString(),
                        //Password = passwordHash,
                        Password = sdr["Password"].ToString(),
                        PhoneNumber = sdr["PhoneNumber"].ToString(),
                        IsAdmin = Convert.ToBoolean(sdr["isAdmin"]),
                        Role = sdr["Role"].ToString(),
                        CreatedAt = Convert.ToDateTime(sdr["CreatedAt"]),
                        UpdatedAt = Convert.ToDateTime(sdr["UpdatedAt"])
                    };
                }
            }
            return user;
        }
        #endregion

        #region DeleteUser
        public bool DeleteUser(int userID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Users_Delete";
                cmd.Parameters.AddWithValue("@UserID", userID);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region InsertUser
        public bool InsertUser(UsersModel user)
        {
            string passwordHash = HashPassword(user.Password);
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Users_INSERT";

                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", passwordHash);
                cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region UpdateUser
        public bool UpdateUser(UsersModel user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Users_UPDATE";

                cmd.Parameters.AddWithValue("@UserID", user.UserID);
                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                cmd.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
                //cmd.Parameters.AddWithValue("@Address", user.Address);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.Parameters.Add("@UpdatedAt", SqlDbType.DateTime).Value = DBNull.Value;

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region AuthenticateUser
        public UsersModel AuthenticateUser(UserLoginModel user)
        {
            //try
            //{
            string passwordHash = HashPassword(user.Password);
            UsersModel userData = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PR_Users_Login";
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", passwordHash);
                    cmd.Parameters.AddWithValue("@Role", user.Role);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userData = new UsersModel
                            {
                                UserID = Convert.ToInt32(reader["UserID"]),
                                FullName = reader["FullName"].ToString(),
                                Email = reader["Email"].ToString(),
                                IsAdmin = Convert.ToBoolean(reader["isAdmin"]),
                                Role = reader["Role"].ToString(),
                            };
                        }
                    }
                }
            }
            return userData;
        }
        #endregion

        #region UserDropDown
        public IEnumerable<UserDropDownModel> UserDropDown(int OwnerID)
        {
            var users = new List<UserDropDownModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_Users_DropDown";
                cmd.Parameters.AddWithValue("@OwnerID",OwnerID);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    users.Add(new UserDropDownModel
                    {
                        UserID = Convert.ToInt32(sdr["UserID"]),
                        FullName = sdr["FullName"].ToString()
                    });
                }
            }
            return users;
        }
        #endregion

        #region ChangePassword
        public bool ChangePassword(ChangePasswordModel model)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("PR_Users_ChangePassword", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", model.UserID);
                    cmd.Parameters.AddWithValue("@NewPasswordHash", HashPassword(model.NewPassword));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        #endregion

        #region HashPassword
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2"));

                return builder.ToString();
            }
        }
        #endregion
    }
}

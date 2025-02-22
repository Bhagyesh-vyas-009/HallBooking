using WebAPiDemo.Models;
using System.Data.SqlClient;
using System.Data;

namespace WebAPiDemo.Data
{
    public class CityRepository
    {
        #region Configuration
        private readonly string _connectionString;
        public CityRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region GetAllCities
        public List<CityModel> GetAllCities()
        {
            var cities = new List<CityModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_City_SelectAll";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cities.Add(new CityModel
                    {
                        CityID = Convert.ToInt32(reader["CityID"]),
                        CityName = reader["CityName"].ToString(),
                        CityCode = reader["CityCode"].ToString(),
                        StateID = Convert.ToInt32(reader["StateID"]),
                        StateName= reader["StateName"].ToString(),
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        CountryName = reader["CountryName"].ToString(),
                    });
                }
                return cities;
            }
        }
        #endregion

        #region GetCityDropDown
        public IEnumerable<CityDropDownModel> GetCityDropDown()
        {
            var cities = new List<CityDropDownModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_City_DropDown";
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cities.Add(new CityDropDownModel
                    {
                        CityID = Convert.ToInt32(reader["CityID"]),
                        CityName = reader["CityName"].ToString()
                    });
                }
                return cities;
            }
        }
        #endregion

        #region GetCityByStateID
        public IEnumerable<CityDropDownModel> GetCityByStateID(int StateID)
        {
            var cities = new List<CityDropDownModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_City_SelectByStateID";
                cmd.Parameters.AddWithValue("@StateID", StateID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cities.Add(new CityDropDownModel
                    {
                        CityID = Convert.ToInt32(reader["CityID"]),
                        CityName = reader["CityName"].ToString()
                    });
                }
                return cities;
            }
        }
        #endregion

        #region SelectCityByPK
        public CityModel SelectCityByPK(int CityID)
        {
            CityModel city = null;
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_City_SelectByPK";
            cmd.Parameters.AddWithValue("@CityID", CityID);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                city = new CityModel
                {
                    CityID = Convert.ToInt32(reader["CityID"]),
                    CityName = reader["CityName"].ToString(),
                    CityCode = reader["CityCode"].ToString(),
                    StateID = Convert.ToInt32(reader["StateID"]),
                    CountryID = Convert.ToInt32(reader["CountryID"]),
                };
            }
            return city;
        }
        #endregion

        #region InsertCity
        public bool InsertCity(CityModel city)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_City_Insert";

            cmd.Parameters.AddWithValue("@CityName", city.CityName);
            cmd.Parameters.AddWithValue("@CityCode", city.CityCode);
            cmd.Parameters.AddWithValue("@StateID", city.CountryID);
            cmd.Parameters.AddWithValue("@CountryID", city.CountryID);
            cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        #endregion

        #region UpdateCity
        public bool UpdateCity(CityModel city)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_City_Update";

            cmd.Parameters.AddWithValue("@CityID", city.CityID);
            cmd.Parameters.AddWithValue("@CityName", city.CityName);
            cmd.Parameters.AddWithValue("@CityCode", city.CityCode);
            cmd.Parameters.AddWithValue("@StateID", city.StateID);
            cmd.Parameters.AddWithValue("@CountryID", city.CountryID);
            cmd.Parameters.Add("@UpdatedAt", SqlDbType.DateTime).Value = DBNull.Value;

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        #endregion

        #region DeleteCity
        public bool DeleteCity(int CityID)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_City_Delete";
            cmd.Parameters.AddWithValue("@CityID", CityID);

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        #endregion
        
    }
}

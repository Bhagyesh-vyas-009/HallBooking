﻿using WebAPiDemo.Models;
using System.Data.SqlClient;
using System.Data;
namespace WebAPiDemo.Data
{
    public class CountryRepository
    {
        #region Configuration
        private readonly string _connectionString;
        public CountryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region GetAllCountries
        public IEnumerable<CountryModel> GetAllCountries()
        {
            var countries = new List<CountryModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_Country_SelectAll";
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    countries.Add(new CountryModel
                    {
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        CountryName = reader["CountryName"].ToString(),
                        CountryCode = reader["CountryCode"].ToString(),
                    });
                }
                return countries;
            }
        }
        #endregion

        #region SelectCountryByID
        public CountryModel SelectCountryByID(int CountryID)
        {
            CountryModel country = null;
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_Country_SelectByPK";
            cmd.Parameters.AddWithValue("@CountryID", CountryID);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                country = new CountryModel
                {
                    CountryID = Convert.ToInt32(reader["CountryID"]),
                    CountryName = reader["CountryName"].ToString(),
                    CountryCode = reader["CountryCode"].ToString(),
                    //CreatedDate = DateTime(reader["CreatedDate"]).ToString(),
                    //MoodifiedDate = reader["UpdatedAt"].ToString(),
                };
            }
            return country;
        }
        #endregion

        #region InsertCountry
        public bool InsertCountry(CountryModel country)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_Country_Insert";

            cmd.Parameters.AddWithValue("@CountryName", country.CountryName);
            cmd.Parameters.AddWithValue("@CountryCode", country.CountryCode);
            cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        #endregion

        #region UpdateCountry
        public bool UpdateCountry(CountryModel country)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_Country_Update";

            cmd.Parameters.AddWithValue("@CountryID", country.CountryID);
            cmd.Parameters.AddWithValue("@CountryName", country.CountryName);
            cmd.Parameters.AddWithValue("@CountryCode", country.CountryCode);
            cmd.Parameters.Add("@UpdatedAt", SqlDbType.DateTime).Value = DBNull.Value;

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        #endregion

        #region DeleteCountry
        public bool DeleteCountry(int CountryID)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_Country_Delete";
            cmd.Parameters.AddWithValue("@CountryID", CountryID);

            int rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        #endregion

        #region GetCountries
        public IEnumerable<CountryDropDownModel> GetCountries()
        {
            var countries = new List<CountryDropDownModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_Country_DropDown";
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    countries.Add(new CountryDropDownModel
                    {
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        CountryName = reader["CountryName"].ToString()
                    });
                }
                return countries;
            }
        }
        #endregion

        #region GetStateByContryID
        public IEnumerable<StateDropDownModel> GetStateByContryID(int CountryID)
        {
            var states = new List<StateDropDownModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_State_SelectComboBoxByCountryID";
                cmd.Parameters.AddWithValue ("@CountryID", CountryID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    states.Add(new StateDropDownModel
                    {
                        StateID = Convert.ToInt32(reader["StateID"]),
                        StateName = reader["StateName"].ToString()
                    });
                }
                return states;
            }
        }
        #endregion

    }
}

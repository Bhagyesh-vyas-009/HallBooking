using Dapper;
using HallBookingAPI.Models;
using System.Data;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HallBookingAPI.Data
{
    public class DashboardRepository
    {
        private readonly string _connectionString;

        public DashboardRepository(IConfiguration connectionString)
        {
            _connectionString = connectionString.GetConnectionString("ConnectionString");
        }

        public DashboardData GetAdminDashboardData()
        {
            var dashboardData = new DashboardData
            {
                Counts = new List<DashboardCounts>(),
                PopularResources = new List<PopularResource>(),
                TopUsers = new List<TopUser>(),
                TopResourcesByCity = new List<TopResourceByCity>(),
                Revenue = new List<Revenue>(),
                MonthlyRevenue = new List<MonthlyRevenue>(),
                UserCountsByMonth = new List<UserCountsByMonth>()
            };

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("usp_GetDashboardData", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        // Read Counts
                        while (reader.Read())
                        {
                            dashboardData.Counts.Add(new DashboardCounts
                            {
                                Metric = reader["Metric"].ToString(),
                                Value = (int)reader["Value"]
                            });
                        }
                        reader.NextResult();

                        // Read Popular Resources
                        while (reader.Read())
                        {
                            dashboardData.PopularResources.Add(new PopularResource
                            {
                                ResourceID = (int)reader["ResourceID"],
                                ResourceName = reader["ResourceName"].ToString(),
                                ResourceType = reader["ResourceType"].ToString(),
                                Location = reader["Location"].ToString(),
                                PricePerDay = (decimal)reader["PricePerDay"],
                                TotalBookings = (int)reader["TotalBookings"]
                            });
                        }
                        reader.NextResult();

                        // Read Top Users
                        while (reader.Read())
                        {
                            dashboardData.TopUsers.Add(new TopUser
                            {
                                UserID = (int)reader["UserID"],
                                UserName = reader["UserName"].ToString(),
                                Email = reader["Email"].ToString(),
                                TotalBookings = (int)reader["TotalBookings"],
                                TotalSpent = (int)reader["TotalSpent"]
                            });
                        }
                        reader.NextResult();

                        // Read Top Resources by City
                        while (reader.Read())
                        {
                            dashboardData.TopResourcesByCity.Add(new TopResourceByCity
                            {
                                CityID = (int)reader["CityID"],
                                CityName = reader["CityName"].ToString(),
                                ResourceID = (int)reader["ResourceID"],
                                ResourceName = reader["ResourceName"].ToString(),
                                TotalBookings = (int)reader["TotalBookings"]
                            });
                        }
                        reader.NextResult();

                        // Read Revenue
                        while (reader.Read())
                        {
                            dashboardData.Revenue.Add(new Revenue
                            {
                                Metric = reader["Metric"].ToString(),
                                Amount = (decimal)reader["Amount"]
                            });
                        }
                        reader.NextResult();

                        // Read Monthly Revenue
                        while (reader.Read())
                        {
                            dashboardData.MonthlyRevenue.Add(new MonthlyRevenue
                            {
                                MonthYear = reader["MonthYear"].ToString(),
                                TotalRevenue = (decimal)reader["TotalRevenue"]
                            });
                        }
                        reader.NextResult();
                        while (reader.Read())
                        {
                            dashboardData.UserCountsByMonth.Add(new UserCountsByMonth
                            {
                                MonthYear = reader["MonthYear"].ToString(),
                                UserCount = Convert.ToInt32(reader["UserCount"])
                            });
                        }
                    }
                }
            }

            return dashboardData;
        }

        public async Task<OwnerDashboardData> GetDashboardDataAsync(int ownerId, DateTime? startDate, DateTime? endDate)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@OwnerID", ownerId, DbType.Int32);
                parameters.Add("@StartDate", startDate ?? new DateTime(DateTime.Now.Year, 1, 1), DbType.Date);
                parameters.Add("@EndDate", endDate ?? DateTime.Now, DbType.Date);

                using (var multi = await connection.QueryMultipleAsync("[dbo].[Owner_GetDashboardData]", parameters, commandType: CommandType.StoredProcedure))
                {
                    var dashboardData = new OwnerDashboardData
                    {
                        Counts = (await multi.ReadAsync<OwnerDashboardCounts>()).ToList(),
                        PopularResources = (await multi.ReadAsync<OwnerPopularResource>()).ToList(),
                        Revenue = (await multi.ReadAsync<OwnerRevenue>()).ToList(),
                        MonthlyRevenue = (await multi.ReadAsync<OwnerMonthlyRevenue>()).ToList(),
                        Bookings = (await multi.ReadAsync<BookingDetails>()).ToList(),
                        TopCustomers = (await multi.ReadAsync<TopCustomer>()).ToList(),
                        UnderutilizedResources = (await multi.ReadAsync<UnderutilizedResource>()).ToList(),
                        SeasonalBookings = (await multi.ReadAsync<SeasonalBooking>()).ToList()
                    };

                    return dashboardData;
                }
            }
        }
    }

}

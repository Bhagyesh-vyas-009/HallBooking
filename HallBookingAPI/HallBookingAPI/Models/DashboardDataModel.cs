namespace HallBookingAPI.Models
{
    public class DashboardCounts
    {
        public string Metric { get; set; }
        public int Value { get; set; }
    }

    public class PopularResource
    {
        public int ResourceID { get; set; }
        public string ResourceName { get; set; }
        public string ResourceType { get; set; }
        public string Location { get; set; }
        public decimal PricePerDay { get; set; }
        public int TotalBookings { get; set; }
    }

    public class TopUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int TotalBookings { get; set; }

        public int TotalSpent { get; set; }
    }

    public class TopResourceByCity
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int ResourceID { get; set; }
        public string ResourceName { get; set; }
        public int TotalBookings { get; set; }
    }

    public class Revenue
    {
        public string Metric { get; set; }
        public decimal Amount { get; set; }
    }

    public class MonthlyRevenue
    {
        public string MonthYear { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class UserCountsByMonth
    {
        public string MonthYear { get; set; }
        public int UserCount { get; set; }
    }

    public class DashboardData
    {
        public List<DashboardCounts> Counts { get; set; }
        public List<PopularResource> PopularResources { get; set; }
        public List<TopUser> TopUsers { get; set; }
        public List<TopResourceByCity> TopResourcesByCity { get; set; }
        public List<Revenue> Revenue { get; set; }
        public List<MonthlyRevenue> MonthlyRevenue { get; set; }
        public List<UserCountsByMonth> UserCountsByMonth { get; set; }
    }

}

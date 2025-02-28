using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HallBookingAPI.Models
{
    public class OwnerDashboardCounts
    {
        public string Metric { get; set; }
        public int Value { get; set; }
    }

    public class OwnerPopularResource
    {
        public int ResourceID { get; set; }
        public string ResourceName { get; set; }
        public string Location { get; set; }
        public decimal PricePerDay { get; set; }
        public int TotalBookings { get; set; }
    }

    public class OwnerRevenue
    {
        public string Metric { get; set; }
        public decimal Amount { get; set; }
    }

    public class OwnerMonthlyRevenue
    {
        public string MonthYear { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class BookingDetails
    {
        public int BookingID { get; set; }
        public int ResourceID { get; set; }
        public string ResourceName { get; set; }
        public string UserName { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
    }

    public class TopCustomer
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public decimal TotalSpent { get; set; }
    }

    public class SeasonalBooking
    {
        public string Season { get; set; }
        public int TotalBookings { get; set; }
    }

    public class UnderutilizedResource
    {
        public int ResourceID { get; set; }
        public string ResourceName { get; set; }
        public string Location { get; set; }
        public int PricePerDay { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class OwnerDashboardData
    {
        public List<OwnerDashboardCounts> Counts { get; set; }
        public List<OwnerPopularResource> PopularResources { get; set; }
        public List<OwnerRevenue> Revenue { get; set; }
        public List<OwnerMonthlyRevenue> MonthlyRevenue { get; set; }
        public List<BookingDetails> Bookings { get; set; }
        public List<TopCustomer> TopCustomers { get; set; }
        public List<UnderutilizedResource> UnderutilizedResources { get; set; }
        public List<SeasonalBooking> SeasonalBookings { get; set; }
    }


}

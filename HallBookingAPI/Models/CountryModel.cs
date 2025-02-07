using FluentValidation;
using System.Text.Json.Serialization;

namespace WebAPiDemo.Models
{
    public class CountryModel
    {
        //[JsonIgnore]
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public string CountryCode { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public class CountryDropDownModel
    {
        //[JsonIgnore]
        public int CountryID { get; set; }
        public string CountryName { get; set; }
    }
}

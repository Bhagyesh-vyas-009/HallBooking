using FluentValidation;
using System;

namespace WebAPiDemo.Models
{
    public class CityModel
    {
        public int CityID { get; set; }
        public string CityName { get; set; }

        public string CityCode { get; set; }

        public int StateID { get; set; }
        public string? StateName { get; set; }

        public int CountryID { get; set; }
        public string? CountryName { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CityDropDownModel
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
    }

}

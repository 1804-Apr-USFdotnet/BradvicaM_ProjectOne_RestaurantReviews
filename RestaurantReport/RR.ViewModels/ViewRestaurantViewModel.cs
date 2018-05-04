using System;

namespace RR.ViewModels
{
    public class ViewRestaurantViewModel
    {
        public Guid RestaurantId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public double AverageRating { get; set; }
        public string Website { get; set; }
    }
}

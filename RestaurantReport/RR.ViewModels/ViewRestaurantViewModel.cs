using System;

namespace RR.ViewModels
{
    public class ViewRestaurantViewModel
    {
        public Guid RestaurantPublicId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public double AverageRating { get; set; }
        public string Website { get; set; }

        public override string ToString()
        {
            return $"\nRestaurantPublicId: {RestaurantPublicId}\nName: {Name}\nStreet: {Street}\nCity: {City}" +
                   $"\nState: {State}\nZipCode: {ZipCode}\nPhoneNumber: {PhoneNumber}\nAverageRating: {AverageRating}\nWebsite: {Website}\n";
        }
    }
}

using System;

namespace RR.ViewModels
{
    public class TopRatedRestaurantViewModel
    {
        public Guid RestaurantPublicId { get; set; }
        public string Name { get; set; }
        public double AverageRating { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }

        public override string ToString()
        {
            return $"\nRestaurantPublicId: {RestaurantPublicId}\nName: {Name}\nAverageRating: {AverageRating}" +
                   $"\nStreet: {Street}\nCity: {City}\nState: {State}\nZipCode: {ZipCode}\n";
        }
    }
}

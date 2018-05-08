using System;
using System.ComponentModel.DataAnnotations;

namespace RR.ViewModels
{
    public class EditRestaurantViewModel
    {
        public Guid RestaurantPublicId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public int ZipCode { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Website { get; set; }

        public override string ToString()
        {
            return $"\nRestaurantPublicId: {RestaurantPublicId}\nName: {Name}\nStreet: {Street}\nCity: {City}" +
                   $"\nState: {State}\nZipCode: {ZipCode}\nPhoneNumber: {PhoneNumber}\nWebsite: {Website}\n";
        }
    }
}

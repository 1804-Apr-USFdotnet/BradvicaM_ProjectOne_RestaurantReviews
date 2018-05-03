using System;
using RR.Models;

namespace RR.ViewModels
{
    public class EditRestaurantViewModel
    {
        public Restaurant Restaurant { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public Guid PublicId { get; set; }
    }
}

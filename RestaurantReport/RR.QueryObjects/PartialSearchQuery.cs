using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using RR.Models;

namespace RR.QueryObjects
{
    public class PartialSearchQuery
    {
        private readonly string _value;
        private readonly IEnumerable<Restaurant> _restaurants;

        public PartialSearchQuery(string value, IEnumerable<Restaurant> restaurants)
        {
            _value = value;
            _restaurants = restaurants;
        }

        public List<Restaurant> AsExpression()
        {
            return (from x in _restaurants
                    where Regex.IsMatch(x.Name, _value)
                      || Regex.IsMatch(x.PhoneNumber, _value)
                      || Regex.IsMatch(x.Website, _value)
                      || Regex.IsMatch(x.City, _value)
                      || Regex.IsMatch(x.State, _value)
                      || Regex.IsMatch(x.Street, _value)
                      || Regex.IsMatch(x.ZipCode.ToString(), _value)
                      || Regex.IsMatch(x.AverageRating.ToString(CultureInfo.CurrentCulture), _value)
                select x).ToList();
        }
    }
}

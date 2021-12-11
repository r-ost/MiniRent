using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniRent.Domain.Common;

namespace MiniRent.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public string Street { get; private set; } = "";

        public string City { get; private set; } = "";

        public string Country { get; private set; } = "";

        public string ZipCode { get; private set; } = "";


        public static Address Empty => new();

        public Address()
        {

        }

        public Address(string street, string city, string country, string zipcode)
        {
            Street = street;
            City = city;
            Country = country;
            ZipCode = zipcode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return Country;
            yield return ZipCode;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWPFDemo.Model
{
    public class UsersModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Gender { get; set; }
        public int country { get; set; }
        public int state { get; set; }
        public int city { get; set; }
        public string? Address { get; set; }
        public string? GenderString { get; set; }
        public string? CountryString { get; set; }
        public string? StateString { get; set; }
        public string? CityString { get; set; }
        public UsersModel()
        {

        }

        public UsersModel(string? firstName, string? lastName, int gender, int country, int state, int city, string? address)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            this.country = country;
            this.state = state;
            this.city = city;
            Address = address;
        }
    }
}

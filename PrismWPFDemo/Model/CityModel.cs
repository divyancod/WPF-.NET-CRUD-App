using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWPFDemo.Model
{
    public class CityModel
    {
        public int Id { get; set; }
        public string? City { get; set; }

        public CityModel()
        {
        }

        public CityModel(int id, string city)
        {
            Id = id;
            City = city;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWPFDemo.Model
{
    public class CountryModel
    {
        public int Id { get; set; }
        public string? Country { get; set; }

        public CountryModel()
        {
        }

        public CountryModel(int id, string country)
        {
            Id = id;
            Country = country;
        }
    }
}

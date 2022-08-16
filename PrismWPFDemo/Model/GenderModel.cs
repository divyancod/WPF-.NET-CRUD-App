using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWPFDemo.Model
{
    public class GenderModel
    {
        public int Id { get; set; }
        public string? Gender { get; set; }

        public GenderModel() { }
        public GenderModel(int id, string gender)
        {
            Id = id;
            Gender = gender;
        }
    }
}

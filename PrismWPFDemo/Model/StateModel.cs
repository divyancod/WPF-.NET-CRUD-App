using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWPFDemo.Model
{
    public class StateModel
    {
        public int Id { get; set; }
        public string? State { get; set; }

        public StateModel()
        {
        }

        public StateModel(int id, string state)
        {
            Id = id;
            State = state;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWPFDemo
{
    public class DbCustomerStore : ICustomerStore
    {
        public List<string> GetAll()
        {
            List<string> nob = new List<string>();
            nob.Add("Divyanshu");
            nob.Add("Verma");
            return nob;
        }
    }
}

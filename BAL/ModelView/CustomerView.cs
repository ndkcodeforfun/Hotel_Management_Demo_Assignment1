using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ModelView
{
    public class CustomerView
    {
        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? CustomerBirthday { get; set; }
        public byte? CustomerStatus { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCase.Business.Models
{
    public class Employee
    {
        [JsonIgnore]
        public decimal id { get; set; }
        public string name { get; set; }
        public decimal salary { get; set; }
        public decimal age { get; set; }
    }
}

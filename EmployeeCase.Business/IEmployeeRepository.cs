using EmployeeCase.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCase.Business
{
    public interface IEmployeeRepository
    {
        List<Employee> Get();
        bool Create(Employee emp);
        bool Delete(decimal id);
    }
}

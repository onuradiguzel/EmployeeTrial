using EmployeeCase.Business.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeCase.Business
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public bool Create(Employee emp)
        {
            var client = new RestClient("http://dummy.restapiexample.com/api/v1/create");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");

            string bodyJson = JsonConvert.SerializeObject(emp, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            request.AddParameter("application/json", bodyJson, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            int retryCount = 1;
            while (response.StatusCode == (HttpStatusCode)429 && retryCount < 5)
            {
                Thread.Sleep(500);
                response = client.Execute(request);
                retryCount++;
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        public bool Delete(decimal id)
        {
            var client = new RestClient("http://dummy.restapiexample.com/api/v1/delete/" + id.ToString());
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AlwaysMultipartFormData = true;
            IRestResponse response = client.Execute(request);
            int retryCount = 1;
            while (response.StatusCode == (HttpStatusCode)429 && retryCount < 5)
            {
                Thread.Sleep(500);
                response = client.Execute(request);
                retryCount++;
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        public List<Employee> Get()
        {
            List<Employee> employeeList = new List<Employee>();
            var client = new RestClient("http://dummy.restapiexample.com/api/v1/employees");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            int retryCount = 1;
            while (response.StatusCode == (HttpStatusCode)429 && retryCount < 5)
            {
                Thread.Sleep(500);
                response = client.Execute(request);
                retryCount++;
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                JObject obj = JObject.Parse(response.Content);
                if (obj["status"].ToString() == "success")
                {
                    foreach (var data in obj["data"])
                    {
                        employeeList.Add(new Employee
                        {
                            id = Convert.ToDecimal(data["id"]),
                            name = data["employee_name"].ToString(),
                            age = Convert.ToDecimal(data["employee_age"]),
                            salary = Convert.ToDecimal(data["employee_salary"])
                        });
                    }
                }
            }
            return employeeList;
        }
    }
}

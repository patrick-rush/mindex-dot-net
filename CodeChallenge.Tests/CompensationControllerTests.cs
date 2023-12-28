
using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            var newEmployee = CreateEmployee();
            
            // Arrange
            var compensation = new Compensation
            {
                Employee = newEmployee,
                Salary = 200,
                EffectiveDate = new System.DateTime(1717646400000)
            };

            var compensationRequest = new CreateCompensationRequest
            {
                EmployeeId = compensation.Employee.EmployeeId,
                Salary = compensation.Salary,
                EffectiveDate = compensation.EffectiveDate
            };

            var requestContent = new JsonSerialization().ToJson(compensationRequest);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation.Employee);
            Assert.AreEqual(compensation.Salary, newCompensation.Salary);
            Assert.AreEqual(compensation.EffectiveDate, newCompensation.EffectiveDate);
        }

        private static Employee CreateEmployee()
        {
            var employee = new Employee()
            {
                Department = "Complaints",
                FirstName = "Debbie",
                LastName = "Downer",
                Position = "Receiver",
            };

            var employeeRequestContent = new JsonSerialization().ToJson(employee);

            // Execute
            var employeePostRequestTask = _httpClient.PostAsync("api/employee",
                new StringContent(employeeRequestContent, Encoding.UTF8, "application/json"));
            var employeeResponse = employeePostRequestTask.Result;
            return employeeResponse.DeserializeContent<Employee>();
        }
    }
}

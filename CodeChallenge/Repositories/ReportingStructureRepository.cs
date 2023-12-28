using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Repositories
{
    public class ReportingStructureRepository : IReportingStructureRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IReportingStructureRepository> _logger;

        public ReportingStructureRepository(ILogger<IReportingStructureRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }
        public ReportingStructure GetById(string id)
        {
            var reportsCount = new short();
            var employee = _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);

            if (employee == null)
                return null;
            
            /* Depth first search: utilize recursive function GatherReports to accumulate count of reports for a given employee*/
            /* use ref for reportsCount since reportsCount (short) is a value type */
            GatherReports(id, ref reportsCount);
            return new ReportingStructure
            {
                Employee = employee,
                NumberOfReports = reportsCount
            };
        }

        private void GatherReports(string id, ref short reportsCount)
        {
            var currentEmployee = _employeeContext.Employees.Include(employee => employee.DirectReports).SingleOrDefault(e => e.EmployeeId == id);
            if (currentEmployee == null) return;
            {
                foreach (var employee in currentEmployee.DirectReports)
                {
                    reportsCount++;
                    GatherReports(employee.EmployeeId, ref reportsCount);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly CompensationContext _compensationContext;
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, CompensationContext compensationContext, EmployeeContext employeeContext)
        {
            _compensationContext = compensationContext;
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Compensation Add(Compensation compensation)
        {
            compensation.CompensationId = Guid.NewGuid().ToString();
            _compensationContext.Compensations.Add(compensation);
            return compensation;
        }

        public CompensationDto GetByEmployeeId(string id)
        {
            var compensation = _compensationContext.Compensations.Include(e => e.Employee).SingleOrDefault(e => e.Employee.EmployeeId == id);

            if (compensation == null)
                return null;

            return new CompensationDto
            {
                CompensationId = compensation.CompensationId,
                Employee = new EmployeeDto { EmployeeId = compensation.Employee.EmployeeId },
                Salary = compensation.Salary,
                EffectiveDate = compensation.EffectiveDate
            };
        }

        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }
    }
}
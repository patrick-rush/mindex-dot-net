using System;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        public String CompensationId { get; set; }
        public Employee Employee { get; set; }
        public decimal Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }

    public class CompensationDto
    {
        public String CompensationId { get; set; }
        public EmployeeDto Employee { get; set; }
        public decimal Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }

    public class EmployeeDto
    {
        public String EmployeeId { get; set; }
    }

    public class CreateCompensationRequest
    {
        public string EmployeeId { get; set; }
        public decimal Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}

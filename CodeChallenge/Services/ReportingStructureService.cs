using System;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IReportingStructureRepository _reportingStructureRepository;
        private readonly ILogger<ReportingStructureService> _logger;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IReportingStructureRepository reportingStructureRepository)
        {
            _reportingStructureRepository = reportingStructureRepository;
            _logger = logger;
        }
        
        public ReportingStructure GetById(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return _reportingStructureRepository.GetById(id);
            }

            return null;
        }
    }
}
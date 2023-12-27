using CodeChallenge.Models;
using System;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        CompensationDto GetByEmployeeId(string id);
        Compensation Add(Compensation compensation);
        Task SaveAsync();
    }
}
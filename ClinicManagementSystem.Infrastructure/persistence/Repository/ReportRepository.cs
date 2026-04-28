using ClinicManagementSystem.Domain.Abstractions.IRepository;
using ClinicManagementSystem.Domain.Entities;
using ClinicManagementSystem.Infrastructure.Persistence;
using ClinicManagementSystem.Infrastructure.Persistence.Repository;

namespace ClinicManagementSystem.Infrastructure.persistence.Repository
{
    public class ReportRepository :GeneralRepository<Report>,IReportRepository
    {
        private readonly ClinicDbContext _context;

        public ReportRepository(ClinicDbContext context):base(context) 
        {
            _context = context;            
        }

    }
}

using ClinicManagementSystem.Domain.Abstractions.IRepository;
using ClinicManagementSystem.Domain.Entities;
using ClinicManagementSystem.Domain.Entities.Enums;
using ClinicManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace ClinicManagementSystem.Infrastructure.persistence.Repository
{
    public class ScheduleRepository:GeneralRepository<Schedule>,IScheduleRepository
    {
        private readonly ClinicDbContext _context;

        public ScheduleRepository(ClinicDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<Schedule?> GetByDateAsync(DateTime date, CancellationToken cancellationToken)
        {
            return await _context.Schedules
        .FirstOrDefaultAsync(s => s.Date.Date == date.Date, cancellationToken);
        }
    }
}

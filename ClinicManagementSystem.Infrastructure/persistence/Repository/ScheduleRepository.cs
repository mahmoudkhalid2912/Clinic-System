using ClinicManagementSystem.Domain.Abstractions.IRepository;
using ClinicManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace ClinicManagementSystem.Infrastructure.Persistence.Repository
{
    public class ScheduleRepository : GeneralRepository<Schedule>, IScheduleRepository
    {
        private readonly ClinicDbContext _context;

        public ScheduleRepository(ClinicDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Schedule?> GetByDateAsync(DateTime date, CancellationToken cancellationToken)
        {
            var start = date.Date;
            var end = start.AddDays(1);

            return await _context.Schedules
                .FirstOrDefaultAsync(s =>
                    s.Date >= start &&
                    s.Date < end,
                    cancellationToken);
        }
    }
}

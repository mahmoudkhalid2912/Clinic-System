using ClinicManagementSystem.Domain.Abstractions.IRepository;
using ClinicManagementSystem.Domain.Abstractions.IUnitOfWork;
using ClinicManagementSystem.Infrastructure.persistence.Repository;
using ClinicManagementSystem.Infrastructure.Persistence;
using ClinicManagementSystem.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Infrastructure.persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ClinicDbContext _context;

        public IBookingRepository BookinRepository { get; private set; }

        public IFeedBackRepository FeedBackRepository { get; private set; }

        public IMedicalHistoryRepository MedicalHistoryRepository{ get; private set; }

        public IPatientRecordRepository PatientRecordRepository { get; private set; }

        public IPaymentRepository PaymentRepository { get; private set; }

        public IReportRepository ReportRepository { get; private set; }

        public IScheduleRepository ScheduleRepository { get; private set; }


        public UnitOfWork(ClinicDbContext context)
        {
            _context = context;
            BookinRepository = new BookingRepository(_context);
            FeedBackRepository = new FeedBackRepository(_context);
            MedicalHistoryRepository = new MedicalHistoryRepository(_context);
            PatientRecordRepository = new PatientRecordRepository(_context);
            PaymentRepository = new PaymentRepository(_context);
            ReportRepository = new ReportRepository(_context);
            ScheduleRepository = new ScheduleRepository(_context);

        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

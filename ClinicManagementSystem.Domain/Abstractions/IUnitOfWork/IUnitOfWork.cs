using ClinicManagementSystem.Domain.Abstractions.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Domain.Abstractions.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBookinRepository BookinRepository { get; }

        IFeedBackRepository FeedBackRepository { get; } 

        IMedicalHistoryRepository MedicalHistoryRepository { get; }

        IPatientRecordRepository PatientRecordRepository { get; }

        IPaymentRepository PaymentRepository { get; }

        IReportRepository ReportRepository { get; }

        IScheduleRepository ScheduleRepository { get; } 


        void Save();
    }
}

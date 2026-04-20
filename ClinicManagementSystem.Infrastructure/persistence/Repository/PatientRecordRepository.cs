using ClinicManagementSystem.Domain.Abstractions.IRepository;
using ClinicManagementSystem.Domain.Entities;
using ClinicManagementSystem.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Infrastructure.persistence.Repository
{
    public class PatientRecordRepository :GeneralRepository<PatientRecord>,IPatientRecordRepository
    {
        private readonly ClinicDbContext _context;

        public PatientRecordRepository(ClinicDbContext context):base(context)
        {
            _context = context;
        }


    }
}

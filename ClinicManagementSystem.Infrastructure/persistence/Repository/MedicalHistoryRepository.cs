using ClinicManagementSystem.Domain.Abstractions.IRepository;
using ClinicManagementSystem.Domain.Entities;
using ClinicManagementSystem.Infrastructure.Persistence;
using ClinicManagementSystem.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Infrastructure.persistence.Repository
{
    public class MedicalHistoryRepository:GeneralRepository<MedicalHistory> , IMedicalHistoryRepository
    {
        private readonly ClinicDbContext _context;

        public MedicalHistoryRepository(ClinicDbContext context):base(context)
        {
            _context = context;
            
        }

    }
}

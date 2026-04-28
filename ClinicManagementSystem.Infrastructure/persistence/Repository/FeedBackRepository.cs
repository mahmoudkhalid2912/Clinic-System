using ClinicManagementSystem.Domain.Abstractions.IRepository;
using ClinicManagementSystem.Domain.Entities;
using ClinicManagementSystem.Infrastructure.Persistence;
using ClinicManagementSystem.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Infrastructure.persistence.Repository
{
    public class FeedBackRepository :GeneralRepository<Feedback>,IFeedBackRepository
    {
        private readonly ClinicDbContext _context;
        public FeedBackRepository(ClinicDbContext context):base(context)
        {
            _context = context; 
        }
    }
}

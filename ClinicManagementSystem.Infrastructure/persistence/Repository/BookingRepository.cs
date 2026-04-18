using ClinicManagementSystem.Domain.Abstractions.IRepository;
using ClinicManagementSystem.Domain.Entities;
using ClinicManagementSystem.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Infrastructure.persistence.Repository
{
    public class BookingRepository:GeneralRepository<Booking> ,IBookinRepository
    {
       private readonly ClinicDbContext _context;

        public BookingRepository(ClinicDbContext context):base(context) 
        {
            _context = context;

        }


    }
}

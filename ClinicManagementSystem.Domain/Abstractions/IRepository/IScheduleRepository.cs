using ClinicManagementSystem.Domain.Entities;
using ClinicManagementSystem.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Domain.Abstractions.IRepository
{
    public interface IScheduleRepository : IGeneralRepository<Schedule>
    {

        Task<Schedule?> GetByDateAsync(
   DateTime date,
   CancellationToken cancellationToken);

    }
}

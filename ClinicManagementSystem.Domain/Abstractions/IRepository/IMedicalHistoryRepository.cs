using ClinicManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicManagementSystem.Domain.Abstractions.IRepository
{
    public interface IMedicalHistoryRepository:IGeneralRepository<MedicalHistory>
    {
    }
}

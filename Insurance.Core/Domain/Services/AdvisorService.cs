using Insurance.Core.Domain.Entities;
using Insurance.Core.Domain.Interfaces.Service;
using Insurance.Core.Domain.Models.InputModel;
using Insurance.Core.Domain.Models.ViewModel;
using Insurance.Core.Infra.Data;
using System;

namespace Insurance.Core.Domain.Services
{
    public class AdvisorService : ContractPartService<AdvisorInputModel, AdvisorViewModel, Advisor>, IAdvisorService
    {
        public AdvisorService(InsuranceDb db) : base(db)
        {
        }

        protected override Advisor MapFromModel(AdvisorInputModel model, Advisor entity)
        {
            entity ??= new Advisor();
            entity.Name = model.Name;
            entity.LastName = model.LastName;
            entity.Address = model.Address;
            entity.Phone = model.Phone;

            if (entity.HealthStatus == HealthStatus.None)
                entity.HealthStatus = GetHealthStatus();

            return entity;
        }

        protected override AdvisorViewModel MapToModel(Advisor entity)
        {
            return new AdvisorViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                LastName = entity.LastName,
                Address = entity.Address,
                Phone = entity.Phone,
                HealthStatus = entity.HealthStatus
            };
        }

        private static HealthStatus GetHealthStatus()
        {
            var value = new Random().Next(1, 100);
            return value >= 70 ? HealthStatus.Green : HealthStatus.Red;
        }
    }
}
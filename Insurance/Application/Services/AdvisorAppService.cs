using Insurance.Application.Interfaces;
using Insurance.Application.Models.InputModel;
using Insurance.Application.Models.ViewModel;
using Insurance.Core.Interfaces;
using Insurance.Domain.Entities;
using Insurance.Domain.Interfaces.Repository;
using System;

namespace Insurance.Domain.Services
{
    public class AdvisorAppService : ContractPartAppService<AdvisorInputModel, AdvisorViewModel, Advisor>, IAdvisorAppService
    {
        public AdvisorAppService(IAdvisorRepository repo, IUnitOfWork uow)
           : base(repo, uow)
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
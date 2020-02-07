﻿using Insurance.Core.Domain.Models.InputModel;
using Insurance.Core.Domain.Models.ViewModel;

namespace Insurance.Core.Domain.Interfaces.Service
{
    public interface ICarrierService : IContractPartService<CarrierInputModel, CarrierViewModel>
    {
    }
}
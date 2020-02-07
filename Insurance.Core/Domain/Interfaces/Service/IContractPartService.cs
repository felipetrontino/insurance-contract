using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Core.Domain.Interfaces.Service
{
    public interface IContractPartService<TInputModel, TViewModel>
        where TInputModel : class
        where TViewModel : class
    {
        Task Add(TInputModel model);

        Task Update(Guid id, TInputModel model);

        Task<TViewModel> Get(Guid id);

        Task<List<TViewModel>> GetAll();

        Task Delete(Guid id);
    }
}
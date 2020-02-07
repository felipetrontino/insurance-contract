using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Application.Interfaces
{
    public interface IContractPartAppService<TInputModel, TViewModel>
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
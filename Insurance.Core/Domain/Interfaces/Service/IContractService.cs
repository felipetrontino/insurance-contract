using Insurance.Core.Domain.Models.InputModel;
using Insurance.Core.Domain.Models.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Core.Domain.Interfaces.Service
{
    public interface IContractService
    {
        Task Establish(ContractInputModel model);

        Task Terminate(ContractInputModel model);

        Task<List<ContractPartViewModel>> FindShortestPath(ContractInputModel model);

        Task<List<ContractViewModel>> GetAll();

        Task<List<ContractPartViewModel>> GetParts();

        Task<List<NodeViewModel>> GetNodes();

        Task<List<EdgeViewModel>> GetEdges();
    }
}
using Insurance.Application.Models.InputModel;
using Insurance.Application.Models.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Application.Interfaces
{
    public interface IContractAppService
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
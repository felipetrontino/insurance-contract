using Insurance.Application.Models.InputModel;
using Insurance.Application.Models.ViewModel;

namespace Insurance.Application.Interfaces
{
    public interface IMgaAppService : IContractPartAppService<MgaInputModel, MgaViewModel>
    {
    }
}
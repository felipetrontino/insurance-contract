using Insurance.Application.Interfaces;
using Insurance.Application.Models.InputModel;
using Insurance.Application.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContractController : ControllerBase
    {
        private readonly IContractAppService _service;

        public ContractController(IContractAppService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("Establish")]
        public async Task Establish(ContractInputModel model)
        {
            await _service.Establish(model);
        }

        [HttpPost]
        [Route("Terminate")]
        public async Task Terminate(ContractInputModel model)
        {
            await _service.Terminate(model);
        }

        [HttpGet]
        public async Task<IEnumerable<ContractViewModel>> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet]
        [Route("Parts")]
        public async Task<IEnumerable<ContractPartViewModel>> GetParts()
        {
            return await _service.GetParts();
        }

        [HttpGet]
        [Route("Nodes")]
        public async Task<IEnumerable<NodeViewModel>> GetNodes()
        {
            return await _service.GetNodes();
        }

        [HttpGet]
        [Route("Edges")]
        public async Task<IEnumerable<EdgeViewModel>> GetEdges()
        {
            return await _service.GetEdges();
        }

        [HttpGet]
        [Route("FindShortestPath")]
        public async Task<IEnumerable<ContractPartViewModel>> FindShortestPath([FromQuery] Guid fromId, [FromQuery] Guid toId)
        {
            var input = new ContractInputModel() { FromId = fromId, ToId = toId };
            return await _service.FindShortestPath(input);
        }
    }
}
using Insurance.Core.Domain.Entities;
using Insurance.Core.Domain.Interfaces.Service;
using Insurance.Core.Domain.Models.InputModel;
using Insurance.Core.Domain.Models.ViewModel;
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
        private readonly IContractService _service;

        public ContractController(IContractService service)
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
        public async Task<IEnumerable<ContractPart>> FindShortestPath([FromQuery] Guid fromId, [FromQuery] Guid toId)
        {
            var input = new ContractInputModel() { FromId = fromId, ToId = toId };
            return await _service.FindShortestPath(input);
        }
    }
}
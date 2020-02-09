using Insurance.Application.Interfaces;
using Insurance.Application.Models.InputModel;
using Insurance.Application.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
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
        [ProducesResponseType(typeof(OkResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Establish(ContractInputModel model)
        {
            await _service.Establish(model);
            return Ok();
        }

        [HttpPost]
        [Route("Terminate")]
        [ProducesResponseType(typeof(OkResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Terminate(ContractInputModel model)
        {
            await _service.Terminate(model);
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ContractViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet]
        [Route("Parts")]
        [ProducesResponseType(typeof(List<ContractPartViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetParts()
        {
            return Ok(await _service.GetParts());
        }

        [HttpGet]
        [Route("Nodes")]
        [ProducesResponseType(typeof(List<NodeViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetNodes()
        {
            return Ok(await _service.GetNodes());
        }

        [HttpGet]
        [Route("Edges")]
        [ProducesResponseType(typeof(List<EdgeViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetEdges()
        {
            return Ok(await _service.GetEdges());
        }

        [HttpGet]
        [Route("FindShortestPath")]
        [ProducesResponseType(typeof(List<ContractPartViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> FindShortestPath([FromQuery] ContractInputModel model)
        {
            return Ok(await _service.FindShortestPath(model));
        }
    }
}
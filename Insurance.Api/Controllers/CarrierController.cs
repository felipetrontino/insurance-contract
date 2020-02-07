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
    public class CarrierController : ControllerBase
    {
        private readonly ICarrierAppService _service;

        public CarrierController(ICarrierAppService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task Add(CarrierInputModel model)
        {
            await _service.Add(model);
        }

        [HttpPut("{id}")]
        public async Task Update(Guid id, CarrierInputModel model)
        {
            await _service.Update(id, model);
        }

        [HttpGet]
        public async Task<IEnumerable<CarrierViewModel>> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<CarrierViewModel> Get(Guid id)
        {
            return await _service.Get(id);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _service.Delete(id);
        }
    }
}
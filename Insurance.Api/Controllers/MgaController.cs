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
    public class MgaController : ControllerBase
    {
        private readonly IMgaService _service;

        public MgaController(IMgaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task Add(MgaInputModel model)
        {
            await _service.Add(model);
        }

        [HttpPut("{id}")]
        public async Task Update(Guid id, MgaInputModel entity)
        {
            await _service.Update(id, entity);
        }

        [HttpGet]
        public async Task<IEnumerable<MgaViewModel>> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<MgaViewModel> Get(Guid id)
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
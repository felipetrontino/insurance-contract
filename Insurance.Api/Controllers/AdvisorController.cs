using Insurance.Core.Domain.Entities;
using Insurance.Core.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdvisorController : ControllerBase
    {
        private readonly IAdvisorService _service;

        public AdvisorController(IAdvisorService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task Add(Advisor entity)
        {
            entity.Id = Guid.NewGuid();
            await _service.Add(entity);
        }

        [HttpPut("{id}")]
        public async Task Update(Guid id, Advisor entity)
        {
            entity.Id = id;
            await _service.Update(entity);
        }

        [HttpGet]
        public async Task<IEnumerable<Advisor>> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Advisor> Get(Guid id)
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
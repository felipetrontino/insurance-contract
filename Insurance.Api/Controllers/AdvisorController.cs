﻿using Insurance.Application.Interfaces;
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
    public class AdvisorController : ControllerBase
    {
        private readonly IAdvisorAppService _service;

        public AdvisorController(IAdvisorAppService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task Add(AdvisorInputModel model)
        {
            await _service.Add(model);
        }

        [HttpPut("{id}")]
        public async Task Update(Guid id, AdvisorInputModel model)
        {
            await _service.Update(id, model);
        }

        [HttpGet]
        public async Task<IEnumerable<AdvisorViewModel>> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<AdvisorViewModel> Get(Guid id)
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
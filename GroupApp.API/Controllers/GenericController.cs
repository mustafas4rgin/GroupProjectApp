using AutoMapper;
using FluentValidation;
using GroupApp.Core.Concrete;
using GroupApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T, TDto> : ControllerBase where T : class where TDto : class
    {
        private readonly IValidator<TDto> _validator;
        private readonly IService<T> _service;
        private readonly IMapper _mapper;

        public GenericController(IService<T> service, IValidator<TDto> validator, IMapper mapper)
        {
            _validator = validator;
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("/api/[controller]s")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("/api/[controller]s/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("/api/create/[controller]")]
        public virtual async Task<IActionResult> Add([FromBody] TDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var entity = _mapper.Map<T>(dto);
            await _service.AddAsync(entity);
            return Ok(entity);
        }

        [HttpPut("/api/update/[controller]")]
        public async Task<IActionResult> Update([FromBody] TDto dto)
        {
            var entity = _mapper.Map<T>(dto);
            await _service.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("/api/delete/[controller]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}


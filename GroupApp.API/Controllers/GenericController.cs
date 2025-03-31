using FluentValidation;
using GroupApp.Core.Concrete;
using GroupApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T> : ControllerBase where T : class
    {
        private readonly IValidator<T> _validator;
        private readonly IService<T> _service;
        public GenericController(IService<T> service, IValidator<T> validator)
        {
            _validator = validator;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public virtual async Task<IActionResult> Add([FromBody] T entity)
        {
            var validationResult = await _validator.ValidateAsync(entity);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _service.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity }, entity);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] T entity)
        {
            await _service.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }

}

using CPUCheckr.Core.DTO;
using CPUCheckr.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CPUCheckr.Controllers;

public sealed class ProcessorController : BaseController
{
    private readonly IProcessorService _processorService;

    public ProcessorController(IProcessorService processorService)
    {
        _processorService = processorService;
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProcessorDto>> Get([FromRoute] Guid id)
        => OkOrNotFound(await _processorService.GetAsync(id));

    [HttpGet]
    public async Task<ActionResult<ICollection<ProcessorDto>>> GetAll()
        => OkOrNotFound(await _processorService.GetAllAsync());

    [HttpGet("manufacturer")]
    public async Task<ActionResult<ICollection<ProcessorDto>>> GetAll([FromQuery] string manufacturer)
        => OkOrNotFound(await _processorService.GetAllByManufacturerAsync(manufacturer));
    
    [HttpGet("cores")]
    public async Task<ActionResult<ICollection<ProcessorDto>>> GetAll([FromQuery] int cores)
        => OkOrNotFound(await _processorService.GetAllByCoresAsync(cores));

    [HttpPost("create")]
    public async Task<ActionResult> Add([FromBody] ProcessorDto dto)
    {
        await _processorService.AddAsync(dto);

        return CreatedAtAction(nameof(Get), new { id = dto.Id }, null);
    }

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] double price)
    {
        await _processorService.UpdatePriceAsync(id, price);

        return Accepted();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] ProcessorDto dto)
    {
        await _processorService.UpdateAsync(id, dto);

        return Accepted();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _processorService.DeleteAsync(id);

        return NoContent();
    }
}
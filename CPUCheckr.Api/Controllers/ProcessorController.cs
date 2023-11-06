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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProcessorDto>> Get([FromRoute] Guid id)
        => OkOrNotFound(await _processorService.GetAsync(id));

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ICollection<ProcessorDto>>> GetAll()
        => OkOrNotFound(await _processorService.GetAllAsync());

    [HttpGet("manufacturer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ICollection<ProcessorDto>>> GetAll([FromQuery] string manufacturer)
        => OkOrNotFound(await _processorService.GetAllByManufacturerAsync(manufacturer));
    
    [HttpGet("cores")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ICollection<ProcessorDto>>> GetAll([FromQuery] int cores)
        => OkOrNotFound(await _processorService.GetAllByCoresAsync(cores));

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Add([FromBody] ProcessorDto dto)
    {
        if (dto.Id == Guid.Empty)
        {
            dto = dto with { Id = Guid.NewGuid() };
        }
        
        await _processorService.AddAsync(dto);

        return CreatedAtAction(nameof(Get), new { id = dto.Id }, null);
    }

    [HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] PriceDto price)
    {
        await _processorService.UpdatePriceAsync(id, price);

        return Accepted();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] ProcessorDto dto)
    {
        await _processorService.UpdateAsync(id, dto);

        return Accepted();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _processorService.DeleteAsync(id);

        return NoContent();
    }
}
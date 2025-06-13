using Microsoft.AspNetCore.Mvc;
using Test.Dto;
using Test.Services;

namespace Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecordsController : ControllerBase
{
    private readonly IRecordService _service;

    public RecordsController(IRecordService service)
    {
        this._service = service;
    }

    [HttpPost]
    public async Task<ActionResult<RecordDto>> CreateRecord([FromBody] CreateRecordRequestDto requestDto)
    {
        try
        {
            var res = await _service.CreateRecord(requestDto);
            return CreatedAtAction(nameof(GetRecords), new { }, res);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecordDto>>> GetRecords(
        [FromQuery] DateTime? created,
        [FromQuery] int? languageId,
        [FromQuery] int? taskId)
    {
        var res = await _service.GetRecords(created, languageId, taskId);
        return Ok(res);
    }
}
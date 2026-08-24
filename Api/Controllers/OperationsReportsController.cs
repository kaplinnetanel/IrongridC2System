using Api.DTOcs;
using Api.Models;
using Api.Repservoirs;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/reports")]
public class OperationsReportsController : ControllerBase
{
    private readonly IOperationsReportsReservoir _operationsReportsReservoir;
    public OperationsReportsController(IOperationsReportsReservoir OperationsReportsReservoir)
    {
        _operationsReportsReservoir = OperationsReportsReservoir;
    }
    [HttpGet("critical-assets")]
    public async Task<ActionResult<IEnumerable<CriticalAssets>>> GetCriticaAssets()
    {
        var result = await _operationsReportsReservoir.GetCriticaAssets();
        return Ok(result);
    }
    [HttpGet("unit/{unitId}/assets")]
    public async Task<ActionResult<IEnumerable<SUnitIdAssets>>> GetUnitAssets(int unitId)
    {
        var result = await _operationsReportsReservoir.GetUnitAssets(unitId);
        return Ok(result);
    }
    [HttpGet("summary-by-unit")]
    public async Task<ActionResult<IEnumerable<SummaryByUnitDTO>>> GetSummary()
    {
        var result = await _operationsReportsReservoir.GetSummary();
        return Ok(result);
    }
}
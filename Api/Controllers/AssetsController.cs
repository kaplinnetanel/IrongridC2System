using Api.DTOcs;
using Api.Models;
using Api.Repservoirs;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers;

[ApiController]
[Route("api/assets")]
public class AssetsController : ControllerBase
{
    private readonly IAseetsReservoir _aseetsReservoir;
    public AssetsController(IAseetsReservoir AseetsReservoir)
    {
        _aseetsReservoir = AseetsReservoir;
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Assets?>> GeatByIdAsset(int id)
    {
        Console.WriteLine("in");
        var result = await _aseetsReservoir.GeatByIdAsset(id);
        if (result == null)

        {
            Console.WriteLine(result);
            return NotFound();
        }
        return Ok(result);

    }
    [HttpPost("units")]
    public async Task<ActionResult<Units?>> AppIdAsset(Units units)
    {
        var result = await _aseetsReservoir.AppIdAsset(units);
        if (result == null)
        {
            return BadRequest();
        }
        return Ok(result);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<Assets?>> UpAssets(int id, UpAssetsDTO assets)
    {
        var result = await _aseetsReservoir.UpAssets(id, assets);
        if (result == false)
        {
            return NotFound();
        }
        return Ok(assets);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletAssets(int id)
    {
        var result = await _aseetsReservoir.DeletAssets(id);
        if (result == false)
        {
            return NotFound();
        }
        return NoContent();
    }
}

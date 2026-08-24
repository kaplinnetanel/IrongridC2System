using Api.DTOcs;
using Api.Models;
using Api.Repservoirs;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/asset-status")]
public class AssetsStatusController : ControllerBase
{
    private readonly IAssetsStatusReservoir _assetsStatusReservoir;
    public AssetsStatusController(IAssetsStatusReservoir AssetsStatusReservoir)
    {
         _assetsStatusReservoir = AssetsStatusReservoir;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssetLiveStatusDTO>>> GeatassetsStatus()
    {
        var result = await _assetsStatusReservoir.GeatassetsStatus();
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);

        
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<AssetLiveStatusDTO?>> GeatassetsStatusById(int id)
    {
        var result = await _assetsStatusReservoir.GeatassetsStatusById(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);

    }
    [HttpGet("status")]
    public async Task<ActionResult<IEnumerable<AssetLiveStatusDTO>>> GeatassetsStatus(string status)
    {
        var result = await _assetsStatusReservoir.GeatassetsStatuss(status);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

}

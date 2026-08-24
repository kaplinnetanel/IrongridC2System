using Api.Data;
using Api.DTOcs;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repservoirs;

public class AseetsReservoir : IAseetsReservoir
{
    private readonly ApplicationDbContext _applicationDb;

    public AseetsReservoir(ApplicationDbContext applicationDb)
    {
        _applicationDb = applicationDb;
    }
    public async Task<AssetsByIdDto?> GeatByIdAsset(int id)
    {
        var a= await _applicationDb.Assets.Where(a => a.Id == id).Select(a => new
        AssetsByIdDto
        {
            UnitId = a.UnitId,
            AssetSerial = a.AssetSerial,
            AssetType = a.AssetType

        }).FirstOrDefaultAsync();
        if (a == null)
        {
            return null;
        }
        return a;
    }
    public async Task<Units?> AppIdAsset(Units units)
    {
       await _applicationDb.Units.AddAsync(units);
       await _applicationDb.SaveChangesAsync();
       return units;
    }
    public async Task<bool> UpAssets(int id, UpAssetsDTO assets)
    {
        var result = await _applicationDb.Assets.FindAsync(id);
        if (result == null)
        {
            return false;
        }

        result.UnitId = assets.UnitId;
        result.AssetSerial = assets.AssetSerial;
        result.AssetType = assets.AssetType;

        
        await _applicationDb.SaveChangesAsync();
        return true;

    }
    public async Task<bool> DeletAssets(int id)
    {
        var result = await _applicationDb.Assets.FirstOrDefaultAsync(a => a.Id == id);
        if (result == null)
        {
            return false;
        }
        _applicationDb.Assets.Remove(result);
        await _applicationDb.SaveChangesAsync();
        return true;

    }
}

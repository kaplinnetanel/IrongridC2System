using Api.Data;
using Api.DTOcs;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repservoirs;

public class AssetsStatusReservoir : IAssetsStatusReservoir
{
    private readonly ApplicationDbContext _applicationDb;

    public AssetsStatusReservoir(ApplicationDbContext applicationDb)
    {
        _applicationDb = applicationDb;
    }
    public async Task<IEnumerable<AssetLiveStatusDTO>> GeatassetsStatus()
    {
        IQueryable<AssetLiveStatus> query = _applicationDb.AssetLiveStatus;
        return await query.Select(p => new AssetLiveStatusDTO
        {
            AssetType = p.AssetType,
            RawValue = p.RawValue,
            ProcessedStatus = p.ProcessedStatus,
            IsVerified = p.IsVerified,
            LastUpdate = p.LastUpdate
        }).ToListAsync();
    }
    public async Task<AssetLiveStatusDTO?> GeatassetsStatusById(int id)
    {
        var result  = await _applicationDb.AssetLiveStatus.Where(a => a.AssetId == id).Select(p => new AssetLiveStatusDTO
        {
            AssetType = p.AssetType,
            RawValue = p.RawValue,
            ProcessedStatus = p.ProcessedStatus,
            IsVerified = p.IsVerified,
            LastUpdate = p.LastUpdate

        }).FirstOrDefaultAsync();
        if (result == null)
        {
            return null;
        }
        return result;    

    }
    public async Task<IEnumerable<AssetLiveStatusDTO>> GeatassetsStatuss(string status)
    {
        return await _applicationDb.AssetLiveStatus.Where(a => a.ProcessedStatus == status).Select(p => new AssetLiveStatusDTO
        {
            AssetType = p.AssetType,
            RawValue = p.RawValue,
            ProcessedStatus = p.ProcessedStatus,
            IsVerified = p.IsVerified,
            LastUpdate = p.LastUpdate

        }).ToListAsync();
    }

}
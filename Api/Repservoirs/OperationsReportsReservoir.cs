using Api.Data;
using Api.DTOcs;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repservoirs;

public class OperationsReportsReservoir : IOperationsReportsReservoir
{
    private readonly ApplicationDbContext _applicationDb;

    public OperationsReportsReservoir(ApplicationDbContext applicationDb)
    {
        _applicationDb = applicationDb;
    }
    public async Task<IEnumerable<CriticalAssets>> GetCriticaAssets()
    {
        return await _applicationDb.Assets.Where(a => a.assetLiveStatus.ProcessedStatus == "Warning"
        || a.assetLiveStatus.IsVerified == false).Select(p => new CriticalAssets
        {
            assetId = p.Id,
            AssetSerial = p.AssetType,
            AssetType = p.AssetType,
            UnitName = p.Units.UnitName,
            Sector = p.Units.Sector,
            ProcessedStatus = p.assetLiveStatus.ProcessedStatus,
            IsVerified = p.assetLiveStatus.IsVerified,
            LastUpdate = p.assetLiveStatus.LastUpdate
        }).ToListAsync();
    }
    public async Task<IEnumerable<SUnitIdAssets>> GetUnitAssets(int unitId)
    {
        return await _applicationDb.Assets.Where(a => a.UnitId == unitId).Select(p => new SUnitIdAssets
        {
            assetId = p.Id,
            AssetSerial = p.AssetType,
            AssetType = p.AssetType,
            ProcessedStatus = p.assetLiveStatus.ProcessedStatus,
            IsVerified = p.assetLiveStatus.IsVerified,
            LastUpdate = p.assetLiveStatus.LastUpdate
        }).ToListAsync();

    }
    public async Task<IEnumerable<SummaryByUnitDTO>> GetSummary()
    {

        IQueryable<Units> query = _applicationDb.Units;
        return await query.Select(x => new SummaryByUnitDTO
        {
            unitId = x.Id,
            UnitName = x.UnitName,
            sector = x.Sector,
            totalAssets = x.Assets.Count(),
            stableAssets = x.Assets.Count(x => x.assetLiveStatus.ProcessedStatus == "Stable"),
            warningAssets = x.Assets.Count(x => x.assetLiveStatus.ProcessedStatus == "Warning"),
            unverifiedAssets = 0
        } ).ToListAsync();
    }
}


        
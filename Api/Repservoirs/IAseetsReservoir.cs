using Api.DTOcs;
using Api.Models;

namespace Api.Repservoirs;

public interface IAseetsReservoir
{
    Task<AssetsByIdDto?> GeatByIdAsset(int id);
    Task<Units?> AppIdAsset(Units units);
    Task<bool> UpAssets(int id, UpAssetsDTO assets);
    Task<bool> DeletAssets(int id);
}

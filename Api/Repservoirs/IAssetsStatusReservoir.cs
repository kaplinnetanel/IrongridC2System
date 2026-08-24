using Api.DTOcs;
using Api.Models;
using System.Data.SqlTypes;

namespace Api.Repservoirs;

public interface IAssetsStatusReservoir
{
    Task<IEnumerable<AssetLiveStatusDTO>> GeatassetsStatus();
    Task<AssetLiveStatusDTO?> GeatassetsStatusById(int id);
    Task<IEnumerable<AssetLiveStatusDTO>> GeatassetsStatuss(string status);

}

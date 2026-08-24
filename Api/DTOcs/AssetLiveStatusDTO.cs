using System.ComponentModel.DataAnnotations;

namespace Api.DTOcs;

public class AssetLiveStatusDTO
{
    public string AssetType { get; set; } = string.Empty;
    public string RawValue { get; set; } = string.Empty;
    public string ProcessedStatus { get; set; } = string.Empty;
    public bool IsVerified { get; set; }
    public DateTime LastUpdate { get; set; }
}




        
        
using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class AssetLiveStatus
{
    [Key]
    public int AssetId { get; set; }
    [Required]
    public string AssetType { get; set; } = string.Empty;
    [Required]
    public string RawValue { get; set; } = string.Empty;
    [Required]
    public string ProcessedStatus { get; set; } = string.Empty;
    [Required]
    public bool IsVerified { get; set; }
    [Required]
    public DateTime LastUpdate { get; set; }
    public Assets Assets { get; set; } = null!;
}

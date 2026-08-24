using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Assets
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int UnitId { get; set; }
    [Required]
    [StringLength(255)]
    public string AssetSerial { get; set; } = string.Empty;
    public string AssetType { get; set; } = "GenericAsset";
    public Units Units { get; set; } = null!;
    public AssetLiveStatus assetLiveStatus { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Consumer.Models;

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
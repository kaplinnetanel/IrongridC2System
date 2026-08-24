using Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Api.DTOcs
{
    public class UpAssetsDTO
    {

        public int UnitId { get; set; }
        public string AssetSerial { get; set; } = string.Empty;
        public string AssetType { get; set; } = "GenericAsset";
    }
}

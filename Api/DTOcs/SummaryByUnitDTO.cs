namespace Api.DTOcs;

public class SummaryByUnitDTO
{
   public int unitId { get; set; }
   public string UnitName { get; set; } = string.Empty;
   public string  sector { get; set; } = string.Empty;
   public int  totalAssets { get; set; }
   public int stableAssets { get; set; }
   public int warningAssets { get; set; }
   public  int unverifiedAssets { get; set; }
}

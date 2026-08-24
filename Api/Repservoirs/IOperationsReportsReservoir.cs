using Api.DTOcs;

namespace Api.Repservoirs;

public interface IOperationsReportsReservoir
{
    Task<IEnumerable<CriticalAssets>> GetCriticaAssets();
    Task<IEnumerable<SUnitIdAssets>> GetUnitAssets(int unitId);
    Task<IEnumerable<SummaryByUnitDTO>> GetSummary();
}

using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Units
{
    [Key]
    public int Id { get; set; }
    [StringLength(255)]
    public string UnitName { get; set; } = "Unknown Unit";
    [StringLength(255)]
    public string Sector { get; set; } = "General";
    public ICollection<Assets> Assets = new List<Assets>();
}


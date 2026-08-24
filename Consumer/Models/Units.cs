using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Models;

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

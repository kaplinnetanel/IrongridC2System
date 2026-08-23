using Producer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Producer.Services;

public class DataLoaderService
{
    public string _dataDirectory { get; set; }
    public DataLoaderService(string dataDirectory = "Data")
    {
        _dataDirectory = dataDirectory;
    }
    public List<IroGrid> LoadIroGrid()
    {
        string filePath = Path.Combine(_dataDirectory,"field_reports.json");
        string jsonString = File.ReadAllText(filePath);

        return JsonSerializer.Deserialize<List<IroGrid>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<IroGrid>();
    }

}

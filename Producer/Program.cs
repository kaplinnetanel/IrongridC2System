using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Producer.Models;
using Producer.Services;
using System.Text.Json;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsetings.json", optional: false)
    .Build();

string bootstrapServers = configuration["Kafka:BootstrapServers"] ?? "localhost:9092";

var dLoder = new DataLoaderService();
var iroGrid = dLoder.LoadIroGrid();
Console.WriteLine("Finish load jeson");

var kafkaService = new ProducerService(bootstrapServers);

foreach (var n in iroGrid)
{
    if (n.AssetType == "UAV")
    {
      
        string json = JsonSerializer.Serialize(n);
        await kafkaService.SendMessageAsync("uav", null, json);
        Console.WriteLine("Add topics UAV");
    }
    else
    {
        string json = JsonSerializer.Serialize(n);
        await kafkaService.SendMessageAsync("PerimeterSensor", null, json);
        Console.WriteLine("Add topics PerimeterSensor");
    }
}
Console.WriteLine("Finish send the topics");


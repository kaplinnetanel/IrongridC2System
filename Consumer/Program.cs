using Confluent.Kafka;
using Consumer.Data;
using Consumer.Models;
using Consumer.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class Pogram
{
    public static async Task Main(string[] args)
    {


        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var serviceCollection = new ServiceCollection();
        var conn = configuration.GetConnectionString("DefaultConnection");



        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
         options.UseMySql(conn, ServerVersion.AutoDetect(conn)));

        serviceCollection.AddScoped<ProcessingService>();

        var serviceProvider = serviceCollection.BuildServiceProvider();



       
        using (var scope = serviceProvider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.EnsureCreated();
        }

        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"] ?? "localhost:9092",
            GroupId = configuration["Kafka:GroupId"] ?? "consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };

        var topic1 = configuration["Kafka:Topics:UAV"] ?? "uav";
        var topic2 = configuration["Kafka:Topics:PerimeterSensor"] ?? "PerimeterSensor";
        using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
        consumer.Subscribe(new[] { topic1, topic2 });

        while (true)
        {
            var result = consumer.Consume(TimeSpan.FromSeconds(10));
            if (result == null || result.Message.Value == null)
            {
                continue;
            }

            using var scope = serviceProvider.CreateScope();

            if (result.Topic == topic1)
            {

                var processingService = scope.ServiceProvider.GetRequiredService<ProcessingService>();
                if (await processingService.ProcessIroGrid(result.Message.Value))
                {
                    consumer.Commit(result);
                    Console.WriteLine("UAV message processed successfully.");
                }
            }
            else if (result.Topic == topic2)
            {
                var processingService = scope.ServiceProvider.GetRequiredService<ProcessingService>();
                if (await processingService.ProcessIroGrid(result.Message.Value))
                {
                    consumer.Commit(result);
                    Console.WriteLine("PerimeterSensor message processed successfully.");
                }
            }
        }

    }
}



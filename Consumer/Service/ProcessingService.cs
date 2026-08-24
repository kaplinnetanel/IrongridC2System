using Consumer.Data;
using Consumer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Consumer.Service;

public class ProcessingService
{
    private readonly ApplicationDbContext _applicationDb;

    public ProcessingService(ApplicationDbContext applicationDb)
    {
        _applicationDb = applicationDb;
    }
    public async Task<bool> ProcessIroGrid(string jsonMessage)
    {
        var reading = JsonSerializer.Deserialize<IroGridReding>(jsonMessage);
        if (reading == null) return false;
        bool isVerified = true;
        string processedStatus = "Stable";
        string wVa = reading.RawValue;
        if (reading.AssetType == "UAV")
        {
            if (int.TryParse(reading.RawValue, out int aval))
            {
                if (aval > 20 && aval < 100)
                {
                    isVerified = true;
                    processedStatus = "Stable";
                }
                if (aval > 0 && aval < 19)
                {
                    isVerified = true;
                    processedStatus = "Warning";
                }
                else
                {
                    isVerified = false;
                    processedStatus = "Warning";
                }
            }
            else
            {
                isVerified = false;
                processedStatus = "Warning";
            }
        }
        else
        {
            string Lowercaseword = reading.RawValue?.Trim().ToLower() ?? string.Empty;

            if (Lowercaseword == "good")
            {
                wVa = "Good";
                isVerified = true;
                processedStatus = "Stable";

            }
            if (Lowercaseword == "bad")
            {
                wVa = "Bad";
                isVerified = true;
                processedStatus = "Warning";
            }
            else
            {
                wVa = "null";
                isVerified = false;
                processedStatus = "Warning";

            }


        }
        Console.WriteLine("up AssetLiveStatus");
        var e = await _applicationDb.AssetLiveStatus.FirstOrDefaultAsync(x => x.AssetId == reading.AssetId);
        if (e != null)
        {
            e.AssetId = reading.AssetId;
            e.AssetType = reading.AssetType;
            e.RawValue = wVa;
            e.ProcessedStatus = processedStatus;
            e.IsVerified = isVerified;
            e.LastUpdate = DateTime.UtcNow;

        }
        else
        {
            Console.WriteLine("new AssetLiveStatus");
            _applicationDb.AssetLiveStatus.Add(new AssetLiveStatus
            {
                AssetId = reading.AssetId,
                AssetType = reading.AssetType,
                RawValue = wVa,
                ProcessedStatus = processedStatus,
                IsVerified = isVerified,
                LastUpdate = DateTime.Now,
            }); 
        }
        await _applicationDb.SaveChangesAsync();
        return true;
    }

}














using OpenTelemetry;
using OpenTelemetry.Exporter;
using System.Diagnostics;

public class CustomExporter : BaseExporter<Activity>
{
    public override ExportResult Export(in Batch<Activity> batch)
    {
        Console.WriteLine($"Exporting trace: {activity.DisplayName}");
        foreach (var activity in batch)
        {
            Console.WriteLine($"custom exporter: {activity.DisplayName}");
        }
        return ExportResult.Success;
    }
}
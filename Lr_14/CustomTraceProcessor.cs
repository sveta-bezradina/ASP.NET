using OpenTelemetry;
using System.Diagnostics;

public class CustomTraceProcessor : BaseProcessor<Activity>
{
    public override void OnEnd(Activity data)
    {
        data.SetTag("application.version", "1.0.0");
        data.SetTag("environment", "development");
        base.OnEnd(data);
    }
}

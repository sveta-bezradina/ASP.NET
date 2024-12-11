using OpenTelemetry;
using System.Diagnostics;
using System.Linq;

public class FilteringProcessor : BaseProcessor<Activity>
{
    private readonly Func<Activity, bool> _filter;

    public FilteringProcessor(Func<Activity, bool> filter)
    {
        _filter = filter;
    }

    public override void OnEnd(Activity data)
    {
        if (_filter(data))
        {
            base.OnEnd(data);
        }
    }
}

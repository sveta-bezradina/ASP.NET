using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
    {
        tracerProviderBuilder
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Lr_14"))
            .AddAspNetCoreInstrumentation()
            .AddProcessor(new FilteringProcessor(activity =>
            {
                var statusCode = activity.Tags.FirstOrDefault(t => t.Key == "http.status_code").Value;
                return statusCode == "500";
            }))
            .AddProcessor(new CustomTraceProcessor())
            .AddProcessor(new BatchActivityExportProcessor(new CustomExporter()))
            .AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri("http://localhost:4321");
            });
    });

var app = builder.Build();

app.MapGet("/", () => "Hello, OpenTelemetry!");
app.MapGet("/custom-trace", (TracerProvider tracerProvider) =>
{
    var tracer = tracerProvider.GetTracer("custom-tracer");
    using (var span = tracer.StartActiveSpan("SampleOperation"))
    {
        span.SetAttribute("http.method", "GET");
        span.SetAttribute("http.response.status_code", "200");
        span.SetAttribute("custom.trace.detail", "Custom operation executed");
        span.SetStatus(Status.Ok);

        Thread.Sleep(100);

        span.End();
    }
    return Results.Ok("Custom trace generated with attributes.");
});


app.Run();

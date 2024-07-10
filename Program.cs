using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add Health checks services
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = async (context, HealthReport) =>
    {
        context.Response.ContentType = "application/json";

        var result = JsonSerializer.Serialize(new
        {
            status = HealthReport.Status.ToString(),
            errors = HealthReport.Entries.Select(e => new { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status) })
        });

        await context.Response.WriteAsync(result);
    }
    }).AllowAnonymous();


app.Run();

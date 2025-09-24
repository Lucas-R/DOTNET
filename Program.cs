using API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
    options.ListenAnyIP(5001, listenOptions => 
    {
        listenOptions.UseHttps();
    });
});

builder.Services.AddScoped<DatabaseContext>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment()){ app.MapOpenApi(); }

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

using Pijze.Bff;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services
    .AddAuthentication(builder.Configuration)
    .AddProxy(builder.Configuration)
    .AddSpaStaticFiles(configuration =>
    {
        configuration.RootPath = "ClientApp/dist";
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

if(builder.Environment.IsProduction())
    app.UseSpaStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapReverseProxy();
    endpoints.MapControllers();
});

app.UseSpa(configuration =>
{
    configuration.Options.SourcePath = "ClientApp";
    if (builder.Environment.IsDevelopment())
        configuration.UseProxyToSpaDevelopmentServer(builder.Configuration["Spa:DevelopmentServerUrl"]);
});

app.Run();
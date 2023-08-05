using Pijze.Bff;
using Pijze.Bff.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services
    .AddSwagger(builder.Configuration)
    .AddAuthentication(builder.Configuration)
    .AddProxy(builder.Configuration)
    .AddSpaStaticFiles(configuration =>
    {
        configuration.RootPath = "ClientApp/dist";
    });

var app = builder.Build();

app.MapHealthChecks("/health");
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(opt=>opt.SwaggerEndpoint("/swagger/services/1.0/swagger.json", "Pijze - 1.0"));
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
    if(app.Environment.IsDevelopment())
        endpoints.MapSwaggers(builder.Configuration, app.Services.GetService<ISwaggerClient>());
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
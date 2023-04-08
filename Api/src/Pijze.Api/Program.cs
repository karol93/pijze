using Pijze.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Pijze.Api;
using Pijze.Api.Security.AuthorizationHandlers;
using Pijze.Api.Security.Requirements;
using Pijze.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
	.AddApis()
	.AddDomain()
	.AddInfrastructure(builder.Configuration.GetConnectionString("PijzeDb"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	var issuer = builder.Configuration.GetSection("JwtConfig:Issuer").Value;
	options.Authority = issuer;
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateAudience = true,
		ValidAudiences = builder.Configuration.GetSection("JwtConfig:Audience").Value.Split(";"),
		ValidateIssuer = true,
		ValidIssuer = builder.Configuration.GetSection(issuer).Value,
	};
});

builder.Services.AddAuthorization(o =>
{
	o.AddPolicy("admin", p=>p.RequireRole("Admin"));
	o.AddPolicy("read:pijze", p => p.RequireAuthenticatedUser().AddRequirements(new ScopeRequirement("read:pijze")));
});

builder.Services.AddSingleton<IAuthorizationHandler, ScopeHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
//
// app.UseDefaultFiles();
// app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// app.MapControllers();
// app.MapFallbackToFile("index.html");

app.UseApis();

app.Run();
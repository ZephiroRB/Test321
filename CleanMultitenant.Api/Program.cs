using CleanMultitenant.Application.Services.Users.AddUser;
using CleanMultitenant.Domain.Configurations;
using CleanMultitenant.Domain.Interfaces.Internal;
using CleanMultitenant.Domain.Models.Internal;
using CleanMultitenant.Domain.Notifications;
using CleanMultitenant.Infra.Contexts.Internal;
using CleanMultitenant.Infra.Contexts.Tenant;
using CleanMultitenant.Infra.Repositories.Internal;
using CleanMultitenant.Infra.Services.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
}).AddEntityFrameworkStores<InternalDbContext>();

builder.Services.AddDbContext<InternalDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("lnternal"));
});

builder.Services.AddDbContext<TenantDbContext>();
builder.Services.AddScoped<ITenantDbContext>(provider => provider.GetService<TenantDbContext>());

builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AddUserRequestHandler)));

builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<INotifier, Notifier>();

builder.Services.AddHttpContextAccessor();

var section = builder.Configuration.GetSection("J2tConfig");
builder.Services.Configure<JwtConfig>(section);
var config = section.Get<JwtConfig>();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Secret)),
        ValidateIssuer = true,
        ValidIssuer = config.Issuer,
        ValidateAudience = true,
        ValidAudience = config.Audience
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

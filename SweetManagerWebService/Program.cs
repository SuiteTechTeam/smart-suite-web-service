using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using SweetManagerIotWebService.API.IAM.Application.Internal.CommandServices.Credentials;
using SweetManagerIotWebService.API.IAM.Application.Internal.CommandServices.Preferences;
using SweetManagerIotWebService.API.IAM.Application.Internal.CommandServices.Roles;
using SweetManagerIotWebService.API.IAM.Application.Internal.CommandServices.Users;
using SweetManagerIotWebService.API.IAM.Application.Internal.OutboundServices;
using SweetManagerIotWebService.API.IAM.Application.Internal.QueryServices.Credentials;
using SweetManagerIotWebService.API.IAM.Application.Internal.QueryServices.Preferences;
using SweetManagerIotWebService.API.IAM.Application.Internal.QueryServices.Roles;
using SweetManagerIotWebService.API.IAM.Application.Internal.QueryServices.Users;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Preferences;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Roles;
using SweetManagerIotWebService.API.IAM.Domain.Repositories.Users;
using SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Preferences;
using SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Roles;
using SweetManagerIotWebService.API.IAM.Domain.Services.CommandServices.Users;
using SweetManagerIotWebService.API.IAM.Domain.Services.QueryServices.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Services.QueryServices.Preferences;
using SweetManagerIotWebService.API.IAM.Domain.Services.QueryServices.Roles;
using SweetManagerIotWebService.API.IAM.Domain.Services.QueryServices.Users;
using SweetManagerIotWebService.API.IAM.Infrastructure.Hashing.Argon2Id.Services;
using SweetManagerIotWebService.API.IAM.Infrastructure.Persistence.EFC.Repositories.Credentials;
using SweetManagerIotWebService.API.IAM.Infrastructure.Persistence.EFC.Repositories.Preferences;
using SweetManagerIotWebService.API.IAM.Infrastructure.Persistence.EFC.Repositories.Roles;
using SweetManagerIotWebService.API.IAM.Infrastructure.Persistence.EFC.Repositories.Users;
using SweetManagerIotWebService.API.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using SweetManagerIotWebService.API.IAM.Infrastructure.Population.Roles;
using SweetManagerIotWebService.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using SweetManagerIotWebService.API.IAM.Infrastructure.Tokens.JWT.Services;
using SweetManagerIotWebService.API.Shared.Domain.Repositories;
using SweetManagerIotWebService.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using System.Data;
using SweetManagerIotWebService.API.Reservations.Application.Internal.CommandServices;
using SweetManagerIotWebService.API.Reservations.Application.Internal.QueryServices;
using SweetManagerIotWebService.API.Reservations.Domain.Repositories;
using SweetManagerIotWebService.API.Reservations.Domain.Services.Booking;
using SweetManagerIotWebService.API.Reservations.Domain.Services.Room;
using SweetManagerIotWebService.API.Reservations.Domain.Services.TypeRoom;
using SweetManagerIotWebService.API.Reservations.Infrastructure.Persistence.EFC.Repositories;
using System.Text;

using SweetManagerIotWebService.API.Commerce.Application.Internal.CommandServices;
using SweetManagerIotWebService.API.Commerce.Application.Internal.QueryServices;
using SweetManagerIotWebService.API.Commerce.Domain.Repositories;
using SweetManagerIotWebService.API.Commerce.Domain.Services;
using SweetManagerIotWebService.API.Commerce.Infrastructure.Persistence.Dapper.Dashboard;
using SweetManagerIotWebService.API.Commerce.Infrastructure.Persistence.EFC.Repositories;
using SweetManagerIotWebService.API.OrganizationalManagement.Application.Internal.CommandServices;
using SweetManagerIotWebService.API.OrganizationalManagement.Application.Internal.QueryServices;
using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Repositories;
using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Services;
using SweetManagerIotWebService.API.OrganizationalManagement.Infrastructure.Persistence.EFC.Repositories;
using SweetManagerIotWebService.API.Inventory.Application.Internal.CommandServices;
using SweetManagerIotWebService.API.Inventory.Application.Internal.QueryServices;
using SweetManagerIotWebService.API.Inventory.Domain.Repositories;
using SweetManagerIotWebService.API.Inventory.Domain.Services;
using SweetManagerIotWebService.API.Inventory.Infrastructure.Persistence.Repositories;
using SweetManagerIotWebService.API.Communication.Application.Internal.CommandServices;
using SweetManagerIotWebService.API.Communication.Application.Internal.QueryServices;
using SweetManagerIotWebService.API.Communication.Domain.Repositories;
using SweetManagerIotWebService.API.Communication.Domain.Services;
using SweetManagerIotWebService.API.Communication.Infrastructure.Persistence.EFC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Configure Lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Database Configuration
// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("SweetManager");

builder.Services.AddTransient<IDbConnection>(db => new MySqlConnection(connectionString));

var connectionStringFromEnvironment = Environment.GetEnvironmentVariable("SweetManagerDbConnection");

if (connectionStringFromEnvironment != null)
{
    connectionString = connectionStringFromEnvironment;
}

// Configure Database Context and Logging Levels
builder.Services.AddDbContext<SweetManagerContext>(
    options =>
    {
        if (connectionString != null)
            if (builder.Environment.IsDevelopment())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            else if (builder.Environment.IsProduction())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Error)
                    .EnableDetailedErrors();
    });

#endregion

#region OPENAPI Configuration
// Configure Lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "Sweet Manager API",
                Version = "v1",
                Description = "Sweet Manager API",
                TermsOfService = new Uri("https://acme-learning.com/tos"),
                Contact = new OpenApiContact
                {
                    Name = "Sweet Manager Studios",
                    Email = "contact@swm.com"
                },
                License = new OpenApiLicense
                {
                    Name = "Apache 2.0",
                    Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                }
            });
        c.EnableAnnotations();
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer", Type = ReferenceType.SecurityScheme
                    }
                },
                Array.Empty<string>()
            }
        });
    });

#endregion

builder.Services.AddHttpContextAccessor();

#region Dependency Injection

// IAM Bounded context
builder.Services.AddScoped<IAdminCredentialRepository, AdminCredentialRepository>();
builder.Services.AddScoped<IGuestCredentialRepository, GuestCredentialRepository>();
builder.Services.AddScoped<IOwnerCredentialRepository, OwnerCredentialRepository>();
builder.Services.AddScoped<IGuestPreferenceRepository, GuestPreferenceRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IGuestRepository, GuestRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IAdminCredentialCommandService, AdminCredentialCommandService>();
builder.Services.AddScoped<IGuestCredentialCommandService, GuestCredentialCommandService>();
builder.Services.AddScoped<IOwnerCredentialCommandService, OwnerCredentialCommandService>();
builder.Services.AddScoped<IGuestPreferenceCommandService, GuestPreferenceCommandService>();
builder.Services.AddScoped<IRoleCommandService, RoleCommandService>();
builder.Services.AddScoped<IAdminCommandService, AdminCommandService>();
builder.Services.AddScoped<IGuestCommandService, GuestCommandService>();
builder.Services.AddScoped<IOwnerCommandService, OwnerCommandService>();
builder.Services.AddScoped<IAdminCredentialQueryService, AdminCredentialQueryService>();
builder.Services.AddScoped<IGuestCredentialQueryService, GuestCredentialQueryService>();
builder.Services.AddScoped<IOwnerCredentialQueryService, OwnerCredentialQueryService>();
builder.Services.AddScoped<IGuestPreferenceQueryService, GuestPreferenceQueryService>();
builder.Services.AddScoped<IRoleQueryService, RoleQueryService>();
builder.Services.AddScoped<IAdminQueryService, AdminQueryService>();
builder.Services.AddScoped<IGuestQueryService, GuestQueryService>();
builder.Services.AddScoped<IOwnerQueryService, OwnerQueryService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<RolesInitializer>();

// Reservations bounded context
builder.Services.AddScoped<IRoomCommandService, RoomCommandService>();
builder.Services.AddScoped<IRoomQueryService, RoomQueryService>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

builder.Services.AddScoped<ITypeRoomRepository, TypeRoomRepository>();
builder.Services.AddScoped<ITypeRoomCommandService, TypeRoomCommandServices>();
builder.Services.AddScoped<ITypeRoomQueryService, TypeRoomQueryServices>();

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingCommandServices, BookingCommandService>();
builder.Services.AddScoped<IBookingQueryServices, BookingQueryService>();

// Commerce Bounded context
builder.Services.AddScoped<IPaymentCustomerRepository, PaymentCustomerRepository>();
builder.Services.AddScoped<IPaymentCustomerCommandService, PaymentCustomerCommandService>();
builder.Services.AddScoped<IPaymentCustomerQueryService, PaymentCustomerQueryService>();

builder.Services.AddScoped<IPaymentOwnerRepository, PaymentOwnerRepository>();
builder.Services.AddScoped<IPaymentOwnerCommandService, PaymentOwnerCommandService>();
builder.Services.AddScoped<IPaymentOwnerQueryService, PaymentOwnerQueryService>();

builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<ISubscriptionCommandService, SubscriptionCommandService>();
builder.Services.AddScoped<ISubscriptionQueryService, SubscriptionQueryService>();

builder.Services.AddScoped<IContractOwnerRepository, ContractOwnerRepository>();
builder.Services.AddScoped<IContractOwnerCommandService, ContractOwnerCommandService>();
builder.Services.AddScoped<IContractOwnerQueryService, ContractOwnerQueryService>();

builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IDashboardQueryService, DashboardQueryService>();

// Inventory Bounded Context
builder.Services.AddScoped<ISupplyRepository, SupplyRepository>(); 
builder.Services.AddScoped<ISupplyCommandService, SupplyCommandService>();
builder.Services.AddScoped<ISupplyQueryService, SupplyQueryService>();

builder.Services.AddScoped<ISupplyRequestRepository, SupplyRequestRepository>();
builder.Services.AddScoped<ISupplyRequestCommandService, SupplyRequestCommandService>();
builder.Services.AddScoped<ISupplyRequestQueryService, SupplyRequestQueryService>();
// Communication Bounded context
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationCommandService, NotificationCommandService>();
builder.Services.AddScoped<INotificationQueryService, NotificationQueryService>();


// Organizational Management Bounded context
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IHotelCommandService, HotelCommandService>();
builder.Services.AddScoped<IHotelQueryService, HotelQueryService>();
builder.Services.AddScoped<IProviderCommandService, ProviderCommandService>();
builder.Services.AddScoped<IProviderQueryService, ProviderQueryService>();
builder.Services.AddScoped<IProviderRepository, ProviderRepository>();


// Shared Bounded context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#endregion

#region JWT Configuration

var tokenSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.Configure<TokenSettings>(tokenSettings);

var secretKey = tokenSettings["SecretKey"];

var audience = tokenSettings["Audience"];

var issuer = tokenSettings["Issuer"];

var securityKey = !string.IsNullOrEmpty(secretKey)
    ? new SymmetricSecurityKey(Encoding.Default.GetBytes(secretKey))
    : throw new ArgumentException("Secret key is null or empty");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = securityKey,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddScoped<IHashingService, HashingService>();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddTransient<TokenValidationHandler>();

builder.Services.AddAuthorization();

#endregion

var app = builder.Build();

#region Ensure Database Created (COMPILE AppDbContext)
// Verify Database Objects are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SweetManagerContext>();
    context.Database.EnsureCreated();
}

#endregion

#region Run DatabaseInitializer
using (var scope = app.Services.CreateScope())
{
    var roleInitializer = scope.ServiceProvider.GetRequiredService<RolesInitializer>();

    roleInitializer.InitializeAsync().Wait();
}
#endregion

// Configuration cors
app.UseCors(
    b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
);

app.UseSwagger();

app.UseSwaggerUI();

app.UseRouting();

app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run(); 
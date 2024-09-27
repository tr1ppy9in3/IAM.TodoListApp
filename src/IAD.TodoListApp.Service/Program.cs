using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

using IAD.TodoListApp.Core.Options;

using IAD.TodoListApp.Packages;

using IAD.TodoListApp.DataAccess;
using IAD.TodoListApp.DataAccess.User;
using IAD.TodoListApp.DataAccess.Auth;
using IAD.TodoListApp.DataAccess.TodoTask;
using IAD.TodoListApp.DataAccess.TaskCategory;

using IAD.TodoListApp.UseCases;
using IAD.TodoListApp.UseCases.Abstractions;
using IAD.TodoListApp.UseCases.User;
using IAD.TodoListApp.UseCases.User.Queries;
using IAD.TodoListApp.UseCases.TodoTask;
using IAD.TodoListApp.UseCases.TodoTask.Models;
using IAD.TodoListApp.UseCases.Auth;
using IAD.TodoListApp.UseCases.TaskCategory;

using IAD.TodoListApp.Service.SwaggerFilters;
using IAD.TodoListApp.Service.Infrastructure;
using IAD.TodoListApp.Service.Middlewares;

using IAD.TodoListApp.Chat.Service.Hubs;

namespace IAD.TodoListApp.Service;

/// <summary>
/// ��������� ��� �������� �����.
/// </summary>
public static class Program
{
    /// <summary>
    /// ������� �����
    /// </summary>
    /// <param name="args"> ��������� ��� ������� </param>
    public static async Task Main(string[] args)
    {
        var builder = ConfigureApp(args);
        await RunApp(builder);
    }

    private static WebApplicationBuilder ConfigureApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.ClearProviders();

        var services = builder.Services;
        var cfg = builder.Configuration;

        services.AddControllers();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.WithOrigins("http://localhost:3001")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
        });

        var env = builder.Environment;
        services.AddSingleton(env);

        AddAuthentication(builder.Services, builder.Configuration);
        AddAuthorization(builder.Services);

        services.AddHealthChecks();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            options.OperationFilter<SecurityRequirementsOperationFilter>();
            options.OperationFilter<AuthorizeCheckOperationFilter>();

            var basePath = AppContext.BaseDirectory;
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(basePath, xmlFile);
            options.IncludeXmlComments(xmlPath);

            options.UseAllOfToExtendReferenceSchemas();
            options.UseAllOfForInheritance();
            options.UseOneOfForPolymorphism();
            options.UseInlineDefinitionsForEnums();
        });



        ConfigureDI(services, cfg);

        return builder;
    }

    private static void ConfigureDI(IServiceCollection services, ConfigurationManager configuration)
    {
        // DB Context
        var connectionStrings = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<Context>(options =>
        {
            options.UseNpgsql(connectionStrings);
            options.UseSnakeCaseNamingConvention();
        });
        // SignalR
        services.AddSignalR();
        // Mediator
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllUsersQuery).Assembly));
        // AutoMapper
        services.AddAutoMapper(cfg => cfg.AddProfile(typeof(MappingProfile)));
        // Validation
        services.AddValidationPipelines(typeof(TaskInputModelValidator).Assembly);
        // Dependencies
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ITaskCategoryRepository, TaskCategoryRepository>();
        services.AddScoped<UserAccessor>();
        // Options
        services.Configure<PasswordOptions>(configuration.GetSection("PasswordOptions"));
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
    }

    private static void AddAuthentication(IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtConfig = configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]!));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfig["Jwt:Issuer"],
                ValidAudience = jwtConfig["Jwt:Audience"],
                IssuerSigningKey = key
            };
        });

        services.AddHttpContextAccessor();
    }

    private static void AddAuthorization(IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("AdminPolicy", policy =>
            {
                policy.RequireRole("Admin");
            })
            .AddPolicy("RegularUserPolicy", policy =>
            {
                policy.RequireRole("RegularUser");
            });

        services.AddScoped<ITokenService, TokenService>();
    }

    private static async Task RunApp(WebApplicationBuilder builder)
    {
        var app = builder.Build();
        var appName = builder.Configuration["ServiceName"] ?? throw new ArgumentNullException(builder.Configuration["ServiceName"], "ServiceName is not provided");

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }

        app.UseRouting();
        app.UseCors("AllowAllOrigins");

        app.UseMiddleware<BlacklistTokenMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapHealthChecks("/health");
        app.MapGet(string.Empty, async ctx => await ctx.Response.WriteAsync(appName));

        app.MapControllers();

        app.MapHub<ChatHub>("hubs/chat");

        await app.RunAsync();
    }
}
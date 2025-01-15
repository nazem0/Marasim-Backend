using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.Repositories;
using System.Globalization;
using System.Text;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(context =>
            {
                context
                    .UseLazyLoadingProxies()
                    .UseSqlServer
                    (builder.Configuration.GetConnectionString("MyDB"),
                    c => c.EnableRetryOnFailure());
            });

            builder.Services.AddIdentity<User, IdentityRole>
            (Options =>
            {
                Options.Lockout.MaxFailedAccessAttempts = 10;
                Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                Options.User.RequireUniqueEmail = true;
                Options.SignIn.RequireConfirmedPhoneNumber = false;
                Options.SignIn.RequireConfirmedEmail = false;
                Options.SignIn.RequireConfirmedAccount = false;
                Options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddApiEndpoints();

            builder.Services.AddAuthentication(Option =>
            {
                Option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                Option.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
                Option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(Options =>
            {
                Options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                };
            });

            builder.Services.AddCors
            (option =>
            {
                option.AddDefaultPolicy(i =>
                i.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            });

            builder.Services.Configure<IdentityOptions>(Options =>
            {
                Options.Password.RequireNonAlphanumeric = false;
                Options.Password.RequireUppercase = false;

            });
            builder.Services.ConfigureApplicationCookie(Options =>
            {
                Options.LoginPath = "/Api/Account/Login";

            });
            // Add services to the container.
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICityRepository, CityRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IFollowRepository, FollowRepository>();
            builder.Services.AddScoped<IGovernorateRepository, GovernorateRepository>();
            builder.Services.AddScoped<IInvitationRepository, InvitationRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IPostAttachmentRepository, PostAttachmentRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IPromoCodeRepository, PromoCodeRepository>();
            builder.Services.AddScoped<IReactRepository, ReactRepository>();
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IServiceAttachmentRepository, ServiceAttachmentRepository>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<IVendorRepository, VendorRepository>();
            builder.Services.AddScoped<IWithdrawalRepository, WithdrawalRepository>();
            builder.Services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "E_Commerce API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer eYXXXXXXXX\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                    },
                    Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors();
            app.MapIdentityApi<User>();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute("Default", "api/{Controller}/{Action=Index}/{id?}");

            //app.MapControllers();

            app.Run();

        }
    }
}
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models;
using Repository;
using System.Text;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var Builder = WebApplication.CreateBuilder(args);
            Builder.Services.AddDbContext<EntitiesContext>(context =>
            {
                context
                    //.UseLazyLoadingProxies()
                    .UseSqlServer
                    (Builder.Configuration.GetConnectionString("MyDB"));

                //context.UseLazyLoadingProxies()
                //    .UseSqlServer
                //    (Builder.Configuration.GetConnectionString("MySamer"));
            });

            Builder.Services.AddIdentity<User, IdentityRole>(Options =>
            {
                Options.Lockout.MaxFailedAccessAttempts = 2;
                Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                Options.User.RequireUniqueEmail = true;
                Options.SignIn.RequireConfirmedPhoneNumber = false;
                Options.SignIn.RequireConfirmedEmail = false;
                Options.SignIn.RequireConfirmedAccount = false;
                Options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
            })
                            .AddEntityFrameworkStores<EntitiesContext>()
                            .AddDefaultTokenProviders();
            Builder.Services.AddAuthentication(Option =>
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
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Builder.Configuration["Jwt:Key"]!)),

                                };

                            });

            Builder.Services.AddCors(option =>
            {
                option.AddDefaultPolicy(i =>
                i.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            });

            Builder.Services.Configure<IdentityOptions>(Options =>
            {
                Options.Password.RequireNonAlphanumeric = false;
                Options.Password.RequireUppercase = false;

            });
            Builder.Services.ConfigureApplicationCookie(Options =>
            {
                Options.LoginPath = "/Api/Account/Login";

            });
            // Add services to the container.
            Builder.Services.AddScoped<UserManager>();
            Builder.Services.AddScoped<CategoryManager>();
            Builder.Services.AddScoped<ReservationManager>();
            Builder.Services.AddScoped<ServiceAttachmentManager>();
            Builder.Services.AddScoped<ServiceManager>();
            Builder.Services.AddScoped<ReviewManager>();
            Builder.Services.AddScoped<ReviewManager>();
            Builder.Services.AddScoped<PostManager>();
            Builder.Services.AddScoped<CommentManager>();
            Builder.Services.AddScoped<ReactManager>();
            Builder.Services.AddScoped<PostAttachmentManager>();
            Builder.Services.AddScoped<CheckListManager>();
            Builder.Services.AddScoped<FollowManager>();
            Builder.Services.AddScoped<PromoCodeManager>();
            Builder.Services.AddScoped<VendorManager>();
            Builder.Services.AddScoped<ServiceAttachmentManager>();
            Builder.Services.AddScoped<AccountRepository>();
            Builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling =
        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            Builder.Services.AddEndpointsApiExplorer();
            Builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "JWTToken_Auth_API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
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

            var App = Builder.Build();

            // Configure the HTTP request pipeline.
            if (App.Environment.IsDevelopment())
            {
                App.UseSwagger();
                App.UseSwaggerUI();
            }

            App.UseHttpsRedirection();
            App.UseCors();
            App.UseStaticFiles();
            App.UseAuthentication();
            App.UseAuthorization();
            App.MapControllerRoute("Default", "api/{Controller}/{Action=Index}/{id?}");

            //app.MapControllers();

            App.Run();

        }
    }
}
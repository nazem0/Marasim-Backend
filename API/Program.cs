using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repository;
using System.Text;

namespace Marasim_Backend
{
    public class Program
    {
        public static int Main()
        {
            WebApplicationBuilder Builder =
              WebApplication.CreateBuilder();

            #region DI Container
            Builder.Services.AddDbContext<EntitiesContext>(context =>
            {
                context.UseLazyLoadingProxies()
                    .UseSqlServer
                    (Builder.Configuration.GetConnectionString("MyDB"));
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

            Builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(Options =>
                {
                    Options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Builder.Configuration["Jwt:Key"]!))
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
                Options.LoginPath = "/Account/Login";

            });


            Builder.Services.AddScoped<CategoryManager>();
            Builder.Services.AddScoped<BookingManager>();
            Builder.Services.AddScoped<BookingDetailsManager>();
            Builder.Services.AddScoped<ServiceAttachmentManager>();
            Builder.Services.AddScoped<ServiceManager>();
            Builder.Services.AddScoped<ReveiwManager>();
            Builder.Services.AddScoped<CheckListManager>();
            Builder.Services.AddScoped<FollowManager>();
            Builder.Services.AddScoped<PromoCodeManager>();
            Builder.Services.AddScoped<VendorManager>();
            Builder.Services.AddScoped<ServiceAttachmentManager>();
            Builder.Services.AddScoped<AccountManager>();

            //Builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, UesrClaimsFactory>();
            Builder.Services.AddControllers();
            #endregion
            var App = Builder.Build();
            App.UseCors();
            App.UseStaticFiles();
            App.UseAuthentication();
            App.UseAuthorization();
            App.MapControllerRoute("Default", "{Controller}/{Action=Index}/{id?}");


            App.Run();


            return 0;
        }

    }
}

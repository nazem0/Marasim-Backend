using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;

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
            Builder.Services.Configure<IdentityOptions>(Options =>
            {
                Options.Password.RequireNonAlphanumeric = false;
                Options.Password.RequireUppercase = false;

            });
            Builder.Services.ConfigureApplicationCookie(Options =>
            {
                Options.LoginPath = "/Account/SignIn";

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
            Builder.Services.AddScoped<UserManager>();
            Builder.Services.AddScoped<VendorManager>();
            Builder.Services.AddScoped<ServiceAttachmentManager>();

            //Builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, UesrClaimsFactory>();
            Builder.Services.AddControllers();
            #endregion
            var App = Builder.Build();
            App.UseStaticFiles();
            App.UseAuthentication();
            App.UseAuthorization();
            App.MapControllerRoute("Default", "{Controller}/{Action=Index}/{id?}");


            App.Run();


            return 0;
        }

    }
}

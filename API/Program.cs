using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Models;
using Repository;

namespace Marasim_Backend
{
    public class Program
    {
        public static int Main()
        {
            WebApplicationBuilder builder =
              WebApplication.CreateBuilder();

            #region DI Container
            builder.Services.AddDbContext<EntitiesContext>(i =>
            {
                i.UseLazyLoadingProxies().UseSqlServer(
                    builder.Configuration.GetConnectionString("MyDB"));
            });

            builder.Services.AddIdentity<User, IdentityRole>(i =>
            {
                i.Lockout.MaxFailedAccessAttempts = 2;
                i.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                i.User.RequireUniqueEmail = true;
                i.SignIn.RequireConfirmedPhoneNumber = false;
                i.SignIn.RequireConfirmedEmail = false;
                i.SignIn.RequireConfirmedAccount = false;
                i.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
            })
                .AddEntityFrameworkStores<EntitiesContext>()
                .AddDefaultTokenProviders();
            builder.Services.Configure<IdentityOptions>(i =>
            {
                i.Password.RequireNonAlphanumeric = false;
                i.Password.RequireUppercase = false;

            });
            builder.Services.ConfigureApplicationCookie(i =>
            {
                i.LoginPath = "/Account/SignIn";

            });
            //builder.Services.AddScoped(typeof(UnitOfWork));
            builder.Services.AddScoped(typeof(CategoryManager));
            builder.Services.AddScoped(typeof(BookingManager));
            builder.Services.AddScoped(typeof(BookingDetailsManager));
            builder.Services.AddScoped(typeof(ServiceAttachmentManager));
            builder.Services.AddScoped(typeof(ServiceManager));
            builder.Services.AddScoped(typeof(ReveiwManager));
            builder.Services.AddScoped(typeof(CheckListManager));

            builder.Services.AddScoped(typeof(FollowManager));
		builder.Services.AddScoped(typeof(PromoCodeManager));
		builder.Services.AddScoped(typeof(UserManager));
		builder.Services.AddScoped(typeof(VendorManager));
		builder.Services.AddScoped(typeof(ServiceAttachmentManager));



            //builder.Services.AddScoped(typeof(AccountManger));
            //builder.Services.AddScoped(typeof(RoleManager));
            //builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, UesrClaimsFactory>();
            builder.Services.AddControllers();
            #endregion

            var webApp = builder.Build();

            #region Middel Were
            //webApp.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory() + "/Content"),
            //    RequestPath = ""

            //});
            webApp.UseStaticFiles();
            webApp.UseAuthentication();
            webApp.UseAuthorization();
            webApp.MapControllerRoute("Default", "{Controller}/{Action=Index}/{id?}");

            #endregion

            webApp.Run();


            return 0;
        }

    }
}

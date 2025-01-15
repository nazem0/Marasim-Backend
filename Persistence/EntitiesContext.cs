using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.DataSeeding;
using Persistence.EntitiesConfigs;

namespace Persistence
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostAttachment> PostsAttachments { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
        public DbSet<React> Reacts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceAttachment> ServiceAttachments { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public DbSet<City> Cities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new CategoryConfig())
                .ApplyConfiguration(new CommentConfig())
                .ApplyConfiguration(new FollowConfig())
                .ApplyConfiguration(new PostConfig())
                .ApplyConfiguration(new PostAttachmentConfig())
                .ApplyConfiguration(new PromoCodeConfig())
                .ApplyConfiguration(new ReactConfig())
                .ApplyConfiguration(new ReservationConfig())
                .ApplyConfiguration(new ReviewConfig())
                .ApplyConfiguration(new PaymentConfig())
                .ApplyConfiguration(new WithdrawalConfig())
                .ApplyConfiguration(new ServiceConfig())
                .ApplyConfiguration(new ServiceAttachmentConfig())
                .ApplyConfiguration(new UserConfig())
                .ApplyConfiguration(new VendorConfig())
                .ApplyConfiguration(new InvitationConfig())
                .ApplyConfiguration(new GovernorateConfig())
                .ApplyConfiguration(new CityConfig())
                .SeedGovernorates()
                .SeedCities()
                .SeedCategories()
                .SeedRoles()
                .SeedUsers();

            base.OnModelCreating(modelBuilder);
        }
        //protected overrIde voId OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder
        //        //.UseLazyLoadingProxies()
        //        .UseSqlServer(@"Data Source=.; Initial Catalog=Marasim; 
        //            Integrated Security=True; TrustServerCertificate=True");

        //    //optionsBuilder.UseSqlServer(@"Data Source=localhost; Initial Catalog=Marasim; TrustServerCertificate=True; User Id=SA; Password=$aMer2030");
        //    base.OnConfiguring(optionsBuilder);
        //}

    }
}

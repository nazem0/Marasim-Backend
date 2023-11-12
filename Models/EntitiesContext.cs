using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Models
{
    public class EntitiesContext : IdentityDbContext<User>
    {
        public EntitiesContext(DbContextOptions options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CheckList> CheckLists { get; set; }
        public DbSet<CheckListItem> CheckListItems { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostAttachment> PostsAttachments { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
        public DbSet<React> Reacts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceAttachment> ServiceAttachments { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new CheckListConfig());
            modelBuilder.ApplyConfiguration(new CheckListItemConfig());
            modelBuilder.ApplyConfiguration(new CommentConfig());
            modelBuilder.ApplyConfiguration(new FollowConfig());
            modelBuilder.ApplyConfiguration(new PostConfig());
            modelBuilder.ApplyConfiguration(new PostAttachmentConfig());
            modelBuilder.ApplyConfiguration(new PromoCodeConfig());
            modelBuilder.ApplyConfiguration(new ReactConfig());
            modelBuilder.ApplyConfiguration(new ReservationConfig());
            modelBuilder.ApplyConfiguration(new ReviewConfig());
            modelBuilder.ApplyConfiguration(new PaymentConfig());
            modelBuilder.ApplyConfiguration(new ServiceConfig());
            modelBuilder.ApplyConfiguration(new ServiceAttachmentConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new VendorConfig());
            modelBuilder.ApplyConfiguration(new InvitationConfig());

            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
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

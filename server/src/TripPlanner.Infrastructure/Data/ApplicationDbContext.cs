using Microsoft.EntityFrameworkCore;
using TripPlanner.Core.Entities;

namespace TripPlanner.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;
        public DbSet<AuthenticationLog> AuthenticationLogs { get; set; } = null!;

        public DbSet<TripRequest> TripRequests { get; set; } = null!;
        public DbSet<TripPackage> TripPackages { get; set; } = null!;
        public DbSet<ItineraryDay> ItineraryDays { get; set; } = null!;
        
        public DbSet<Restaurant> Restaurants { get; set; } = null!;
        public DbSet<Attraction> Attractions { get; set; } = null!;
        public DbSet<Campsite> Campsites { get; set; } = null!;
        public DbSet<EmergencyService> EmergencyServices { get; set; } = null!;
        public DbSet<WeatherForecast> WeatherForecasts { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Coupon> Coupons { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;

        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<Invoice> Invoices { get; set; } = null!;
        public DbSet<Pdf> Pdfs { get; set; } = null!;

        public DbSet<AdminReview> AdminReviews { get; set; } = null!;
        public DbSet<BlogPost> BlogPosts { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<AuditLog> AuditLogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // UserRole Many-to-Many
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            // PostGIS Extension for PostgreSQL
            modelBuilder.HasPostgresExtension("postgis");
            modelBuilder.HasPostgresExtension("uuid-ossp");

            // JSONB Columns mapping
            modelBuilder.Entity<TripRequest>()
                .Property(t => t.Preferences).HasColumnType("jsonb");
            modelBuilder.Entity<TripPackage>()
                .Property(t => t.AiGenerationMetadata).HasColumnType("jsonb");
            modelBuilder.Entity<Campsite>()
                .Property(c => c.Amenities).HasColumnType("jsonb");
            modelBuilder.Entity<AuditLog>()
                .Property(a => a.OldValues).HasColumnType("jsonb");
            modelBuilder.Entity<AuditLog>()
                .Property(a => a.NewValues).HasColumnType("jsonb");

            // Spatial Indexes
            modelBuilder.Entity<Restaurant>()
                .HasIndex(r => r.Location)
                .HasMethod("GIST");
            modelBuilder.Entity<Attraction>()
                .HasIndex(a => a.Location)
                .HasMethod("GIST");
            modelBuilder.Entity<Campsite>()
                .HasIndex(c => c.Location)
                .HasMethod("GIST");

            // Soft Delete Filters (Global Query Filters)
            modelBuilder.Entity<User>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<Role>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<TripRequest>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<TripPackage>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<ItineraryDay>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<Restaurant>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<Attraction>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<Campsite>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<Category>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<Product>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<Coupon>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<Order>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<Invoice>().HasQueryFilter(e => e.DeletedAt == null);
            modelBuilder.Entity<BlogPost>().HasQueryFilter(e => e.DeletedAt == null);

            // Constraints and Indexes
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Role>().HasIndex(r => r.Name).IsUnique();
            modelBuilder.Entity<Product>().HasIndex(p => p.Sku).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Coupon>().HasIndex(c => c.Code).IsUnique();
            
            // Decimal precision
            modelBuilder.Entity<TripPackage>().Property(p => p.TotalEstimatedCost).HasPrecision(10, 2);
            modelBuilder.Entity<Restaurant>().Property(p => p.Rating).HasPrecision(3, 2);
            modelBuilder.Entity<Attraction>().Property(p => p.Cost).HasPrecision(10, 2);
            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(10, 2);
            modelBuilder.Entity<Coupon>().Property(p => p.DiscountAmount).HasPrecision(10, 2);
            modelBuilder.Entity<Order>().Property(p => p.Subtotal).HasPrecision(10, 2);
            modelBuilder.Entity<Order>().Property(p => p.DiscountApplied).HasPrecision(10, 2);
            modelBuilder.Entity<Order>().Property(p => p.TotalAmount).HasPrecision(10, 2);
            modelBuilder.Entity<OrderItem>().Property(p => p.UnitPrice).HasPrecision(10, 2);
            modelBuilder.Entity<Payment>().Property(p => p.Amount).HasPrecision(10, 2);
        }
    }
}

using CineMatrixAPI.Domain.Entities;
using CineMatrixAPI.Domain.Entities.Identities;
using CineMatrixAPI.Entities;
using CineMatrixAPI.Persistance.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CineMatrixAPI.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<ShowTime> ShowTimes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var guidAdmin = Guid.NewGuid().ToString();
            var guidUser = Guid.NewGuid().ToString();
            var guidAdminCreate = Guid.NewGuid().ToString();
            // Role Seed Data
            modelBuilder.Entity<AppRole>().HasData(
                new AppRole { Id = guidAdmin, Name = "Admin", NormalizedName = "ADMIN" },
                new AppRole { Id = guidUser, Name = "User", NormalizedName = "USER" }
            );

            // User Seed Data
            var hasher = new PasswordHasher<AppUser>();

            var user = new AppUser
            {
                Id = guidAdminCreate,
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "default",
                LastName = "default",
                Birthday = DateTime.UtcNow,
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true
            };

            user.PasswordHash = hasher.HashPassword(user, "Admin!23");

            modelBuilder.Entity<AppUser>().HasData(user);

            // User - Role Relationship Seed Data
            modelBuilder.Entity<AppUserRole>().HasData(
                new AppUserRole { UserId = guidAdminCreate, RoleId = guidAdmin } // Admin user is assigned the Admin role
            );

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieConfig).Assembly);

            //movie-branch many-to-many
            modelBuilder.Entity<ShowTime>().HasKey(t => new { t.Id});

            modelBuilder.Entity<ShowTime>()
            .HasOne<Movie>(t => t.Movie)
            .WithMany(m => m.ShowTimes)
            .HasForeignKey(t => t.MovieId);

            modelBuilder.Entity<ShowTime>()
            .HasOne<Branch>(t => t.Branch)
            .WithMany(b => b.ShowTimes)
            .HasForeignKey(t => t.BranchId);

            //User-booking one-to-many booking-many user-one
            modelBuilder.Entity<Booking>().
                HasOne<AppUser>(b => b.User).
                WithMany(u => u.Bookings).
                HasForeignKey(b => b.UserId);



            //ticket-booking one to one ticket-one booking-one
            //modelBuilder.Entity<Ticket>()
            //    .HasOne<Booking>(t => t.Booking)
            //    .WithOne(b => b.Ticket)
            //    .HasForeignKey<Booking>(b => b.TicketId);

            //review-user one-to-many user-one review-many
            modelBuilder.Entity<Review>().
                HasOne<AppUser>(r => r.User).
                WithMany(u => u.Reviews).
                HasForeignKey(r => r.UserId);

            //review-movie one to many review-many movie-one
            modelBuilder.Entity<Review>().
                HasOne<Movie>(r => r.Movie).
                WithMany(u => u.Reviews).
                HasForeignKey(r => r.MovieId);
        }
    }
}

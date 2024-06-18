using Microsoft.EntityFrameworkCore;

namespace BadmintonCenter.BusinessObject.Models
{
    public class BadmintonDbContext : DbContext
    {
        public BadmintonDbContext(DbContextOptions<BadmintonDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<BookingType> BookingTypes { get; set; } = null!;
        public DbSet<Court> Courts { get; set; } = null!;
        public DbSet<TimeSlot> TimeSlots { get; set; } = null!;
        public DbSet<BookingDetail> BookingDetails { get; set; } = null!;
        public DbSet<Package> Packages { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserPackage> UserPackages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Role table
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(p => p.RoleId);
                entity.Property(p => p.RoleName).IsRequired();
            });

            // 1-M relation between role and user
            modelBuilder.Entity<Role>()
                        .HasMany(d => d.Users)
                        .WithOne(e => e.Role)
                        .HasForeignKey(e => e.RoleId)
                        .IsRequired();

            // User table
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(p => p.UserId);
                entity.Property(p => p.UserName).HasMaxLength(20).IsRequired();
                entity.Property(p => p.PasswordSalt).IsRequired();
                entity.Property(p => p.PasswordHash).IsRequired();
                entity.Property(p => p.FullName).HasMaxLength(50).IsRequired();
                entity.Property(p => p.Email).HasMaxLength(50);
                entity.Property(p => p.PhoneNumber).HasMaxLength(10).IsRequired();
            });

            // 1-M relation between user and transaction
            modelBuilder.Entity<User>()
                        .HasMany(d => d.Transactions)
                        .WithOne(e => e.User)
                        .HasForeignKey(e => e.UserId)
                        .IsRequired();

            // 1-M relation between user and booking
            modelBuilder.Entity<User>()
                        .HasMany(d => d.Bookings)
                        .WithOne(e => e.User)
                        .HasForeignKey(e => e.UserId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

            // BookingType table
            modelBuilder.Entity<BookingType>(entity =>
            {
                entity.HasKey(p => p.BookingTypeId);
                entity.Property(p => p.Type).IsRequired();
            });

            // 1-M relation between bookingType and booking
            modelBuilder.Entity<BookingType>()
                        .HasMany(d => d.Bookings)
                        .WithOne(e => e.BookingType)
                        .HasForeignKey(e => e.BookingTypeId)
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

            // PaymentMethod table
            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.HasKey(p => p.PaymentMethodId);
                entity.Property(p => p.MethodName).HasMaxLength(20).IsRequired();
                entity.Property(p => p.Description).HasMaxLength(50).IsRequired();
            });

            // 1-M relation between paymentMethod and transaction
            modelBuilder.Entity<PaymentMethod>()
                        .HasMany(d => d.Transactions)
                        .WithOne(e => e.PaymentMethod)
                        .HasForeignKey(e => e.PaymentMethodId)
                        .IsRequired();

            // Court table
            modelBuilder.Entity<Court>(entity =>
            {
                entity.HasKey(p => p.CourtId);
                entity.Property(p => p.CourtName).HasMaxLength(10).IsRequired();
                entity.Property(p => p.Description).HasMaxLength(50).IsRequired();
                entity.Property(p => p.Status).IsRequired();
            });

            // TimeSlot table
            modelBuilder.Entity<TimeSlot>(entity =>
            {
                entity.HasKey(p => p.SlotTimeId);
                entity.Property(p => p.StartTime).IsRequired();
                entity.Property(p => p.EndTime).IsRequired();
                entity.Property(p => p.Time).IsRequired();
                entity.Property(p => p.Price).IsRequired();
            });

            // Package table
            modelBuilder.Entity<Package>(entity =>
            {
                entity.HasKey(p => p.PackageId);
                entity.Property(p => p.PackageName).HasMaxLength(20).IsRequired();
                entity.Property(p => p.Description).HasMaxLength(50).IsRequired();
                entity.Property(p => p.HourIncluded).IsRequired();
                entity.Property(p => p.Price).IsRequired();
            });

            // 1-M relation between paymentMethod and transaction
            modelBuilder.Entity<Package>()
                        .HasMany(d => d.Transactions)
                        .WithOne(e => e.Package)
                        .HasForeignKey(e => e.PackageId)
                        .IsRequired();

            // Booking table
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(p => p.BookingId);
                entity.Property(p => p.BookingDate).IsRequired();
                entity.Property(p => p.ValidDate).IsRequired();
                entity.Property(p => p.ExpiredDate).IsRequired();
                entity.Property(p => p.Status).IsRequired();
                entity.Property(p => p.TotalHour).IsRequired();
                entity.Property(p => p.TotalPrice).IsRequired();
            });

            // Transaction table
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(p => p.TransactionId);
                entity.Property(p => p.Price).IsRequired();
                entity.Property(p => p.PaymentMethodId).IsRequired();
                entity.Property(p => p.UserId).IsRequired();
                entity.Property(p => p.Status).IsRequired();
                entity.Property(p => p.CreatedAt).IsRequired();
                entity.Property(p => p.Description).HasMaxLength(50).IsRequired();
            });

            // 1-1 relation between booking and transaction
            modelBuilder.Entity<Booking>()
                        .HasOne(p => p.Transaction)
                        .WithOne(a => a.Booking)
                        .HasForeignKey<Booking>(p => p.BookingId)
                        .OnDelete(DeleteBehavior.Cascade);

            // M-M relation
            // UserPackage table
            modelBuilder.Entity<UserPackage>(entity =>
            {
                entity.HasKey(p => new {p.PackageId, p.UserId});
                entity.Property(p => p.HourRemaining).IsRequired();
                entity.Property(p => p.ValidInMonth).IsRequired();
            });
            modelBuilder.Entity<UserPackage>()
                        .HasOne(up => up.User)
                        .WithMany(t => t.UserPackages)
                        .HasForeignKey(p => p.UserId);
            modelBuilder.Entity<UserPackage>()
                        .HasOne(up => up.Package)
                        .WithMany(t => t.UserPackages)
                        .HasForeignKey(p => p.PackageId);

            // BookingDetail table
            modelBuilder.Entity<BookingDetail>(entity =>
            {
                entity.HasKey(p => new { p.CourtId, p.BookingId, p.TimeSlotId });
            });
            modelBuilder.Entity<BookingDetail>()
                        .HasOne(up => up.Booking)
                        .WithMany(t => t.BookingDetails)
                        .HasForeignKey(p => p.BookingId);
            modelBuilder.Entity<BookingDetail>()
                        .HasOne(up => up.Court)
                        .WithMany(t => t.BookingDetails)
                        .HasForeignKey(p => p.CourtId);
            modelBuilder.Entity<BookingDetail>()
                        .HasOne(up => up.TimeSlot)
                        .WithMany(t => t.BookingDetails)
                        .HasForeignKey(p => p.TimeSlotId);
        }
    }
}

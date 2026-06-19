using BCS240031_LeHoangNam_KiemTraGiuaki.Models;
using Microsoft.EntityFrameworkCore;

namespace BCS240031_LeHoangNam_KiemTraGiuaki.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Room_BCS240031> Rooms_BCS240031 => Set<Room_BCS240031>();
    public DbSet<RoomType_BCS240031> RoomTypes_BCS240031 => Set<RoomType_BCS240031>();
    public DbSet<RoomImage_BCS240031> RoomImages_BCS240031 => Set<RoomImage_BCS240031>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room_BCS240031>().ToTable("Rooms_BCS240031");
        modelBuilder.Entity<RoomType_BCS240031>().ToTable("RoomTypes_BCS240031");
        modelBuilder.Entity<RoomImage_BCS240031>().ToTable("RoomImages_BCS240031");

        modelBuilder.Entity<Room_BCS240031>()
            .HasIndex(r => new { r.RoomTypeId, r.Name })
            .IsUnique();

        modelBuilder.Entity<Room_BCS240031>()
            .HasOne(r => r.RoomType).WithMany(t => t.Rooms)
            .HasForeignKey(r => r.RoomTypeId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<RoomImage_BCS240031>()
            .HasOne(i => i.Room).WithMany(r => r.RoomImages)
            .HasForeignKey(i => i.RoomId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RoomType_BCS240031>().HasData(
            new RoomType_BCS240031 { Id = 1, Name = "Phòng tiêu chuẩn", Description = "Phòng khép kín cơ bản" },
            new RoomType_BCS240031 { Id = 2, Name = "Phòng có gác", Description = "Có gác lửng rộng rãi" },
            new RoomType_BCS240031 { Id = 3, Name = "Căn hộ mini", Description = "Có khu bếp riêng" });

        modelBuilder.Entity<Room_BCS240031>().HasData(
            new Room_BCS240031 { Id = 1, Name = "A101", Price = 2500000, Area = 20, IsAvailable = true, Description = "Tầng 1", RoomTypeId = 1 },
            new Room_BCS240031 { Id = 2, Name = "A202", Price = 3200000, Area = 28, IsAvailable = true, Description = "Ban công thoáng", RoomTypeId = 2 },
            new Room_BCS240031 { Id = 3, Name = "B301", Price = 4500000, Area = 35, IsAvailable = false, Description = "Đầy đủ nội thất", RoomTypeId = 3 },
            new Room_BCS240031 { Id = 4, Name = "B102", Price = 2700000, Area = 22, IsAvailable = true, Description = "Gần cổng", RoomTypeId = 1 },
            new Room_BCS240031 { Id = 5, Name = "C201", Price = 3500000, Area = 30, IsAvailable = false, Description = "Gác rộng", RoomTypeId = 2 });

        modelBuilder.Entity<RoomImage_BCS240031>().HasData(
            new RoomImage_BCS240031 { Id = 1, RoomId = 1, ImageUrl = "https://images.unsplash.com/photo-1522708323590-d24dbb6b0267?w=900", IsThumbnail = true },
            new RoomImage_BCS240031 { Id = 2, RoomId = 1, ImageUrl = "https://images.unsplash.com/photo-1505693416388-ac5ce068fe85?w=900", IsThumbnail = false },
            new RoomImage_BCS240031 { Id = 3, RoomId = 2, ImageUrl = "https://images.unsplash.com/photo-1560448204-e02f11c3d0e2?w=900", IsThumbnail = true });
    }
}

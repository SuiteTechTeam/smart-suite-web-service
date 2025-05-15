using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Commerce.Domain.Model.Entities;
using SweetManagerIotWebService.API.Communication.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Credentials;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Preferences;
using SweetManagerIotWebService.API.IAM.Domain.Model.Entities.Roles;
using SweetManagerIotWebService.API.Inventory.Domain.Model.Entities;
using SweetManagerIotWebService.API.OrganizationalManagement.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Aggregates;
using SweetManagerIotWebService.API.Reservations.Domain.Model.Entities;
using SweetManagerIotWebService.API.Inventory.Domain.Model.Aggregates;

namespace SweetManagerIotWebService.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public partial class SweetManagerContext : DbContext
{
    public SweetManagerContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        // Enable Audit Fields Interceptors
        builder.AddCreatedUpdatedInterceptor();
    }

    public SweetManagerContext(DbContextOptions<SweetManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AdminCredential> AdminCredentials { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<ContractOwner> ContractOwners { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<GuestCredential> GuestCredentials { get; set; }

    public virtual DbSet<GuestPreference> GuestPreferences { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<OwnerCredential> OwnerCredentials { get; set; }

    public virtual DbSet<PaymentCustomer> PaymentCustomers { get; set; }

    public virtual DbSet<PaymentOwner> PaymentOwners { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Supply> Supplies { get; set; }

    public virtual DbSet<SupplyRequest> SupplyRequests { get; set; }

    public virtual DbSet<TypeRoom> TypeRooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("admins");

            entity.HasIndex(e => e.RoleId, "role_id");

            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .HasColumnName("surname");

            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasColumnName("state");

            entity.HasIndex(e => e.HotelId, "hotel_id");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Admins)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("admins_ibfk_1");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Admins)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("admins_ibfk_2");
        });

        modelBuilder.Entity<AdminCredential>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PRIMARY");

            entity.ToTable("admin_credentials");

            entity.Property(e => e.AdminId).HasColumnName("admin_id").ValueGeneratedNever();
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");

            entity.HasOne(d => d.Admin).WithOne(p => p.AdminCredential)
                .HasForeignKey<AdminCredential>(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("admin_credentials_ibfk_1");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("bookings");

            entity.HasIndex(e => e.PaymentCustomerId, "payment_customer_id");

            entity.HasIndex(e => e.PreferenceId, "preference_id");

            entity.HasIndex(e => e.RoomId, "room_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(10)
                .HasColumnName("amount");
            entity.Property(e => e.Description)
                .HasMaxLength(5000)
                .HasColumnName("description");
            entity.Property(e => e.FinalDate)
                .HasColumnType("date")
                .HasColumnName("final_date");
            entity.Property(e => e.NightCount).HasColumnName("night_count");
            entity.Property(e => e.PaymentCustomerId).HasColumnName("payment_customer_id");
            entity.Property(e => e.PreferenceId).HasColumnName("preference_id");
            entity.Property(e => e.PriceRoom)
                .HasPrecision(10)
                .HasColumnName("price_room");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("start_date");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasColumnName("state");

            entity.HasOne(d => d.PaymentCustomer).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PaymentCustomerId)
                .HasConstraintName("bookings_ibfk_1");

            entity.HasOne(d => d.Preference).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PreferenceId)
                .HasConstraintName("bookings_ibfk_3");

            entity.HasOne(d => d.Room).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("bookings_ibfk_2");
        });

        modelBuilder.Entity<ContractOwner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("contract_owners");

            entity.HasIndex(e => e.OwnerId, "owner_id");

            entity.HasIndex(e => e.SubscriptionId, "subscription_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FinalDate)
                .HasColumnType("date")
                .HasColumnName("final_date");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.SubscriptionId).HasColumnName("subscription_id");

            entity.HasOne(d => d.Owner).WithMany(p => p.ContractOwners)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("contract_owners_ibfk_1");

            entity.HasOne(d => d.Subscription).WithMany(p => p.ContractOwners)
                .HasForeignKey(d => d.SubscriptionId)
                .HasConstraintName("contract_owners_ibfk_2");
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("guests");

            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedNever();

            entity.HasIndex(e => e.RoleId, "role_id");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.State)
                .HasDefaultValueSql("'active'")
                .HasColumnType("enum('ACTIVE','INACTIVE')")
                .HasColumnName("state");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");

            entity.HasOne(d => d.Role).WithMany(p => p.Guests)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("guests_ibfk_1");
        });

        modelBuilder.Entity<GuestCredential>(entity =>
        {
            entity.HasKey(e => e.GuestId).HasName("PRIMARY");

            entity.ToTable("guest_credentials");

            entity.Property(e => e.GuestId).HasColumnName("guest_id").ValueGeneratedNever();
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");

            entity.HasOne(d => d.Guest).WithOne(p => p.GuestCredential)
                .HasForeignKey<GuestCredential>(d => d.GuestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("guest_credentials_ibfk_1");
        });

        modelBuilder.Entity<GuestPreference>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("guest_preferences");

            entity.HasIndex(e => e.GuestId, "guest_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.GuestId).HasColumnName("guest_id");
            entity.Property(e => e.Temperature).HasColumnName("temperature");

            entity.HasOne(d => d.Guest).WithMany(p => p.GuestPreferences)
                .HasForeignKey(d => d.GuestId)
                .HasConstraintName("guest_preferences_ibfk_1");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("hotels");

            entity.HasIndex(e => e.OwnerId, "owner_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");

            entity.HasOne(d => d.Owner).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("hotels_ibfk_1");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notifications");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(5000)
                .HasColumnName("content");
            entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.SenderType)
                .HasMaxLength(20)
                .HasColumnName("sender_type");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("owners");

            entity.HasIndex(e => e.RoleId, "role_id");

            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasColumnName("state");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");

            entity.HasOne(d => d.Role).WithMany(p => p.Owners)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("owners_ibfk_1");
        });

        modelBuilder.Entity<OwnerCredential>(entity =>
        {
            entity.HasKey(e => e.OwnerId).HasName("PRIMARY");

            entity.ToTable("owner_credentials");

            entity.Property(e => e.OwnerId).HasColumnName("owner_id").ValueGeneratedNever();
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");

            entity.HasOne(d => d.Owner).WithOne(p => p.OwnerCredential)
                .HasForeignKey<OwnerCredential>(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("owner_credentials_ibfk_1");
        });

        modelBuilder.Entity<PaymentCustomer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("payment_customers");

            entity.HasIndex(e => e.GuestId, "guest_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FinalAmount)
                .HasPrecision(10)
                .HasColumnName("final_amount");
            entity.Property(e => e.GuestId).HasColumnName("guest_id");

            entity.HasOne(d => d.Guest).WithMany(p => p.PaymentCustomers)
                .HasForeignKey(d => d.GuestId)
                .HasConstraintName("payment_customers_ibfk_1");
        });

        modelBuilder.Entity<PaymentOwner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("payment_owners");

            entity.HasIndex(e => e.OwnerId, "owner_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.FinalAmount)
                .HasPrecision(10)
                .HasColumnName("final_amount");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");

            entity.HasOne(d => d.Owner).WithMany(p => p.PaymentOwners)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("payment_owners_ibfk_1");
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("providers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasColumnName("state");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");


            entity.ToTable("roles");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rooms");

            entity.HasIndex(e => e.HotelId, "hotel_id");

            entity.HasIndex(e => e.TypeRoomId, "type_room_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.State)
                .HasDefaultValueSql("'available'")
                .HasColumnType("enum('available','occupied','maintenance')")
                .HasColumnName("state");
            entity.Property(e => e.TypeRoomId).HasColumnName("type_room_id");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("rooms_ibfk_2");

            entity.HasOne(d => d.TypeRoom).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.TypeRoomId)
                .HasConstraintName("rooms_ibfk_1");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("subscriptions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(200)
                .HasColumnName("content");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10)
                .HasColumnName("price");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("supplies");

            entity.HasIndex(e => e.HotelId, "hotel_id");

            entity.HasIndex(e => e.ProviderId, "provider_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10)
                .HasColumnName("price");
            entity.Property(e => e.ProviderId).HasColumnName("provider_id");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasColumnName("state");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("supplies_ibfk_2");

            entity.HasOne(d => d.Provider).WithMany(p => p.Supplies)
                .HasForeignKey(d => d.ProviderId)
                .HasConstraintName("supplies_ibfk_1");
        });

        modelBuilder.Entity<SupplyRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("supply_requests");

            entity.HasIndex(e => e.PaymentOwnerId, "payment_owner_id");

            entity.HasIndex(e => e.SupplyId, "supply_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(10)
                .HasColumnName("amount");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.PaymentOwnerId).HasColumnName("payment_owner_id");
            entity.Property(e => e.SupplyId).HasColumnName("supply_id");

            entity.HasOne(d => d.PaymentOwner).WithMany(p => p.SupplyRequests)
                .HasForeignKey(d => d.PaymentOwnerId)
                .HasConstraintName("supply_requests_ibfk_1");

            entity.HasOne(d => d.Supply).WithMany(p => p.SupplyRequests)
                .HasForeignKey(d => d.SupplyId)
                .HasConstraintName("supply_requests_ibfk_2");
        });

        modelBuilder.Entity<TypeRoom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("type_rooms");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Price)
                .HasPrecision(10)
                .HasColumnName("price");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

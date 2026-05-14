using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using System.Reflection;

namespace POS.Infrastructure.Persistences.Contexts;

public partial class Pos2Context : DbContext
{
    public Pos2Context()
    {
    }

    public Pos2Context(DbContextOptions<Pos2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<BranchOffice> BranchOffices { get; set; }

    public virtual DbSet<Business> Businesses { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuRole> MenuRoles { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Purcharse> Purcharses { get; set; }

    public virtual DbSet<PurcharseDetail> PurcharseDetails { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleDetail> SaleDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UsersBranchOffice> UsersBranchOffices { get; set; }


    public virtual DbSet<VoucherDocumentType> VoucherDocumentTypes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

        // Primero aplica las configuraciones automáticas
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // AL FINAL, fuerzas los ignorados para que nadie los sobrescriba
        modelBuilder.Entity<SaleDetail>(entity =>
        {
            entity.ToTable("SaleDetails");
            entity.HasKey(e => new { e.SaleId, e.ProductId });

            entity.Ignore(e => e.Id);
            entity.Ignore(e => e.AuditCreateDate);
            entity.Ignore(e => e.AuditCreateUser);
            entity.Ignore(e => e.AuditUpdateDate);
            entity.Ignore(e => e.AuditUpdateUser);
            entity.Ignore(e => e.AuditDeleteDate);
            entity.Ignore(e => e.AuditDeleteUser);
            entity.Ignore(e => e.State);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

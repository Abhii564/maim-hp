using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace maximFinal.Models
{
    public partial class MaximContext : DbContext
    {
        public MaximContext()
        {
        }

        public MaximContext(DbContextOptions<MaximContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<ApplicationLink> ApplicationLink { get; set; }
        public virtual DbSet<Attachment> Attachment { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryLink> CategoryLink { get; set; }
        public virtual DbSet<Industry> Industry { get; set; }
        public virtual DbSet<IndustryLink> IndustryLink { get; set; }
        public virtual DbSet<MiscAttachment> MiscAttachment { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Standard> Standard { get; set; }
        public virtual DbSet<StandardLink> StandardLink { get; set; }
        public virtual DbSet<StandardParent> StandardParent { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=maxim123.database.windows.net;Database=Maxim;User Id=maximuser;Password=Locke.123456;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(3200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Thumbnail)
                    .HasColumnName("thumbnail")
                    .HasMaxLength(4000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ApplicationLink>(entity =>
            {
                entity.ToTable("Application_link");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApplicationId)
                    .HasColumnName("application_id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.ApplicationLink)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Application_link_Application");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ApplicationLink)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Application_link_Product");
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.ThumbnailTemp)
                    .HasColumnName("thumbnail_temp")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Attachment)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Attachment_Product");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Keywords)
                    .HasColumnName("keywords")
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ThumbnailTemp)
                    .HasColumnName("thumbnail_temp")
                    .HasMaxLength(4000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Keywords)
                    .HasColumnName("keywords")
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ThumbnailTemp)
                    .HasColumnName("thumbnail_temp")
                    .HasMaxLength(4000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CategoryLink>(entity =>
            {
                entity.ToTable("Category_Link");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryLink)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Link_Category");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CategoryLink)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Link_Product");
            });

            modelBuilder.Entity<Industry>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Keywords)
                    .HasColumnName("keywords")
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ThumbnailTemp)
                    .HasColumnName("thumbnail_temp")
                    .HasMaxLength(4000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IndustryLink>(entity =>
            {
                entity.ToTable("Industry_Link");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IndustryId)
                    .HasColumnName("industry_id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Industry)
                    .WithMany(p => p.IndustryLink)
                    .HasForeignKey(d => d.IndustryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Industry_Link_Industry");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.IndustryLink)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Industry_Link_Product");
            });

            modelBuilder.Entity<MiscAttachment>(entity =>
            {
                entity.HasKey(e => e.PId);

                entity.ToTable("Misc_Attachment");

                entity.Property(e => e.PId)
                    .HasColumnName("p_id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Attachment)
                    .HasColumnName("attachment")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.FileType)
                    .HasColumnName("file_type")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.MiscAttachment)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Misc_Attachment_Brand");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("ix_product");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.BrandId)
                    .HasColumnName("brand_id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .IsUnicode(false);

                entity.Property(e => e.Featured).HasColumnName("featured");

                entity.Property(e => e.Features).HasColumnName("features");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Specification).HasColumnName("specification");

                entity.Property(e => e.TempThumbnail)
                    .HasColumnName("temp_thumbnail")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Brand");
            });

            modelBuilder.Entity<Standard>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Keywords)
                    .HasColumnName("keywords")
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ParentId)
                    .HasColumnName("parent_id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.ThumbnailTemp)
                    .HasColumnName("thumbnail_temp")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Standard)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Standard_Standard_Parent");
            });

            modelBuilder.Entity<StandardLink>(entity =>
            {
                entity.ToTable("Standard_Link");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.StandardId)
                    .HasColumnName("standard_id")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.StandardLink)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Standard_Link_Product1");

                entity.HasOne(d => d.Standard)
                    .WithMany(p => p.StandardLink)
                    .HasForeignKey(d => d.StandardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Standard_Link_Product");
            });

            modelBuilder.Entity<StandardParent>(entity =>
            {
                entity.ToTable("Standard_Parent");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Keywords)
                    .HasColumnName("keywords")
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ThumbnailTemp)
                    .HasColumnName("thumbnail_temp")
                    .HasMaxLength(4000)
                    .IsUnicode(false);
            });
        }
    }
}

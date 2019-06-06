using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Films_Form
{
    public partial class Films1Context : DbContext
    {
       public Films1Context()
        {
        }

        public Films1Context(DbContextOptions<Films1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Buy> Buy { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Film> Film { get; set; }
        public virtual DbSet<FilmsToGenre> FilmsToGenre { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Purchase> Purchase { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=desktop-jusih42\\sqlexpress;Initial Catalog=Films1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", x => x.UseNetTopologySuite());
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Buy>(entity =>
            {
                entity.HasKey(e => e.BId)
                    .HasName("PK_BUY");

                entity.HasIndex(e => e.CId)
                    .HasName("Do_FK");

                entity.Property(e => e.BId).HasColumnName("B_ID");

                entity.Property(e => e.BPrice).HasColumnName("B_price");

                entity.Property(e => e.CId).HasColumnName("C_ID");

                entity.HasOne(d => d.C)
                    .WithMany(p => p.Buy)
                    .HasForeignKey(d => d.CId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BUY_DO_CLIENT");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.CId)
                    .HasName("PK_CLIENT");

                entity.Property(e => e.CId).HasColumnName("C_ID");

                entity.Property(e => e.CCardNumber)
                    .HasColumnName("C_Card_number")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CCvv).HasColumnName("C_CVV");

                entity.Property(e => e.CDate)
                    .HasColumnName("C_Date")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CFio)
                    .HasColumnName("C_FIO")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CLogin)
                    .IsRequired()
                    .HasColumnName("C_Login")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CPassword)
                    .IsRequired()
                    .HasColumnName("C_Password")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasKey(e => e.FId)
                    .HasName("PK_FILM");

                entity.Property(e => e.FId).HasColumnName("F_ID");

                entity.Property(e => e.FAge).HasColumnName("F_Age");

                entity.Property(e => e.FDesc)
                    .IsRequired()
                    .HasColumnName("F_desc")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FImdb)
                    .HasColumnName("F_IMDB")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FName)
                    .IsRequired()
                    .HasColumnName("F_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FPrice).HasColumnName("F_Price");
            });

            modelBuilder.Entity<FilmsToGenre>(entity =>
            {
                entity.HasKey(e => new { e.GId, e.FId })
                    .HasName("PK_FILMS TO GENRE");

                entity.ToTable("Films to genre");

                entity.HasIndex(e => e.FId)
                    .HasName("Have2_FK");

                entity.HasIndex(e => e.GId)
                    .HasName("Have_FK");

                entity.Property(e => e.GId).HasColumnName("G_ID");

                entity.Property(e => e.FId).HasColumnName("F_ID");

                entity.HasOne(d => d.F)
                    .WithMany(p => p.FilmsToGenre)
                    .HasForeignKey(d => d.FId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FILMS TO_HAVE2_FILM");

                entity.HasOne(d => d.G)
                    .WithMany(p => p.FilmsToGenre)
                    .HasForeignKey(d => d.GId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FILMS TO_HAVE_GENRE");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.GId)
                    .HasName("PK_GENRE");

                entity.Property(e => e.GId).HasColumnName("G_ID");

                entity.Property(e => e.GName)
                    .IsRequired()
                    .HasColumnName("G_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(e => new { e.BId, e.FId })
                    .HasName("PK_PURCHASE");

                entity.HasIndex(e => e.BId)
                    .HasName("Purchase_FK");

                entity.HasIndex(e => e.FId)
                    .HasName("Purchase2_FK");

                entity.Property(e => e.BId).HasColumnName("B_ID");

                entity.Property(e => e.FId).HasColumnName("F_ID");

                entity.HasOne(d => d.B)
                    .WithMany(p => p.Purchase)
                    .HasForeignKey(d => d.BId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PURCHASE_PURCHASE_BUY");

                entity.HasOne(d => d.F)
                    .WithMany(p => p.Purchase)
                    .HasForeignKey(d => d.FId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PURCHASE_PURCHASE2_FILM");
            });
        }
    }
}

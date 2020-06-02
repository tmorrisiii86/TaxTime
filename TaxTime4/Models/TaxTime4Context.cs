using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TaxTime4.Models
{
    public partial class TaxTime4Context : DbContext
    {
        public TaxTime4Context()
        {
        }

        public TaxTime4Context(DbContextOptions<TaxTime4Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Dependent> Dependent { get; set; }
        public virtual DbSet<Deposit> Deposit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=TaxTime4;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("address");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasColumnName("address1")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasColumnName("address2")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustId).HasColumnName("cust_id");

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("last_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasColumnName("zip_code")
                    .HasColumnType("numeric(5, 0)");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.CustId)
                    .HasConstraintName("FK__address__cust_id__38996AB5");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.CustId);

                entity.ToTable("appointment");

                entity.Property(e => e.CustId)
                    .HasColumnName("cust_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.LastAppt)
                    .HasColumnName("last_appt")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("last_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.NextAppt)
                    .HasColumnName("next_appt")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Cust)
                    .WithOne(p => p.Appointment)
                    .HasForeignKey<Appointment>(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__appointme__cust___412EB0B6");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("contact");

                entity.Property(e => e.ContactId).HasColumnName("contact_id");

                entity.Property(e => e.Cell1)
                    .HasColumnName("cell1")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Cell2)
                    .HasColumnName("cell2")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Cell3)
                    .HasColumnName("cell3")
                    .HasColumnType("numeric(4, 0)");

                entity.Property(e => e.CustId).HasColumnName("cust_id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Home1)
                    .HasColumnName("home1")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Home2)
                    .HasColumnName("home2")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Home3)
                    .HasColumnName("home3")
                    .HasColumnType("numeric(4, 0)");

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("last_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.Work1)
                    .HasColumnName("work1")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Work2)
                    .HasColumnName("work2")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Work3)
                    .HasColumnName("work3")
                    .HasColumnType("numeric(4, 0)");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.Contact)
                    .HasForeignKey(d => d.CustId)
                    .HasConstraintName("FK__contact__cust_id__3B75D760");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustId);

                entity.ToTable("customer");

                entity.Property(e => e.CustId).HasColumnName("cust_id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("last_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ssn1)
                    .HasColumnName("ssn1")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Ssn2)
                    .HasColumnName("ssn2")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Ssn3)
                    .HasColumnName("ssn3")
                    .HasColumnType("numeric(4, 0)");
            });

            modelBuilder.Entity<Dependent>(entity =>
            {
                entity.HasKey(e => e.DepId);

                entity.ToTable("dependent");

                entity.Property(e => e.DepId).HasColumnName("dep_id");

                entity.Property(e => e.CustId).HasColumnName("cust_id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("last_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ssn1)
                    .HasColumnName("ssn1")
                    .HasColumnType("numeric(3, 0)");

                entity.Property(e => e.Ssn2)
                    .HasColumnName("ssn2")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Ssn3)
                    .HasColumnName("ssn3")
                    .HasColumnType("numeric(4, 0)");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.Dependent)
                    .HasForeignKey(d => d.CustId)
                    .HasConstraintName("FK__dependent__cust___3E52440B");
            });

            modelBuilder.Entity<Deposit>(entity =>
            {
                entity.HasKey(e => e.CustId);

                entity.ToTable("deposit");

                entity.Property(e => e.CustId)
                    .HasColumnName("cust_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Account)
                    .HasColumnName("account")
                    .HasColumnType("numeric(12, 0)");

                entity.Property(e => e.LastUpdated)
                    .HasColumnName("last_updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.Routing)
                    .HasColumnName("routing")
                    .HasColumnType("numeric(9, 0)");

                entity.HasOne(d => d.Cust)
                    .WithOne(p => p.Deposit)
                    .HasForeignKey<Deposit>(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__deposit__cust_id__440B1D61");
            });
        }
    }
}

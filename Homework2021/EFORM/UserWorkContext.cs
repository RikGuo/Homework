using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Homework2021.EFORM
{
    public partial class UserWorkContext : DbContext
    {
        
        public UserWorkContext(DbContextOptions<UserWorkContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRefGroup> UserRefGroup { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=UserWork;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Company).HasMaxLength(100);

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("User_Id");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.Desc).HasMaxLength(200);

                entity.Property(e => e.GroupName).HasMaxLength(50);

                entity.Property(e => e.ModifyDate).HasColumnType("date");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.ModifyDate).HasColumnType("date");

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.UserId).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(100);
            });

            modelBuilder.Entity<UserRefGroup>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.GroupId).HasColumnName("Group_Id");

                entity.Property(e => e.ModifyDate).HasColumnType("date");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UserRefGroup)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_UserRefGroup_Group");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRefGroup)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserRefGroup_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

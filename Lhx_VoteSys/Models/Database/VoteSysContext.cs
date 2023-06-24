using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Lhx_VoteSys.Models.Database
{
    public partial class VoteSysContext : DbContext
    {
        public VoteSysContext()
        {
        }

        public VoteSysContext(DbContextOptions<VoteSysContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Enterprise> Enterprises { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VoteItem> VoteItems { get; set; }
        public virtual DbSet<VoteList> VoteLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=VoteSys;Integrated Security=SSPI");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_PRC_CI_AS");

            modelBuilder.Entity<Enterprise>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Enterprise");

                entity.Property(e => e.Likes).HasColumnName("likes");

                entity.Property(e => e.Projectid).HasColumnName("projectid");

                entity.Property(e => e.Projectname)
                    .IsRequired()
                    .HasColumnName("projectname");

                entity.Property(e => e.Year)
                    .IsRequired()
                    .HasColumnName("year");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<VoteItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("VoteItem");

                entity.Property(e => e.Awards).HasColumnName("awards");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Likes).HasColumnName("likes");

                entity.Property(e => e.Photo).HasColumnName("photo");

                entity.Property(e => e.Projectname)
                    .IsRequired()
                    .HasColumnName("projectname");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Verify).HasColumnName("verify");
            });

            modelBuilder.Entity<VoteList>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("VoteList");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Voteid).HasColumnName("voteid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

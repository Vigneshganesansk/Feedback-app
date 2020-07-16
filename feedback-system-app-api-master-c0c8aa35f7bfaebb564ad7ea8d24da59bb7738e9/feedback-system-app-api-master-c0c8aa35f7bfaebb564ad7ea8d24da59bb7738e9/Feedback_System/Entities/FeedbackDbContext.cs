using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Feedback_System.Entities
{
    public partial class FeedbackDbContext : DbContext
    {
        public FeedbackDbContext()
        {
        }

        public FeedbackDbContext(DbContextOptions<FeedbackDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answers> Answers { get; set; }
        public virtual DbSet<Bumaster> Bumaster { get; set; }
        public virtual DbSet<EmployeeUserMaster> EmployeeUserMaster { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<LocationMaster> LocationMaster { get; set; }
        public virtual DbSet<Participants> Participants { get; set; }
        public virtual DbSet<ParticipationStatusMaster> ParticipationStatusMaster { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<RoleMaster> RoleMaster { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DOTNET;Database=FeedbackDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answers>(entity =>
            {
                entity.HasKey(e => e.AnsId);

                entity.Property(e => e.QIdQuestion).HasColumnName("Q_ID_Question");

                entity.HasOne(d => d.QIdQuestionNavigation)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QIdQuestion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answers_Questions");
            });

            modelBuilder.Entity<Bumaster>(entity =>
            {
                entity.HasKey(e => e.Buid);

                entity.ToTable("BUMaster");

                entity.Property(e => e.Buid).HasColumnName("BUID");

                entity.Property(e => e.BuheadId).HasColumnName("BUHeadID");

                entity.Property(e => e.Buname)
                    .IsRequired()
                    .HasColumnName("BUName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmployeeUserMaster>(entity =>
            {
                entity.HasKey(e => e.AssociateId);

                entity.Property(e => e.AssociateId)
                    .HasColumnName("AssociateID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.AddedUserId).HasColumnName("AddedUserID");

                entity.Property(e => e.AssociateName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Buid).HasColumnName("BUID");

                entity.Property(e => e.DeletedDate)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedUserId).HasColumnName("DeletedUserID");

                entity.Property(e => e.Designation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Salt)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Bu)
                    .WithMany(p => p.EmployeeUserMaster)
                    .HasForeignKey(d => d.Buid)
                    .HasConstraintName("FK_EmployeeUserMaster_BUMaster");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.EmployeeUserMaster)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_EmployeeUserMaster_RoleMaster");
            });

            modelBuilder.Entity<Events>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.Property(e => e.EventId)
                    .HasColumnName("EventID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AddedDate).HasColumnType("datetime");

                entity.Property(e => e.AddedUserId).HasColumnName("AddedUserID");

                entity.Property(e => e.BeneficiaryName).IsUnicode(false);

                entity.Property(e => e.CouncilName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedDate).HasColumnType("datetime");

                entity.Property(e => e.DeletedUserId).HasColumnName("DeletedUserID");

                entity.Property(e => e.EventDate).HasColumnType("datetime");

                entity.Property(e => e.EventDescription).IsUnicode(false);

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EventStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OverallVolenteerHours).HasColumnType("decimal(11, 2)");

                entity.Property(e => e.TotalTravelHours).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TotalVolenteerHours).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Venue).IsUnicode(false);
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.Property(e => e.Guid).ValueGeneratedNever();

                entity.Property(e => e.Answer).IsRequired();

                entity.Property(e => e.EventId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Question).IsRequired();
            });

            modelBuilder.Entity<LocationMaster>(entity =>
            {
                entity.HasKey(e => e.LocationId);

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Participants>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.Property(e => e.Guid).ValueGeneratedNever();

                entity.Property(e => e.AssociateId).HasColumnName("AssociateID");

                entity.Property(e => e.AssociateName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EventId)
                    .IsRequired()
                    .HasColumnName("EventID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Iiepcategory)
                    .HasColumnName("IIEPCategory")
                    .IsUnicode(false);

                entity.Property(e => e.ParticipationStatusId).HasColumnName("ParticipationStatusID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.TravelHours).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.VolenteerHours).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Associate)
                    .WithMany(p => p.Participants)
                    .HasForeignKey(d => d.AssociateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Participants_EmployeeUserMaster");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Participants)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Participants_Events");

                entity.HasOne(d => d.ParticipationStatus)
                    .WithMany(p => p.Participants)
                    .HasForeignKey(d => d.ParticipationStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Participants_ParticipationStatusMaster");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Participants)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Participants_RoleMaster");
            });

            modelBuilder.Entity<ParticipationStatusMaster>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Questions>(entity =>
            {
                entity.HasKey(e => e.QId);

                entity.Property(e => e.QId).HasColumnName("Q_ID");

                entity.Property(e => e.Fbtype)
                    .IsRequired()
                    .HasColumnName("FBType")
                    .HasMaxLength(50);

                entity.Property(e => e.QuestionTextArea).IsRequired();

                entity.Property(e => e.RadioAnswer)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RoleMaster>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}

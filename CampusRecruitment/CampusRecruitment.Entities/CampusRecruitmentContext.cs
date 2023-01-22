using CampusRecruitment.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusRecruitment.Entities
{
    public partial class CampusRecruitmentContext : DbContext
    {
        public CampusRecruitmentContext(DbContextOptions<CampusRecruitmentContext> options) : base(options)
        {

        }

        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<DepartmentCoreAreaMapping> DepartmentCoreAreaMappings { get; set; }

        public virtual DbSet<Interview> Interviews { get; set; }

        public virtual DbSet<InterviewHistory> InterviewHistories { get; set; }

        public virtual DbSet<Invite> Invites { get; set; }

        public virtual DbSet<JobOpening> JobOpenings { get; set; }

        public virtual DbSet<JobOpeningCoreAreaMapping> JobOpeningCoreAreaMappings { get; set; }

        public virtual DbSet<LookUp> LookUps { get; set; }

        public virtual DbSet<LookUpGroup> LookUpGroups { get; set; }

        public virtual DbSet<Offer> Offers { get; set; }

        public virtual DbSet<Organization> Organizations { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DepartmentId).HasName("PK_Department_DepartmentId");

                entity.ToTable("Department");

                entity.HasOne(d => d.DepartmentType).WithMany(p => p.Departments)
                    .HasForeignKey(d => d.DepartmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LookUp_DepartmentTypeId");

                entity.HasOne(d => d.Organization).WithMany(p => p.Departments)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Department_OrganizationId");
            });

            modelBuilder.Entity<DepartmentCoreAreaMapping>(entity =>
            {
                entity.HasKey(e => e.DepartmentCoreAreaMappingId).HasName("PK_DepartmentCoreAreaMapping_DepartmentCoreAreaMappingId");

                entity.ToTable("DepartmentCoreAreaMapping");

                entity.HasOne(d => d.CoreAreaType).WithMany(p => p.DepartmentCoreAreaMappings)
                    .HasForeignKey(d => d.CoreAreaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LookUp_CoreAreaTypeId");

                entity.HasOne(d => d.Department).WithMany(p => p.DepartmentCoreAreaMappings)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Department_DepartmentId");
            });

            modelBuilder.Entity<Interview>(entity =>
            {
                entity.HasKey(e => e.InterviewId).HasName("PK_Interview_InterviewId");

                entity.ToTable("Interview");

                entity.Property(e => e.DateOfInterview).HasColumnType("datetime");

                entity.HasOne(d => d.JobOpening).WithMany(p => p.Interviews)
                    .HasForeignKey(d => d.JobOpeningId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Interview_JobOpeningId");

                entity.HasOne(d => d.RoundType).WithMany(p => p.InterviewRoundTypes)
                    .HasForeignKey(d => d.RoundTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Interview_RoundTypeId");

                entity.HasOne(d => d.StatusType).WithMany(p => p.InterviewStatusTypes)
                    .HasForeignKey(d => d.StatusTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Interview_StatusTypeId");

                entity.HasOne(d => d.Student).WithMany(p => p.Interviews)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Interview_StudentId");
            });

            modelBuilder.Entity<InterviewHistory>(entity =>
            {
                entity.HasKey(e => e.InterviewHistoryId).HasName("PK_InterviewHistory_InterviewHistoryId");

                entity.ToTable("InterviewHistory");

                entity.Property(e => e.AttendedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Interview).WithMany(p => p.InterviewHistories)
                    .HasForeignKey(d => d.InterviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Interview_InterviewId");

                entity.HasOne(d => d.RoundType).WithMany(p => p.InterviewHistoryRoundTypes)
                    .HasForeignKey(d => d.RoundTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InterviewHistory_RoundTypeId");

                entity.HasOne(d => d.StatusType).WithMany(p => p.InterviewHistoryStatusTypes)
                    .HasForeignKey(d => d.StatusTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InterviewHistory_StatusTypeId");
            });

            modelBuilder.Entity<Invite>(entity =>
            {
                entity.HasKey(e => e.InviteId).HasName("PK_Invite_InviteId");

                entity.ToTable("Invite");

                entity.Property(e => e.DatetimeOfInvite).HasColumnType("datetime");

                entity.HasOne(d => d.JobOpening).WithMany(p => p.Invites)
                    .HasForeignKey(d => d.JobOpeningId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invite_JobOpeningId");

                entity.HasOne(d => d.Organization).WithMany(p => p.Invites)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invite_OrganizationId");
            });

            modelBuilder.Entity<JobOpening>(entity =>
            {
                entity.HasKey(e => e.JobOpeningId).HasName("PK_JobOpening_JobOpeningId");

                entity.ToTable("JobOpening");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
                entity.Property(e => e.JobDescription).IsUnicode(false);
                entity.Property(e => e.MinCgpaorPercent)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("MinCGPAOrPercent");
                entity.Property(e => e.Qualification)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.Role)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmploymentType).WithMany(p => p.JobOpenings)
                    .HasForeignKey(d => d.EmploymentTypeId)
                    .HasConstraintName("FK_JobOpening_EmploymentTypeId");
            });

            modelBuilder.Entity<JobOpeningCoreAreaMapping>(entity =>
            {
                entity.HasKey(e => e.JobOpeningCoreAreaMappingId).HasName("PK_JobOpeningCoreAreaMapping_JobOpeningCoreAreaMappingId");

                entity.ToTable("JobOpeningCoreAreaMapping");

                entity.HasOne(d => d.CoreAreaType).WithMany(p => p.JobOpeningCoreAreaMappings)
                    .HasForeignKey(d => d.CoreAreaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobOpeningCoreAreaMapping_CoreAreaTypeId");

                entity.HasOne(d => d.JobOpening).WithMany(p => p.JobOpeningCoreAreaMappings)
                    .HasForeignKey(d => d.JobOpeningId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JobOpeningCoreAreaMapping_JobOpeningId");
            });

            modelBuilder.Entity<LookUp>(entity =>
            {
                entity.HasKey(e => e.LookUpId).HasName("PK_LookUp_LookUpId");

                entity.ToTable("LookUp");

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.LookUpGroup).WithMany(p => p.LookUps)
                    .HasForeignKey(d => d.LookUpGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LookUpGroup_LookUpGroupId");
            });

            modelBuilder.Entity<LookUpGroup>(entity =>
            {
                entity.HasKey(e => e.LookUpGroupId).HasName("PK_LookUpGroup_LookUpGroupId");

                entity.ToTable("LookUpGroup");

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.HasKey(e => e.OfferId).HasName("PK_Offer_OfferId");

                entity.ToTable("Offer");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.FilePath)
                    .HasMaxLength(2000)
                    .IsUnicode(false);
                entity.Property(e => e.JoiningDate).HasColumnType("datetime");
                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Interview).WithMany(p => p.Offers)
                    .HasForeignKey(d => d.InterviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Offer_InterviewId");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasKey(e => e.OrganizationId).HasName("PK_Organization_OrganizationId");

                entity.ToTable("Organization");

                entity.Property(e => e.Contact)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasMaxLength(2000)
                    .IsUnicode(false);
                entity.Property(e => e.Website)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.HasOne(d => d.OrganizationSubType).WithMany(p => p.OrganizationOrganizationSubTypes)
                    .HasForeignKey(d => d.OrganizationSubTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LookUp_OrganizationSubTypeId");

                entity.HasOne(d => d.OrganizationType).WithMany(p => p.OrganizationOrganizationTypes)
                    .HasForeignKey(d => d.OrganizationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LookUp_OrganizationTypeId");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentId).HasName("PK_Student_StudentId");

                entity.ToTable("Student");

                entity.Property(e => e.CgpaorPercentage)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("CGPAOrPercentage");
                entity.Property(e => e.Contact)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.DateOfBirth)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.FirstName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.LastName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Department).WithMany(p => p.Students)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_DepartmentId");

                entity.HasOne(d => d.Status).WithMany(p => p.Students)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LookUp_StatusId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK_User_UserId");

                entity.ToTable("User");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("FIrstName");
                entity.Property(e => e.LastName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
                entity.Property(e => e.Password)
                    .HasMaxLength(1000)
                    .IsUnicode(false);
                entity.Property(e => e.UserName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Organization).WithMany(p => p.Users)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_OrganizationId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
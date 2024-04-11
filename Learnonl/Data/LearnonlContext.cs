﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Learnonl.Data;

public partial class LearnonlContext : DbContext
{
    public LearnonlContext()
    {
    }

    public LearnonlContext(DbContextOptions<LearnonlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Assessment> Assessments { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Content> Contents { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Coursecontent> Coursecontents { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Progress> Progresses { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

/*    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-2GK9OQC\\MSSQLSERVER24;Initial Catalog=learnonl;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
*/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Account__1788CCACBCD58C39");

            entity.ToTable("Account");

            entity.HasIndex(e => e.Email, "UQ__Account__A9D1053425869939").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Account__A9D10534508682EC").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Avatar).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.UserRole)
                .HasMaxLength(20)
                .HasDefaultValue("0");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<Assessment>(entity =>
        {
            entity.HasKey(e => e.AssessmentId).HasName("PK__Assessme__3D2BF83EFE64955A");

            entity.Property(e => e.AssessmentId).HasColumnName("AssessmentID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Course).WithMany(p => p.Assessments)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_AssessmentCourseID");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2B4382DF25");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FileImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("file_image");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__C3B4DFAA4A0C9956");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.Comment1).HasColumnName("Comment");
            entity.Property(e => e.CommentDate).HasColumnType("datetime");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.LessonId).HasColumnName("LessonID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Course).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_CommentCourseID");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Comments)
                .HasForeignKey(d => d.LessonId)
                .HasConstraintName("FK_CommentLessonID");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_CommentUserID");
        });

        modelBuilder.Entity<Content>(entity =>
        {
            entity.HasKey(e => e.ContentId).HasName("PK__Content__2907A81E17EEC6D0");

            entity.ToTable("Content");

            entity.Property(e => e.VideoUrl).HasColumnName("VideoURL");

            entity.HasOne(d => d.Coursecontent).WithMany(p => p.Contents)
                .HasForeignKey(d => d.CoursecontentId)
                .HasConstraintName("FK__Content__Coursec__619B8048");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D71875310B1B0");

            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.FileImage)
                .HasMaxLength(255)
                .HasColumnName("file_image");
            entity.Property(e => e.InstructorId).HasColumnName("InstructorID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.VideoUrl).HasColumnName("VideoURL");

            entity.HasOne(d => d.Category).WithMany(p => p.Courses)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_CategoryID");

            entity.HasOne(d => d.Instructor).WithMany(p => p.Courses)
                .HasForeignKey(d => d.InstructorId)
                .HasConstraintName("FK_InstructorID");
        });

        modelBuilder.Entity<Coursecontent>(entity =>
        {
            entity.HasKey(e => e.CoursecontentId).HasName("PK__Courseco__D1026F2ECC675EE5");

            entity.ToTable("Coursecontent");

            entity.Property(e => e.LessonId).HasColumnName("LessonID");
            entity.Property(e => e.LessoncontentId).HasColumnName("LessoncontentID");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Coursecontents)
                .HasForeignKey(d => d.LessonId)
                .HasConstraintName("FK__Coursecon__Lesso__5EBF139D");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__1ABEEF6FED69053F");

            entity.Property(e => e.DocumentId).HasColumnName("DocumentID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.FileUrl)
                .HasMaxLength(255)
                .HasColumnName("FileURL");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Course).WithMany(p => p.Documents)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_DocumentCourseID");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__7F6877FB2E584619");

            entity.Property(e => e.EnrollmentId).HasColumnName("EnrollmentID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.PaymentStatus).HasMaxLength(20);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Course).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_EnrollmentCourseID");

            entity.HasOne(d => d.User).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_EnrollmentUserID");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__Lessons__B084ACB0ADDAF9D5");

            entity.Property(e => e.LessonId).HasColumnName("LessonID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.VideoUrl).HasColumnName("VideoURL");

            entity.HasOne(d => d.Course).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_LessonCourseID");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A586072E2AD");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Course).WithMany(p => p.Payments)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_PaymentCourseID");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_PaymentUserID");
        });

        modelBuilder.Entity<Progress>(entity =>
        {
            entity.HasKey(e => e.ProgressId).HasName("PK__Progress__BAE29C85D92103DA");

            entity.ToTable("Progress");

            entity.Property(e => e.ProgressId).HasColumnName("ProgressID");
            entity.Property(e => e.LessonId).HasColumnName("LessonID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Progresses)
                .HasForeignKey(d => d.LessonId)
                .HasConstraintName("FK_ProgressLessonID");

            entity.HasOne(d => d.User).WithMany(p => p.Progresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_ProgressUserID");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__Ratings__FCCDF85C4EF402E3");

            entity.Property(e => e.RatingId).HasColumnName("RatingID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Rating1).HasColumnName("Rating");
            entity.Property(e => e.RatingDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Course).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_RatingCourseID");

            entity.HasOne(d => d.User).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_RatingUserID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

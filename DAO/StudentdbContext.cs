using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.DAO;

public partial class StudentdbContext : DbContext
{
    public StudentdbContext()
    {
    }

    public StudentdbContext(DbContextOptions<StudentdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AttendanceTb> AttendanceTbs { get; set; }

    public virtual DbSet<ClassTb> ClassTbs { get; set; }

    public virtual DbSet<CourseTb> CourseTbs { get; set; }

    public virtual DbSet<EnrollmentTb> EnrollmentTbs { get; set; }

    public virtual DbSet<ExamResultTb> ExamResultTbs { get; set; }

    public virtual DbSet<ExamTb> ExamTbs { get; set; }

    public virtual DbSet<StudentTb> StudentTbs { get; set; }

    public virtual DbSet<TeacherTb> TeacherTbs { get; set; }

    public virtual DbSet<UserTb> UserTbs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=desktop-fevs92l\\sqlexpress; Database=SMSystem; User Id=sms; Password=sms@dmin123;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AttendanceTb>(entity =>
        {
            entity.HasKey(e => e.AttendancePkid);

            entity.ToTable("Attendance_TB");

            entity.Property(e => e.Status).HasMaxLength(20);
        });

        modelBuilder.Entity<ClassTb>(entity =>
        {
            entity.HasKey(e => e.Classpkid);

            entity.ToTable("Class_TB");

            entity.Property(e => e.ClassName)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<CourseTb>(entity =>
        {
            entity.HasKey(e => e.CoursePkid);

            entity.ToTable("Course_TB");

            entity.Property(e => e.CourseCode).HasMaxLength(50);
            entity.Property(e => e.CourseName)
                .HasMaxLength(150)
                .IsFixedLength();
        });

        modelBuilder.Entity<EnrollmentTb>(entity =>
        {
            entity.HasKey(e => e.EnrollmentPkid);

            entity.ToTable("Enrollment_TB");
        });

        modelBuilder.Entity<ExamResultTb>(entity =>
        {
            entity.HasKey(e => e.ResultPkid);

            entity.ToTable("ExamResult_TB");

            entity.Property(e => e.Grade).HasMaxLength(5);
        });

        modelBuilder.Entity<ExamTb>(entity =>
        {
            entity.HasKey(e => e.ExamPkid);

            entity.ToTable("Exam_TB");

            entity.Property(e => e.ExamTitle).HasMaxLength(150);
        });

        modelBuilder.Entity<StudentTb>(entity =>
        {
            entity.HasKey(e => e.StudentPkid);

            entity.ToTable("Student_TB");

            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.ImagePath)
                .HasMaxLength(200)
                .IsFixedLength();
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.StudentId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("StudentID");
        });

        modelBuilder.Entity<TeacherTb>(entity =>
        {
            entity.HasKey(e => e.TeacherPkid);

            entity.ToTable("Teacher_TB");

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.Specialization).HasMaxLength(100);
        });

        modelBuilder.Entity<UserTb>(entity =>
        {
            entity.HasKey(e => e.UserPkid);

            entity.ToTable("User_TB");

            entity.Property(e => e.UserType).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SEP490_BackEnd.Models;

public partial class ClassNtutorContext : DbContext
{
    public ClassNtutorContext()
    {
    }

    public ClassNtutorContext(DbContextOptions<ClassNtutorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ClassMember> ClassMembers { get; set; }

    public virtual DbSet<Evaluation> Evaluations { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<SubjectTutor> SubjectTutors { get; set; }

    public virtual DbSet<Tutor> Tutors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("connection"));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Account__EC7D7D4DBBE98003");

            entity.ToTable("Account");

            entity.Property(e => e.PersonId)
                .ValueGeneratedNever()
                .HasColumnName("personId");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.Status)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_account_role");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Class__3213E83F3BA1DCEB");

            entity.ToTable("Class");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassDesc)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("classDesc");
            entity.Property(e => e.ClassLevel).HasColumnName("classLevel");
            entity.Property(e => e.ClassName)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("className");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("endDate");
            entity.Property(e => e.MaxCapacity).HasColumnName("maxCapacity");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("startDate");
            entity.Property(e => e.Status)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.SubjectId).HasColumnName("subjectId");
            entity.Property(e => e.TutorId).HasColumnName("tutorId");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Subject).WithMany(p => p.Classes)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_class_subject");

            entity.HasOne(d => d.Tutor).WithMany(p => p.Classes)
                .HasForeignKey(d => d.TutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_class_tutor");
        });

        modelBuilder.Entity<ClassMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Class_me__3213E83FA1659C61");

            entity.ToTable("Class_member");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ClassId).HasColumnName("classId");
            entity.Property(e => e.EvaluationId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("evaluationId");
            entity.Property(e => e.Status)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.StudentId).HasColumnName("studentId");

            entity.HasOne(d => d.Class).WithMany(p => p.ClassMembers)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_classmember_class");

            entity.HasOne(d => d.Student).WithMany(p => p.ClassMembers)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_classmember_student");
        });

        modelBuilder.Entity<Evaluation>(entity =>
        {
            entity.HasKey(e => e.EvaluationId).HasName("PK__Evaluati__053C90BBD3BD844D");

            entity.ToTable("Evaluation");

            entity.Property(e => e.EvaluationId)
                .ValueGeneratedNever()
                .HasColumnName("evaluationId");
            entity.Property(e => e.ClassId).HasColumnName("classId");
            entity.Property(e => e.Comment)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.Status)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.StudentId).HasColumnName("studentId");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Class).WithMany(p => p.Evaluations)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_evaluation_class");

            entity.HasOne(d => d.Student).WithMany(p => p.Evaluations)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_evaluation_student");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__A0D9EFC645062E5B");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("paymentId");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.PayDate)
                .HasColumnType("datetime")
                .HasColumnName("payDate");
            entity.Property(e => e.PayerId).HasColumnName("payerId");
            entity.Property(e => e.PaymentAmount).HasColumnName("paymentAmount");
            entity.Property(e => e.PaymentDesc)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("paymentDesc");
            entity.Property(e => e.PaymentType)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("paymentType");
            entity.Property(e => e.RequestDate)
                .HasColumnType("datetime")
                .HasColumnName("requestDate");
            entity.Property(e => e.RequestId).HasColumnName("requestId");
            entity.Property(e => e.Status)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Payer).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_payment_tutor");

            entity.HasOne(d => d.Request).WithMany(p => p.Payments)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_payment_staff");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Person__EC7D7D4D8A4A8679");

            entity.ToTable("Person");

            entity.Property(e => e.PersonId)
                .ValueGeneratedNever()
                .HasColumnName("personId");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.FullName)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("fullName");
            entity.Property(e => e.Gender)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.UserAvatar)
                .HasColumnType("ntext")
                .HasColumnName("userAvatar");

            entity.HasOne(d => d.PersonNavigation).WithOne(p => p.Person)
                .HasForeignKey<Person>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_info_accountid");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3213E83FB4FFA903");

            entity.ToTable("Role");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.RoleDesc)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("roleDesc");
            entity.Property(e => e.RoleName)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("roleName");
            entity.Property(e => e.Status)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Schedule__3213E83FE77114CA");

            entity.ToTable("Schedule");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ClassId).HasColumnName("classId");
            entity.Property(e => e.DaysOfWeek)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("daysOfWeek");
            entity.Property(e => e.SessionEnd).HasColumnName("sessionEnd");
            entity.Property(e => e.SessionStart).HasColumnName("sessionStart");

            entity.HasOne(d => d.Class).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_schedule_class");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Staff__EC7D7D4D02442710");

            entity.Property(e => e.PersonId)
                .ValueGeneratedNever()
                .HasColumnName("personId");
            entity.Property(e => e.StaffType)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("staffType");

            entity.HasOne(d => d.Person).WithOne(p => p.Staff)
                .HasForeignKey<Staff>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_staff_account");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__4D11D63CC64A9C8D");

            entity.ToTable("Student");

            entity.Property(e => e.StudentId).HasColumnName("studentId");
            entity.Property(e => e.PersonId).HasColumnName("personId");
            entity.Property(e => e.Status)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.StudentLevel).HasColumnName("studentLevel");
            entity.Property(e => e.StudentName)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("studentName");

            entity.HasOne(d => d.Person).WithMany(p => p.Students)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_student_person");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subject__ACF9A76021EFD387");

            entity.ToTable("Subject");

            entity.Property(e => e.SubjectId).HasColumnName("subjectId");
            entity.Property(e => e.Status)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.SubjectDesc)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("subjectDesc");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("subjectName");
        });

        modelBuilder.Entity<SubjectTutor>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SubjectTutor");

            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.Status)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.SubjectId).HasColumnName("subjectId");
            entity.Property(e => e.TutorId).HasColumnName("tutorId");

            entity.HasOne(d => d.Subject).WithMany()
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_subjecttutor_subject");

            entity.HasOne(d => d.Tutor).WithMany()
                .HasForeignKey(d => d.TutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_subjecttutor_tutor");
        });

        modelBuilder.Entity<Tutor>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Tutor__EC7D7D4DD9128612");

            entity.ToTable("Tutor");

            entity.Property(e => e.PersonId)
                .ValueGeneratedNever()
                .HasColumnName("personId");
            entity.Property(e => e.About)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("about");
            entity.Property(e => e.BackCmnd)
                .HasColumnType("ntext")
                .HasColumnName("backCMND");
            entity.Property(e => e.Cmnd)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("CMND");
            entity.Property(e => e.Cv)
                .HasColumnType("ntext")
                .HasColumnName("CV");
            entity.Property(e => e.EducationLevel)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("educationLevel");
            entity.Property(e => e.FrontCmnd)
                .HasColumnType("ntext")
                .HasColumnName("frontCMND");
            entity.Property(e => e.GraduationYear)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("graduationYear");
            entity.Property(e => e.School)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("school");

            entity.HasOne(d => d.Person).WithOne(p => p.Tutor)
                .HasForeignKey<Tutor>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tutor_person");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

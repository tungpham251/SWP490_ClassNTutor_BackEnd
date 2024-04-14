using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Models
{
    public partial class ClassNTutorContext : DbContext
    {
        public ClassNTutorContext()
        {
        }

        public ClassNTutorContext(DbContextOptions<ClassNTutorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<ClassMember> ClassMembers { get; set; } = null!;
        public virtual DbSet<Evaluation> Evaluations { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<SubjectTutor> SubjectTutors { get; set; } = null!;
        public virtual DbSet<Tutor> Tutors { get; set; } = null!;
        public virtual DbSet<Staff> Staffs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.PersonId)
                    .HasName("PK__Account__EC7D7D4D939FB007");

                entity.ToTable("Account");

                entity.Property(e => e.PersonId)
                    .ValueGeneratedNever()
                    .HasColumnName("personId");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Email)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.Status)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("date")
                    .HasColumnName("updatedAt");

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.Account)
                    .HasForeignKey<Account>(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_account_person");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_account_role");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.Property(e => e.ClassId)
                    .ValueGeneratedNever()
                    .HasColumnName("classId");

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
                    .HasColumnType("date")
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
                    .HasColumnType("date")
                    .HasColumnName("updatedAt");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_class_subject");

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.TutorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_class_tutor");
            });

            modelBuilder.Entity<ClassMember>(entity =>
            {
                entity.ToTable("Class_member");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ClassId).HasColumnName("classId");

                entity.Property(e => e.Status)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.StudentId).HasColumnName("studentId");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassMembers)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_classmember_class");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.ClassMembers)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_classmember_student");
            });

            modelBuilder.Entity<Evaluation>(entity =>
            {
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
                    .HasColumnType("date")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.Status)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.StudentId).HasColumnName("studentId");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("date")
                    .HasColumnName("updatedAt");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Evaluations)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_evaluation_class");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Evaluations)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_evaluation_student");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.PaymentId)
                    .ValueGeneratedNever()
                    .HasColumnName("paymentId");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("createdAt");

                entity.Property(e => e.PayDate)
                    .HasColumnType("date")
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
                    .HasColumnType("date")
                    .HasColumnName("requestDate");

                entity.Property(e => e.RequestId).HasColumnName("requestId");

                entity.Property(e => e.Status)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("date")
                    .HasColumnName("updatedAt");

                entity.HasOne(d => d.Payer)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.PayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_payment_tutor");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_payment_staff");
            });

            modelBuilder.Entity<Person>(entity =>
            {
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
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("Request");

                entity.Property(e => e.RequestId)
                    .ValueGeneratedNever()
                    .HasColumnName("requestId");

                entity.Property(e => e.ClassId).HasColumnName("classId");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.ParentId).HasColumnName("parentId");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.RequestType)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("requestType");

                entity.Property(e => e.Status)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.StudentId).HasColumnName("studentId");

                entity.Property(e => e.SubjectId).HasColumnName("subjectId");

                entity.Property(e => e.TutorId).HasColumnName("tutorId");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("date")
                    .HasColumnName("updatedAt");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("fk_request_class");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.RequestParents)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_request_account_parent");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_request_student");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_request_subject");

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.RequestTutors)
                    .HasForeignKey(d => d.TutorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_request_account_tutor");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("roleId");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
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
                    .HasColumnType("date")
                    .HasColumnName("updatedAt");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ClassId).HasColumnName("classId");

                entity.Property(e => e.DayOfWeek)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("dayOfWeek");

                entity.Property(e => e.SessionEnd).HasColumnName("sessionEnd");

                entity.Property(e => e.SessionStart).HasColumnName("sessionStart");

                entity.Property(e => e.Status)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_schedule_class");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.StudentId)
                    .ValueGeneratedNever()
                    .HasColumnName("studentId");

                entity.Property(e => e.ParentId).HasColumnName("parentId");

                entity.Property(e => e.Status)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.StudentLevel).HasColumnName("studentLevel");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.StudentParents)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_student_person_parent");

                entity.HasOne(d => d.StudentNavigation)
                    .WithOne(p => p.StudentStudentNavigation)
                    .HasForeignKey<Student>(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_student_person_student");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.Property(e => e.SubjectId)
                    .ValueGeneratedNever()
                    .HasColumnName("subjectId");

                entity.Property(e => e.Status)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.SubjectName)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("subjectName");
            });

            modelBuilder.Entity<SubjectTutor>(entity =>
            {
                entity.HasKey(e => new { e.SubjectId, e.TutorId, e.Level });

                entity.ToTable("SubjectTutor");

                entity.Property(e => e.SubjectId).HasColumnName("subjectId");

                entity.Property(e => e.TutorId).HasColumnName("tutorId");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Status)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SubjectTutors)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_subjecttutor_subject");

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.SubjectTutors)
                    .HasForeignKey(d => d.TutorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_subjecttutor_tutor");
            });

            modelBuilder.Entity<Tutor>(entity =>
            {
                entity.HasKey(e => e.PersonId)
                    .HasName("PK__Tutor__EC7D7D4D9C3702D5");

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

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.Tutor)
                    .HasForeignKey<Tutor>(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tutor_person");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.HasKey(e => e.PersonId)
                    .HasName("PK__Staff__EC7D7D4D01A87B00");

                entity.ToTable("Staff");

                entity.Property(e => e.PersonId)
                    .ValueGeneratedNever()
                    .HasColumnName("personId");

                entity.Property(e => e.StaffType)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("staffType");

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.Staff)
                    .HasForeignKey<Staff>(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_staff_account");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

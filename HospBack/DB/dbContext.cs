using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HospBack.DB
{
    public partial class dbContext : IdentityDbContext<User>
    {
        private readonly string _connectionString;

        public dbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public dbContext(DbContextOptions<dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Analysis> Analyses { get; set; }
		public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<DoctorType> DoctorTypes { get; set; }
        public virtual DbSet<Hospital> Hospitals { get; set; }
        public virtual DbSet<HospitalDoctorType> HospitalDoctorTypes { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Referral> Referrals { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8");

            modelBuilder.Entity<Analysis>(entity =>
            {
                entity.ToTable("analyses");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AnalyseDate)
                    .HasColumnType("date")
                    .HasColumnName("analyse_date");

                entity.Property(e => e.AnalyseType)
                    .IsRequired()
                    .HasColumnName("analyse_type");

                entity.Property(e => e.ResultDescription)
                    .IsRequired()
                    .HasColumnName("result_description");

                entity.Property(e => e.PatientId)
                    .IsRequired()
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Analyses)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("analyse_user_id");
            });

			modelBuilder.Entity<AspNetRole>(entity =>
			{
				entity.Property(e => e.Name).HasMaxLength(256);

				entity.Property(e => e.NormalizedName).HasMaxLength(256);
			});

			modelBuilder.Entity<AspNetRoleClaim>(entity =>
			{
				entity.Property(e => e.RoleId).IsRequired();

				entity.HasOne(d => d.Role)
					.WithMany(p => p.AspNetRoleClaims)
					.HasForeignKey(d => d.RoleId);
			});

			modelBuilder.Entity<AspNetUser>(entity =>
			{
				entity.ToTable("AspNetUser");

				entity.Property(e => e.Discriminator).IsRequired();

				entity.Property(e => e.Email).HasMaxLength(256);

				entity.Property(e => e.LockoutEnd).HasColumnType("timestamp with time zone");

				entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

				entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

				entity.Property(e => e.UserName).HasMaxLength(256);
			});

			modelBuilder.Entity<AspNetUserClaim>(entity =>
			{
				entity.ToTable("AspNetUserClaim");

				entity.Property(e => e.UserId).IsRequired();

				entity.HasOne(d => d.User)
					.WithMany(p => p.AspNetUserClaims)
					.HasForeignKey(d => d.UserId);
			});

			modelBuilder.Entity<AspNetUserLogin>(entity =>
			{
				entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

				entity.ToTable("AspNetUserLogin");

				entity.Property(e => e.UserId).IsRequired();

				entity.HasOne(d => d.User)
					.WithMany(p => p.AspNetUserLogins)
					.HasForeignKey(d => d.UserId);
			});

			modelBuilder.Entity<AspNetUserRole>(entity =>
			{
				entity.HasKey(e => new { e.UserId, e.RoleId });

				entity.ToTable("AspNetUserRole");

				entity.HasOne(d => d.Role)
					.WithMany(p => p.AspNetUserRoles)
					.HasForeignKey(d => d.RoleId);

				entity.HasOne(d => d.User)
					.WithMany(p => p.AspNetUserRoles)
					.HasForeignKey(d => d.UserId);
			});

			modelBuilder.Entity<AspNetUserToken>(entity =>
			{
				entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

				entity.HasOne(d => d.User)
					.WithMany(p => p.AspNetUserTokens)
					.HasForeignKey(d => d.UserId);
			});

            /////////////////////////////////

			modelBuilder.Entity<Certificate>(entity =>
            {
                entity.ToTable("certificates");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.VisitId).HasColumnName("visit_id");

                entity.HasOne(d => d.Visit)
                    .WithMany(p => p.Certificates)
                    .HasForeignKey(d => d.VisitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("certificate_visit_id");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("doctors");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DoctorType).HasColumnName("doctor_type");

                entity.Property(e => e.HospitalId).HasColumnName("hospital_id");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id");

                entity.HasOne(d => d.DoctorTypeNavigation)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.DoctorType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("doctor_doctor_type");

                entity.HasOne(d => d.Hospital)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.HospitalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("doctor_hospital_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("doctor_user_id");
            });

            modelBuilder.Entity<DoctorType>(entity =>
            {
                entity.ToTable("doctor_types");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Hospital>(entity =>
            {
                entity.ToTable("hospitals");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnName("phone_number");
            });

            modelBuilder.Entity<HospitalDoctorType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("hospital_doctor_type");

                entity.Property(e => e.DoctorType).HasColumnName("doctor_type");

                entity.Property(e => e.HospitalId).HasColumnName("hospital_id");

                entity.HasOne(d => d.DoctorTypeNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.DoctorType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("doctor_doctor_type");

                entity.HasOne(d => d.Hospital)
                    .WithMany()
                    .HasForeignKey(d => d.HospitalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("doctor_hospital_id");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("patients");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnName("phone_number");

                entity.Property(e => e.Email)
                    .HasColumnName("email");
            });

            modelBuilder.Entity<Referral>(entity =>
            {
                entity.ToTable("referrals");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.AnalyseType)
                    .IsRequired()
                    .HasColumnName("analyse_type");

                entity.Property(e => e.CertificateId).HasColumnName("certificate_id");

                entity.Property(e => e.DoctorName)
                    .IsRequired()
                    .HasColumnName("doctor_name");

                entity.Property(e => e.DoctorSurname)
                    .IsRequired()
                    .HasColumnName("doctor_surname");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name");

                entity.Property(e => e.UserSurname)
                    .IsRequired()
                    .HasColumnName("user_surname");

                entity.HasOne(d => d.Certificate)
                    .WithMany(p => p.Referrals)
                    .HasForeignKey(d => d.CertificateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("referral_certificate_id");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("schedules");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DayOfWeek).HasColumnName("day_of_week");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.EndTime).HasColumnName("end_time");

                entity.Property(e => e.StartTime).HasColumnName("start_time");

                entity.Property(e => e.VisitTime).HasColumnName("visit_time");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("schedule_doctor_id");
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.ToTable("visits");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status");

                entity.Property(e => e.PatientId)
                    .IsRequired()
                    .HasColumnName("user_id");

                entity.Property(e => e.VisitDate).HasColumnName("visit_date");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("visit_doctor_id");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("visit_user_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

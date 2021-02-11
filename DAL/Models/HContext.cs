using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DAL.Models
{
    public partial class HContext : DbContext
    {
        public HContext()
        {
        }

        public HContext(DbContextOptions<HContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<app_nav> app_nav { get; set; }
        public virtual DbSet<app_setting> app_setting { get; set; }
        public virtual DbSet<mp_applicant> mp_applicant { get; set; }
        public virtual DbSet<mp_applicant_category> mp_applicant_category { get; set; }
        public virtual DbSet<mp_applicant_checklist> mp_applicant_checklist { get; set; }
        public virtual DbSet<mp_applicant_document> mp_applicant_document { get; set; }
        public virtual DbSet<mp_applicant_education> mp_applicant_education { get; set; }
        public virtual DbSet<mp_applicant_expertise> mp_applicant_expertise { get; set; }
        public virtual DbSet<mp_applicant_language> mp_applicant_language { get; set; }
        public virtual DbSet<mp_applicant_other_activities> mp_applicant_other_activities { get; set; }
        public virtual DbSet<mp_applicant_population> mp_applicant_population { get; set; }
        public virtual DbSet<mp_applicant_practice> mp_applicant_practice { get; set; }
        public virtual DbSet<mp_applicant_specialty> mp_applicant_specialty { get; set; }
        public virtual DbSet<mp_appointment> mp_appointment { get; set; }
        public virtual DbSet<mp_appointment_form> mp_appointment_form { get; set; }
        public virtual DbSet<mp_appointment_log> mp_appointment_log { get; set; }
        public virtual DbSet<mp_appointment_refund> mp_appointment_refund { get; set; }
        public virtual DbSet<mp_child_screening> mp_child_screening { get; set; }
        public virtual DbSet<mp_children> mp_children { get; set; }
        public virtual DbSet<mp_clinic> mp_clinic { get; set; }
        public virtual DbSet<mp_clinic_clinician> mp_clinic_clinician { get; set; }
        public virtual DbSet<mp_clinician> mp_clinician { get; set; }
        public virtual DbSet<mp_clinician_availability> mp_clinician_availability { get; set; }
        public virtual DbSet<mp_clinician_category> mp_clinician_category { get; set; }
        public virtual DbSet<mp_clinician_document> mp_clinician_document { get; set; }
        public virtual DbSet<mp_clinician_education> mp_clinician_education { get; set; }
        public virtual DbSet<mp_clinician_expertise> mp_clinician_expertise { get; set; }
        public virtual DbSet<mp_clinician_language> mp_clinician_language { get; set; }
        public virtual DbSet<mp_clinician_other_activities> mp_clinician_other_activities { get; set; }
        public virtual DbSet<mp_clinician_population> mp_clinician_population { get; set; }
        public virtual DbSet<mp_clinician_practice> mp_clinician_practice { get; set; }
        public virtual DbSet<mp_clinician_rating> mp_clinician_rating { get; set; }
        public virtual DbSet<mp_clinician_specialty> mp_clinician_specialty { get; set; }
        public virtual DbSet<mp_consent> mp_consent { get; set; }
        public virtual DbSet<mp_conversation> mp_conversation { get; set; }
        public virtual DbSet<mp_countries> mp_countries { get; set; }
        public virtual DbSet<mp_couple_intake> mp_couple_intake { get; set; }
        public virtual DbSet<mp_couple_screening> mp_couple_screening { get; set; }
        public virtual DbSet<mp_credit> mp_credit { get; set; }
        public virtual DbSet<mp_education_history> mp_education_history { get; set; }
        public virtual DbSet<mp_emergency_contact> mp_emergency_contact { get; set; }
        public virtual DbSet<mp_employment> mp_employment { get; set; }
        public virtual DbSet<mp_enrollment> mp_enrollment { get; set; }
        public virtual DbSet<mp_family_history> mp_family_history { get; set; }
        public virtual DbSet<mp_family_intake> mp_family_intake { get; set; }
        public virtual DbSet<mp_form> mp_form { get; set; }
        public virtual DbSet<mp_link_applicant_clinician> mp_link_applicant_clinician { get; set; }
        public virtual DbSet<mp_lk_appointment_activity> mp_lk_appointment_activity { get; set; }
        public virtual DbSet<mp_lk_appointment_activity_sub> mp_lk_appointment_activity_sub { get; set; }
        public virtual DbSet<mp_lk_appointment_service> mp_lk_appointment_service { get; set; }
        public virtual DbSet<mp_lk_appointment_type> mp_lk_appointment_type { get; set; }
        public virtual DbSet<mp_lk_drug> mp_lk_drug { get; set; }
        public virtual DbSet<mp_lk_expertise> mp_lk_expertise { get; set; }
        public virtual DbSet<mp_lk_mental_status> mp_lk_mental_status { get; set; }
        public virtual DbSet<mp_lk_notification_type> mp_lk_notification_type { get; set; }
        public virtual DbSet<mp_lk_psychiatric_diagnosis> mp_lk_psychiatric_diagnosis { get; set; }
        public virtual DbSet<mp_lk_specialty> mp_lk_specialty { get; set; }
        public virtual DbSet<mp_lnk_appointment_service_activity_sub> mp_lnk_appointment_service_activity_sub { get; set; }
        public virtual DbSet<mp_lookup> mp_lookup { get; set; }
        public virtual DbSet<mp_major_illness_surgery> mp_major_illness_surgery { get; set; }
        public virtual DbSet<mp_medical_history> mp_medical_history { get; set; }
        public virtual DbSet<mp_medication> mp_medication { get; set; }
        public virtual DbSet<mp_medication_information> mp_medication_information { get; set; }
        public virtual DbSet<mp_mental_health_plan> mp_mental_health_plan { get; set; }
        public virtual DbSet<mp_mental_health_plan_objective> mp_mental_health_plan_objective { get; set; }
        public virtual DbSet<mp_mental_health_plan_review_period> mp_mental_health_plan_review_period { get; set; }
        public virtual DbSet<mp_notification> mp_notification { get; set; }
        public virtual DbSet<mp_ped_evaluation_history> mp_ped_evaluation_history { get; set; }
        public virtual DbSet<mp_ped_symptomps> mp_ped_symptomps { get; set; }
        public virtual DbSet<mp_pediatric_evaluation> mp_pediatric_evaluation { get; set; }
        public virtual DbSet<mp_pharmacy> mp_pharmacy { get; set; }
        public virtual DbSet<mp_phc_health_history> mp_phc_health_history { get; set; }
        public virtual DbSet<mp_phc_medical_history> mp_phc_medical_history { get; set; }
        public virtual DbSet<mp_phc_mental_status> mp_phc_mental_status { get; set; }
        public virtual DbSet<mp_phc_social_history> mp_phc_social_history { get; set; }
        public virtual DbSet<mp_phc_system_review> mp_phc_system_review { get; set; }
        public virtual DbSet<mp_population_group> mp_population_group { get; set; }
        public virtual DbSet<mp_prescription> mp_prescription { get; set; }
        public virtual DbSet<mp_prescription_drug> mp_prescription_drug { get; set; }
        public virtual DbSet<mp_profile> mp_profile { get; set; }
        public virtual DbSet<mp_profile_bank> mp_profile_bank { get; set; }
        public virtual DbSet<mp_profile_hmo> mp_profile_hmo { get; set; }
        public virtual DbSet<mp_profile_match> mp_profile_match { get; set; }
        public virtual DbSet<mp_profile_mental_status> mp_profile_mental_status { get; set; }
        public virtual DbSet<mp_progress_note> mp_progress_note { get; set; }
        public virtual DbSet<mp_psychiatric_opd_evaluation> mp_psychiatric_opd_evaluation { get; set; }
        public virtual DbSet<mp_psychiatric_opd_evaluation_diagnosis> mp_psychiatric_opd_evaluation_diagnosis { get; set; }
        public virtual DbSet<mp_psychiatric_progress_note> mp_psychiatric_progress_note { get; set; }
        public virtual DbSet<mp_psychosocial> mp_psychosocial { get; set; }
        public virtual DbSet<mp_referral> mp_referral { get; set; }
        public virtual DbSet<mp_service_costing> mp_service_costing { get; set; }
        public virtual DbSet<mp_social_relationship> mp_social_relationship { get; set; }
        public virtual DbSet<mp_states> mp_states { get; set; }
        public virtual DbSet<mp_substance_use> mp_substance_use { get; set; }
        public virtual DbSet<mp_summary_treatment_history> mp_summary_treatment_history { get; set; }
        public virtual DbSet<mp_surgical_history> mp_surgical_history { get; set; }
        public virtual DbSet<mp_symptoms> mp_symptoms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                   .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                   .AddJsonFile("appsettings.json")
                                   .Build();
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LockoutEnd).HasColumnType("timestamp with time zone");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<app_nav>(entity =>
            {
                entity.Property(e => e.action).IsRequired();

                entity.Property(e => e.controller).IsRequired();

                entity.Property(e => e.name).IsRequired();
            });

            modelBuilder.Entity<app_setting>(entity =>
            {
                entity.Property(e => e.key).IsRequired();

                entity.Property(e => e.value).IsRequired();
            });

            modelBuilder.Entity<mp_applicant>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.dob).HasColumnType("date");

                entity.Property(e => e.first_name).IsRequired();

                entity.Property(e => e.last_name).IsRequired();

                entity.Property(e => e.phone).IsRequired();

                entity.Property(e => e.preferred_name).IsRequired();

                entity.HasOne(d => d.countryNavigation)
                    .WithMany(p => p.mp_applicant)
                    .HasForeignKey(d => d.country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_applicant_mp_country");

                entity.HasOne(d => d.marital_statusNavigation)
                    .WithMany(p => p.mp_applicantmarital_statusNavigation)
                    .HasForeignKey(d => d.marital_status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_applicant_marital_status");

                entity.HasOne(d => d.stateNavigation)
                    .WithMany(p => p.mp_applicant)
                    .HasForeignKey(d => d.state)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_applicant_mp_state");

                entity.HasOne(d => d.statusNavigation)
                    .WithMany(p => p.mp_applicantstatusNavigation)
                    .HasForeignKey(d => d.status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_applicant_application_status");
            });

            modelBuilder.Entity<mp_applicant_category>(entity =>
            {
                entity.HasOne(d => d.applicant_)
                    .WithMany(p => p.mp_applicant_category)
                    .HasForeignKey(d => d.applicant_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_applicant_category_applicant_id_fkey");

                entity.HasOne(d => d.appointment_categoryNavigation)
                    .WithMany(p => p.mp_applicant_category)
                    .HasForeignKey(d => d.appointment_category)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_applicant_category_appointment_category_fkey");

                entity.HasOne(d => d.appointment_category_subNavigation)
                    .WithMany(p => p.mp_applicant_category)
                    .HasForeignKey(d => d.appointment_category_sub)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_applicant_category_appointment_category_sub_fkey");

                entity.HasOne(d => d.appointment_typeNavigation)
                    .WithMany(p => p.mp_applicant_category)
                    .HasForeignKey(d => d.appointment_type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_applicant_category_appointment_type_fkey");
            });

            modelBuilder.Entity<mp_applicant_checklist>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();

                entity.HasOne(d => d.clinician_)
                    .WithMany(p => p.mp_applicant_checklist)
                    .HasForeignKey(d => d.clinician_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_applicant_checklist_clinician_id_fkey");
            });

            modelBuilder.Entity<mp_applicant_document>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('applicant_document_seq'::regclass)");

                entity.Property(e => e.path).IsRequired();

                entity.HasOne(d => d.applicant_)
                    .WithMany(p => p.mp_applicant_document)
                    .HasForeignKey(d => d.applicant_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_applicant_applicant_document");
            });

            modelBuilder.Entity<mp_applicant_education>(entity =>
            {
                entity.Property(e => e.address).IsRequired();

                entity.Property(e => e.created_by).IsRequired();

                entity.Property(e => e.school).IsRequired();

                entity.HasOne(d => d.applicant_)
                    .WithMany(p => p.mp_applicant_education)
                    .HasForeignKey(d => d.applicant_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_applicant_education_applicant_id_fkey");
            });

            modelBuilder.Entity<mp_applicant_expertise>(entity =>
            {
                entity.HasOne(d => d.applicant_)
                    .WithMany(p => p.mp_applicant_expertise)
                    .HasForeignKey(d => d.applicant_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_mp_applicant_expertise_mp_profile");

                entity.HasOne(d => d.expertise_)
                    .WithMany(p => p.mp_applicant_expertise)
                    .HasForeignKey(d => d.expertise_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_mp_applicant_expertise_mp_expertise");
            });

            modelBuilder.Entity<mp_applicant_language>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('mp_applican_language_id_seq'::regclass)");

                entity.HasOne(d => d.applicant_)
                    .WithMany(p => p.mp_applicant_language)
                    .HasForeignKey(d => d.applicant_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_applican_language_applicant_id_fkey");

                entity.HasOne(d => d.languageNavigation)
                    .WithMany(p => p.mp_applicant_language)
                    .HasForeignKey(d => d.language)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_applican_language_language_fkey");
            });

            modelBuilder.Entity<mp_applicant_other_activities>(entity =>
            {
                entity.Property(e => e.activity).IsRequired();

                entity.Property(e => e.activity_type).IsRequired();

                entity.HasOne(d => d.applicant_)
                    .WithMany(p => p.mp_applicant_other_activities)
                    .HasForeignKey(d => d.applicant_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_applicant_other_activities_applicant_id_fkey");
            });

            modelBuilder.Entity<mp_applicant_population>(entity =>
            {
                entity.HasOne(d => d.applicant_)
                    .WithMany(p => p.mp_applicant_population)
                    .HasForeignKey(d => d.applicant_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_applicant_population_applicant_id_fkey");

                entity.HasOne(d => d.population_)
                    .WithMany(p => p.mp_applicant_population)
                    .HasForeignKey(d => d.population_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_applicant_population_population_id_fkey");
            });

            modelBuilder.Entity<mp_applicant_practice>(entity =>
            {
                entity.Property(e => e.city).IsRequired();

                entity.Property(e => e.duration).IsRequired();

                entity.Property(e => e.hospital).IsRequired();

                entity.Property(e => e.role).IsRequired();

                entity.HasOne(d => d.applicant_)
                    .WithMany(p => p.mp_applicant_practice)
                    .HasForeignKey(d => d.applicant_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_applicant_practice_applicant_id_fkey");
            });

            modelBuilder.Entity<mp_applicant_specialty>(entity =>
            {
                entity.HasOne(d => d.applicant_)
                    .WithMany(p => p.mp_applicant_specialty)
                    .HasForeignKey(d => d.applicant_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_applicant_specialty_applicant_id_fkey");

                entity.HasOne(d => d.specialty_)
                    .WithMany(p => p.mp_applicant_specialty)
                    .HasForeignKey(d => d.specialty_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_applicant_specialty_specialty_id_fkey");
            });

            modelBuilder.Entity<mp_appointment>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.appointment_service).HasDefaultValueSql("1");

                entity.Property(e => e.created_by).IsRequired();

                entity.HasOne(d => d.appointment_serviceNavigation)
                    .WithMany(p => p.mp_appointment)
                    .HasForeignKey(d => d.appointment_service)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_appointment_appointment_service_fkey");

                entity.HasOne(d => d.appointment_typeNavigation)
                    .WithMany(p => p.mp_appointment)
                    .HasForeignKey(d => d.appointment_type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_appointment_appointment_type_fkey");

                entity.HasOne(d => d.appointment_sub_typeNavigation)
                   .WithMany(p => p.mp_appointment)
                   .HasForeignKey(d => d.appointment_activity_sub_id)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("mp_appointment_sub_appointment_type_fkey");

                entity.HasOne(d => d.client_)
                    .WithMany(p => p.mp_appointment)
                    .HasForeignKey(d => d.client_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_appointment_client_id_fkey");

                entity.HasOne(d => d.clinician_)
                    .WithMany(p => p.mp_appointment)
                    .HasForeignKey(d => d.clinician_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_appointment_clinician_id_fkey");
            });

            modelBuilder.Entity<mp_appointment_form>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('mp_session_form_id_seq'::regclass)");

                entity.HasOne(d => d.appointment_)
                    .WithMany(p => p.mp_appointment_form)
                    .HasForeignKey(d => d.appointment_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_appointment_form_appointment_id_fkey");

                entity.HasOne(d => d.form_)
                    .WithMany(p => p.mp_appointment_form)
                    .HasForeignKey(d => d.form_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_session_form_form_id_fkey");
            });

            modelBuilder.Entity<mp_appointment_log>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('mp_appointment_session_id_seq'::regclass)");

                entity.Property(e => e.created_by).IsRequired();

                entity.Property(e => e.role).IsRequired();

                entity.HasOne(d => d.appointment_)
                    .WithMany(p => p.mp_appointment_log)
                    .HasForeignKey(d => d.appointment_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_appointment_log_appointment_id_fkey");
            });

            modelBuilder.Entity<mp_appointment_refund>(entity =>
            {
                entity.Property(e => e.amount).HasColumnType("money");

                entity.Property(e => e.created_by).IsRequired();

                entity.HasOne(d => d.appointment_)
                    .WithMany(p => p.mp_appointment_refund)
                    .HasForeignKey(d => d.appointment_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_appointment_refund_appointment_id_fkey");
            });

            modelBuilder.Entity<mp_child_screening>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('child_screening_id_seq'::regclass)");

                entity.Property(e => e.child_issue_concerns).IsRequired();

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_child_screening)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_child_screening_profile_id_fkey");
            });

            modelBuilder.Entity<mp_children>(entity =>
            {
                entity.Property(e => e.name).IsRequired();

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_children)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_profile_children");
            });

            modelBuilder.Entity<mp_clinic>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.address_c).IsRequired();

                entity.Property(e => e.email_c).IsRequired();

                entity.Property(e => e.first_name_c).IsRequired();

                entity.Property(e => e.hours_of_operation).IsRequired();

                entity.Property(e => e.last_name_c).IsRequired();

                entity.Property(e => e.name).IsRequired();

                entity.Property(e => e.phone_c).IsRequired();
            });

            modelBuilder.Entity<mp_clinic_clinician>(entity =>
            {
                entity.HasOne(d => d.clinic_)
                    .WithMany(p => p.mp_clinic_clinician)
                    .HasForeignKey(d => d.clinic_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_clinic_clinician_clinic_id_fkey");

                entity.HasOne(d => d.clinician_)
                    .WithMany(p => p.mp_clinic_clinician)
                    .HasForeignKey(d => d.clinician_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_clinic_clinician_clinician_id_fkey");
            });

            modelBuilder.Entity<mp_clinician>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.dob).HasColumnType("date");

                entity.Property(e => e.first_name).IsRequired();

                entity.Property(e => e.last_name).IsRequired();

                entity.Property(e => e.phone).IsRequired();

                entity.Property(e => e.preferred_name).IsRequired();
            });

            modelBuilder.Entity<mp_clinician_availability>(entity =>
            {
                entity.Property(e => e.day_name).IsRequired();

                entity.Property(e => e.status)
                    .IsRequired()
                    .HasDefaultValueSql("true");

                entity.HasOne(d => d.clinician_)
                    .WithMany(p => p.mp_clinician_availability)
                    .HasForeignKey(d => d.clinician_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_clinician_availability_clinician_id_fkey");
            });

            modelBuilder.Entity<mp_clinician_category>(entity =>
            {
                entity.HasOne(d => d.appointment_categoryNavigation)
                    .WithMany(p => p.mp_clinician_category)
                    .HasForeignKey(d => d.appointment_category)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_clinician_category_appointment_category_fkey");

                entity.HasOne(d => d.appointment_category_subNavigation)
                    .WithMany(p => p.mp_clinician_category)
                    .HasForeignKey(d => d.appointment_category_sub)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_clinician_category_appointment_category_sub_fkey");

                entity.HasOne(d => d.appointment_typeNavigation)
                    .WithMany(p => p.mp_clinician_category)
                    .HasForeignKey(d => d.appointment_type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_clinician_category_appointment_type_fkey");

                entity.HasOne(d => d.clinician_)
                    .WithMany(p => p.mp_clinician_category)
                    .HasForeignKey(d => d.clinician_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_clinician_category_clinician_id_fkey");
            });

            modelBuilder.Entity<mp_clinician_document>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('mp_profile_document_id_seq'::regclass)");

                entity.Property(e => e.path).IsRequired();

                entity.HasOne(d => d.clinician_)
                    .WithMany(p => p.mp_clinician_document)
                    .HasForeignKey(d => d.clinician_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_clinician_document_clinician_id_fkey");
            });

            modelBuilder.Entity<mp_clinician_education>(entity =>
            {
                entity.Property(e => e.address).IsRequired();

                entity.Property(e => e.school).IsRequired();

                entity.HasOne(d => d.clinician_)
                    .WithMany(p => p.mp_clinician_education)
                    .HasForeignKey(d => d.clinician_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_education_clinician_id_fkey");
            });

            modelBuilder.Entity<mp_clinician_expertise>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('mp_profile_expertise_id_seq'::regclass)");

                entity.HasOne(d => d.clinician_)
                    .WithMany(p => p.mp_clinician_expertise)
                    .HasForeignKey(d => d.clinician_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_clinician_expertise_clinician_id_fkey");

                entity.HasOne(d => d.expertise_)
                    .WithMany(p => p.mp_clinician_expertise)
                    .HasForeignKey(d => d.expertise_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_clinician_expertise_expertise_id_fkey");
            });

            modelBuilder.Entity<mp_clinician_language>(entity =>
            {
                entity.HasOne(d => d.clinician_)
                    .WithMany(p => p.mp_clinician_language)
                    .HasForeignKey(d => d.clinician_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_clinician_language_clinician_id_fkey");

                entity.HasOne(d => d.languageNavigation)
                    .WithMany(p => p.mp_clinician_language)
                    .HasForeignKey(d => d.language)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_clinician_language_language_fkey");
            });

            modelBuilder.Entity<mp_clinician_other_activities>(entity =>
            {
                entity.Property(e => e.activity).IsRequired();

                entity.Property(e => e.activity_type).IsRequired();

                entity.HasOne(d => d.clinician_)
                    .WithMany(p => p.mp_clinician_other_activities)
                    .HasForeignKey(d => d.clinician_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_clinician_other_activities_clinician_id_fkey");
            });

            modelBuilder.Entity<mp_clinician_population>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('mp_profile_population_id_seq'::regclass)");

                entity.HasOne(d => d.clinician_)
                    .WithMany(p => p.mp_clinician_population)
                    .HasForeignKey(d => d.clinician_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_clinician_population_clinician_id_fkey");

                entity.HasOne(d => d.population_)
                    .WithMany(p => p.mp_clinician_population)
                    .HasForeignKey(d => d.population_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_clinician_population_population_id_fkey");
            });

            modelBuilder.Entity<mp_clinician_practice>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('mp_applicant_practice_id_seq'::regclass)");

                entity.Property(e => e.city).IsRequired();

                entity.Property(e => e.duration).IsRequired();

                entity.Property(e => e.hospital).IsRequired();

                entity.Property(e => e.role).IsRequired();

                entity.HasOne(d => d.clinician_)
                    .WithMany(p => p.mp_clinician_practice)
                    .HasForeignKey(d => d.clinician_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_clinician_practice_applicant_id_fkey");
            });

            modelBuilder.Entity<mp_clinician_rating>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();
            });

            modelBuilder.Entity<mp_clinician_specialty>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('mp_profile_specialty_id_seq'::regclass)");

                entity.HasOne(d => d.clinician_)
                    .WithMany(p => p.mp_clinician_specialty)
                    .HasForeignKey(d => d.clinician_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_clinician_specialty_clinician_id_fkey");

                entity.HasOne(d => d.specialty_)
                    .WithMany(p => p.mp_clinician_specialty)
                    .HasForeignKey(d => d.specialty_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_clinician_specialty_specialty_id_fkey");
            });

            modelBuilder.Entity<mp_consent>(entity =>
            {
                entity.Property(e => e.consent).IsRequired();

                entity.Property(e => e.consent_type).IsRequired();
            });

            modelBuilder.Entity<mp_conversation>(entity =>
            {
                entity.Property(e => e.from).IsRequired();

                entity.Property(e => e.message).IsRequired();

                entity.Property(e => e.to).IsRequired();
            });

            modelBuilder.Entity<mp_countries>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('mp_countries_seq'::regclass)");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.sortname)
                    .IsRequired()
                    .HasMaxLength(3);
            });

            modelBuilder.Entity<mp_couple_intake>(entity =>
            {
                entity.Property(e => e.counseling_reason).IsRequired();

                entity.Property(e => e.created_by).IsRequired();

                entity.Property(e => e.length_of_relationship).IsRequired();

                entity.Property(e => e.partner_name).IsRequired();

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_couple_intake)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_couple_intake_profile_id_fkey");
            });

            modelBuilder.Entity<mp_couple_screening>(entity =>
            {
                entity.Property(e => e.couple_counseling_expectation).IsRequired();

                entity.Property(e => e.relationship_duration).IsRequired();
            });

            modelBuilder.Entity<mp_credit>(entity =>
            {
                entity.Property(e => e.amount).HasColumnType("money");

                entity.Property(e => e.created_by).IsRequired();

                entity.Property(e => e.transaction_reference).IsRequired();

                entity.HasOne(d => d.appointment_)
                    .WithMany(p => p.mp_credit)
                    .HasForeignKey(d => d.appointment_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_credit_appointment_id_fkey");
            });

            modelBuilder.Entity<mp_education_history>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('education_history_id_seq'::regclass)");

                entity.Property(e => e.created_at).HasColumnType("timestamp(6) without time zone");

                entity.Property(e => e.created_by).IsRequired();

                entity.Property(e => e.how_far_school).IsRequired();
            });

            modelBuilder.Entity<mp_emergency_contact>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('mp_emergency_contact_seqq'::regclass)");

                entity.Property(e => e.created_by).IsRequired();

                entity.Property(e => e.first_name).IsRequired();

                entity.Property(e => e.last_name).IsRequired();

                entity.Property(e => e.phone).IsRequired();

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_emergency_contact)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_mp_profile_mp_contact");
            });

            modelBuilder.Entity<mp_employment>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();

                entity.Property(e => e.employer).IsRequired();

                entity.Property(e => e.employment_length).IsRequired();

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_employment)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_mp_profile_employment");
            });

            modelBuilder.Entity<mp_enrollment>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_enrollment)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_enrollment_profile_id_fkey");
            });

            modelBuilder.Entity<mp_family_history>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_family_history)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_mp_profile_mp_family_history");
            });

            modelBuilder.Entity<mp_family_intake>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();

                entity.Property(e => e.partner_name).IsRequired();

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_family_intake)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_family_intake_profile_id_fkey");
            });

            modelBuilder.Entity<mp_form>(entity =>
            {
                entity.Property(e => e.name).IsRequired();
            });

            modelBuilder.Entity<mp_link_applicant_clinician>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('mp_link_applicant_profile_id_seq'::regclass)");

                entity.Property(e => e.created_by).IsRequired();

                entity.HasOne(d => d.applicant_)
                    .WithMany(p => p.mp_link_applicant_clinician)
                    .HasForeignKey(d => d.applicant_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_link_applicant_clinician_applicant_id_fkey");

                entity.HasOne(d => d.clinician_)
                    .WithMany(p => p.mp_link_applicant_clinician)
                    .HasForeignKey(d => d.clinician_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_link_applicant_clinician_clinician_id_fkey");
            });

            modelBuilder.Entity<mp_lk_appointment_activity>(entity =>
            {
                entity.Property(e => e.name).IsRequired();
            });

            modelBuilder.Entity<mp_lk_appointment_activity_sub>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.name).IsRequired();
            });

            modelBuilder.Entity<mp_lk_appointment_service>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('mp_appointment_service_id_seq'::regclass)");

                entity.Property(e => e.name).IsRequired();

                entity.Property(e => e.time_minutes).HasDefaultValueSql("30");
            });

            modelBuilder.Entity<mp_lk_appointment_type>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('mp_appointment_type_id_seq'::regclass)");

                entity.Property(e => e.name).IsRequired();
            });

            modelBuilder.Entity<mp_lk_drug>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('mp_drug_id_seq'::regclass)");

                entity.Property(e => e.description).IsRequired();

                entity.Property(e => e.name).IsRequired();
            });

            modelBuilder.Entity<mp_lk_expertise>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('mp_expertise_id_seq'::regclass)");

                entity.Property(e => e.name).IsRequired();
            });

            modelBuilder.Entity<mp_lk_mental_status>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.name).IsRequired();
            });

            modelBuilder.Entity<mp_lk_notification_type>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.name).IsRequired();
            });

            modelBuilder.Entity<mp_lk_psychiatric_diagnosis>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();
            });

            modelBuilder.Entity<mp_lk_specialty>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.name).IsRequired();
            });

            modelBuilder.Entity<mp_lnk_appointment_service_activity_sub>(entity =>
            {
                entity.HasKey(e => new { e.activity_sub_id, e.appointment_service_id })
                    .HasName("mp_lnk_appointment_service_activity_sub_pkey");

                entity.HasOne(d => d.activity_sub_)
                    .WithMany(p => p.mp_lnk_appointment_service_activity_sub)
                    .HasForeignKey(d => d.activity_sub_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_lnk_appointment_service_activity_sub_activity_sub_id_fkey");

                entity.HasOne(d => d.appointment_service_)
                    .WithMany(p => p.mp_lnk_appointment_service_activity_sub)
                    .HasForeignKey(d => d.appointment_service_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_lnk_appointment_service_activity_appointment_service_id_fkey");
            });

            modelBuilder.Entity<mp_lookup>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('lookup_id_seq'::regclass)");

                entity.Property(e => e.category).IsRequired();

                entity.Property(e => e.value).IsRequired();
            });

            modelBuilder.Entity<mp_major_illness_surgery>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.created_by).IsRequired();

                entity.Property(e => e.diagnosis).IsRequired();

                entity.Property(e => e.reason).IsRequired();

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_major_illness_surgery)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_profile_major_illness");
            });

            modelBuilder.Entity<mp_medical_history>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('medical_history_id_seq'::regclass)");

                entity.Property(e => e.created_by).IsRequired();

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_medical_history)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_medical_history_profile_id_fkey");
            });

            modelBuilder.Entity<mp_medication>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('medication_id_seq'::regclass)");

                entity.Property(e => e.created_by).IsRequired();

                entity.Property(e => e.dosage).IsRequired();

                entity.Property(e => e.last_dose).HasColumnType("time without time zone");

                entity.Property(e => e.medication).IsRequired();

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_medication)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_profile_medications");
            });

            modelBuilder.Entity<mp_medication_information>(entity =>
            {
                entity.HasOne(d => d.appointment_)
                    .WithMany(p => p.mp_medication_information)
                    .HasForeignKey(d => d.appointment_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_medication_information_appointment_id_fkey");

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_medication_information)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_medication_information_profile_id_fkey");
            });

            modelBuilder.Entity<mp_mental_health_plan>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();

                entity.Property(e => e.history).IsRequired();

                entity.Property(e => e.long_term_goal).IsRequired();

                entity.Property(e => e.problem_area).IsRequired();

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_mental_health_plan)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_mental_health_plan_profile_id_fkey");
            });

            modelBuilder.Entity<mp_mental_health_plan_objective>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();

                entity.Property(e => e.objective).IsRequired();

                entity.HasOne(d => d.health_plan_)
                    .WithMany(p => p.mp_mental_health_plan_objective)
                    .HasForeignKey(d => d.health_plan_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_mental_health_plan_objective_health_plan_id_fkey");
            });

            modelBuilder.Entity<mp_mental_health_plan_review_period>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('mp_review_period_id_seq'::regclass)");

                entity.Property(e => e.goal).IsRequired();

                entity.HasOne(d => d.health_plan_)
                    .WithMany(p => p.mp_mental_health_plan_review_period)
                    .HasForeignKey(d => d.health_plan_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_mental_health_plan_review_period_health_plan_id_fkey");
            });

            modelBuilder.Entity<mp_notification>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();

                entity.Property(e => e.created_by_name).IsRequired();

                entity.Property(e => e.notification).IsRequired();

                entity.Property(e => e.title).IsRequired();

                entity.Property(e => e.user_id).IsRequired();
            });

            modelBuilder.Entity<mp_ped_evaluation_history>(entity =>
            {
                entity.HasOne(d => d.appointment_)
                    .WithMany(p => p.mp_ped_evaluation_history)
                    .HasForeignKey(d => d.appointment_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_ped_evaluation_history_appointment_id_fkey");

                entity.HasOne(d => d.ped_eval_)
                    .WithMany(p => p.mp_ped_evaluation_history)
                    .HasForeignKey(d => d.ped_eval_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_ped_evaluation_history_ped_eval_id_fkey");

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_ped_evaluation_history)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_ped_evaluation_history_profile_id_fkey");
            });

            modelBuilder.Entity<mp_ped_symptomps>(entity =>
            {
                entity.HasOne(d => d.ped_evaluation_)
                    .WithMany(p => p.mp_ped_symptomps)
                    .HasForeignKey(d => d.ped_evaluation_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ped_symptom_ped_evaluation");

                entity.HasOne(d => d.symptom_)
                    .WithMany(p => p.mp_ped_symptomps)
                    .HasForeignKey(d => d.symptom_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ped_symptom_symptom");
            });

            modelBuilder.Entity<mp_pediatric_evaluation>(entity =>
            {
                entity.Property(e => e.next_visit).HasColumnType("date");

                entity.Property(e => e.visit_date).HasColumnType("date");
            });

            modelBuilder.Entity<mp_pharmacy>(entity =>
            {
                entity.Property(e => e.email).IsRequired();

                entity.Property(e => e.name).IsRequired();
            });

            modelBuilder.Entity<mp_phc_health_history>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();
            });

            modelBuilder.Entity<mp_phc_medical_history>(entity =>
            {
                entity.Property(e => e.complain).IsRequired();

                entity.Property(e => e.created_by).IsRequired();

                entity.Property(e => e.food).IsRequired();

                entity.Property(e => e.medication).IsRequired();

                entity.Property(e => e.visit_reason).IsRequired();
            });

            modelBuilder.Entity<mp_phc_mental_status>(entity =>
            {
                entity.Property(e => e.create_by).IsRequired();
            });

            modelBuilder.Entity<mp_phc_social_history>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();
            });

            modelBuilder.Entity<mp_phc_system_review>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();
            });

            modelBuilder.Entity<mp_population_group>(entity =>
            {
                entity.Property(e => e.name).IsRequired();
            });

            modelBuilder.Entity<mp_prescription>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();

                entity.Property(e => e.viewed).HasDefaultValueSql("0");

                entity.HasOne(d => d.clinician_)
                    .WithMany(p => p.mp_prescription)
                    .HasForeignKey(d => d.clinician_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_prescription_clinician_id_fkey");

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_prescription)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_prescription_profile_id_fkey");
            });

            modelBuilder.Entity<mp_prescription_drug>(entity =>
            {
                entity.Property(e => e.dosage).IsRequired();

                entity.Property(e => e.drug).IsRequired();

                entity.HasOne(d => d.prescription_)
                    .WithMany(p => p.mp_prescription_drug)
                    .HasForeignKey(d => d.prescription_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_prescription_drug_prescription_id_fkey");
            });

            modelBuilder.Entity<mp_profile>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.dob).HasColumnType("date");

                entity.Property(e => e.first_name).IsRequired();

                entity.Property(e => e.gender).HasDefaultValueSql("1");

                entity.Property(e => e.last_name).IsRequired();

                entity.Property(e => e.phone).IsRequired();

                entity.Property(e => e.preferred_name).IsRequired();

                entity.Property(e => e.unique_id).ValueGeneratedOnAdd();

                entity.Property(e => e.user_id).IsRequired();
            });

            modelBuilder.Entity<mp_profile_bank>(entity =>
            {
                entity.Property(e => e.account_name).IsRequired();

                entity.Property(e => e.account_number).IsRequired();

                entity.Property(e => e.bank_name).IsRequired();

                entity.Property(e => e.created_by).IsRequired();
            });

            modelBuilder.Entity<mp_profile_hmo>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();

                entity.Property(e => e.hmo_name).IsRequired();

                entity.Property(e => e.insurance_number).IsRequired();

                entity.Property(e => e.organization_name).IsRequired();

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_profile_hmo)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_profile_hmo_profile_id_fkey");
            });

            modelBuilder.Entity<mp_profile_match>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.HasOne(d => d.appointment_activity_)
                    .WithMany(p => p.mp_profile_match)
                    .HasForeignKey(d => d.appointment_activity_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_select_clinician_appointment_activity_id_fkey");

                entity.HasOne(d => d.appointment_activity_sub_)
                    .WithMany(p => p.mp_profile_match)
                    .HasForeignKey(d => d.appointment_activity_sub_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_select_clinician_appointment_activity_sub_id_fkey");

                entity.HasOne(d => d.appointment_type_)
                    .WithMany(p => p.mp_profile_match)
                    .HasForeignKey(d => d.appointment_type_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_select_clinician_appointment_type_id_fkey");

                entity.HasOne(d => d.clinician_)
                    .WithMany(p => p.mp_profile_match)
                    .HasForeignKey(d => d.clinician_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_select_clinician_clinician_id_fkey");

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_profile_match)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_select_clinician_profile_id_fkey");
            });

            modelBuilder.Entity<mp_profile_mental_status>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();
            });

            modelBuilder.Entity<mp_progress_note>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_progress_note)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_mp_profile_progress_note");
            });

            modelBuilder.Entity<mp_psychiatric_opd_evaluation>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();

                entity.Property(e => e.problem).IsRequired();

                entity.Property(e => e.summary_of_recommendation).IsRequired();

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_psychiatric_opd_evaluation)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_psychiatric_opd_evaluation_profile_id_fkey");
            });

            modelBuilder.Entity<mp_psychiatric_opd_evaluation_diagnosis>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('\"mp_psychiatric_opd_evaluation-diagnosis_id_seq\"'::regclass)");

                entity.Property(e => e.created_by).IsRequired();

                entity.HasOne(d => d.psychiatric_evaluation_)
                    .WithMany(p => p.mp_psychiatric_opd_evaluation_diagnosis)
                    .HasForeignKey(d => d.psychiatric_evaluation_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_psychiatric_opd_evaluation-di_psychiatric_evaluation_id_fkey");
            });

            modelBuilder.Entity<mp_psychiatric_progress_note>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();

                entity.Property(e => e.date_of_service).HasColumnType("date");

                entity.HasOne(d => d.appointment_)
                    .WithMany(p => p.mp_psychiatric_progress_note)
                    .HasForeignKey(d => d.appointment_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_psychiatric_progress_note_appointment_id_fkey");

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_psychiatric_progress_note)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_psychiatric_progress_note_profile_id_fkey");
            });

            modelBuilder.Entity<mp_psychosocial>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('psychosocial_id_seq'::regclass)");

                entity.Property(e => e.created_by).IsRequired();

                entity.Property(e => e.effect_symptons).IsRequired();

                entity.Property(e => e.history).IsRequired();

                entity.Property(e => e.problem).IsRequired();
            });

            modelBuilder.Entity<mp_referral>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();

                entity.HasOne(d => d.clinician_)
                    .WithMany(p => p.mp_referral)
                    .HasForeignKey(d => d.clinician_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_referral_clinician_id_fkey");

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_referral)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_referral_profile_id_fkey");

                entity.HasOne(d => d.profile_match_)
                    .WithMany(p => p.mp_referral)
                    .HasForeignKey(d => d.profile_match_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_referral_profile_match_id_fkey");
            });

            modelBuilder.Entity<mp_service_costing>(entity =>
            {
                entity.Property(e => e.cost).HasColumnType("money");

                entity.Property(e => e.created_by).IsRequired();

                entity.HasOne(d => d.appointment_activity_sub_)
                    .WithMany(p => p.mp_service_costing)
                    .HasForeignKey(d => d.appointment_activity_sub_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_service_costing_appointment_activity_sub_id_fkey");

                entity.HasOne(d => d.appointment_service_)
                    .WithMany(p => p.mp_service_costing)
                    .HasForeignKey(d => d.appointment_service_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_service_costing_appointment_service_id_fkey");

                entity.HasOne(d => d.clinician_)
                    .WithMany(p => p.mp_service_costing)
                    .HasForeignKey(d => d.clinician_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_service_costing_clinician_id_fkey");
            });

            modelBuilder.Entity<mp_social_relationship>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_social_relationship)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_profile_social_relationship");
            });

            modelBuilder.Entity<mp_states>(entity =>
            {
                entity.Property(e => e.id).HasDefaultValueSql("nextval('mp_states_seq'::regclass)");

                entity.Property(e => e.country_id).HasDefaultValueSql("1");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.country_)
                    .WithMany(p => p.mp_states)
                    .HasForeignKey(d => d.country_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_mp_states_mp_country");
            });

            modelBuilder.Entity<mp_substance_use>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();
            });

            modelBuilder.Entity<mp_summary_treatment_history>(entity =>
            {
                entity.Property(e => e.date_of_service).HasColumnType("date");

                entity.HasOne(d => d.appointment_)
                    .WithMany(p => p.mp_summary_treatment_history)
                    .HasForeignKey(d => d.appointment_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_summary_treatment_history_appointment_id_fkey");

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_summary_treatment_history)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_summary_treatment_history_profile_id_fkey");

                entity.HasOne(d => d.psychiatric_evaluation_)
                    .WithMany(p => p.mp_summary_treatment_history)
                    .HasForeignKey(d => d.psychiatric_evaluation_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_summary_treatment_history_psychiatric_opd_id_fkey");
            });

            modelBuilder.Entity<mp_surgical_history>(entity =>
            {
                entity.Property(e => e.created_by).IsRequired();

                entity.HasOne(d => d.profile_)
                    .WithMany(p => p.mp_surgical_history)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("mp_surgical_history_profile_id_fkey");
            });

            modelBuilder.HasSequence("applicant_document_seq");

            modelBuilder.HasSequence("child_screening_id_seq");

            modelBuilder.HasSequence("education_history_id_seq");

            modelBuilder.HasSequence("mp_emergency_contact_seqq");

            modelBuilder.HasSequence("lookup_id_seq");

            modelBuilder.HasSequence("medical_history_id_seq");

            modelBuilder.HasSequence("medication_id_seq");

            modelBuilder.HasSequence("mp_cities_seq");

            modelBuilder.HasSequence("mp_clinician_category_id_seq");

            modelBuilder.HasSequence("mp_clinician_language_id_seq");

            modelBuilder.HasSequence("mp_clinician_other_activities_id_seq");

            modelBuilder.HasSequence("mp_clinician_practice_id_seq");

            modelBuilder.HasSequence("mp_countries_seq");

            modelBuilder.HasSequence("mp_enrollment_id_seq");

            modelBuilder.HasSequence("mp_medication_information_id_seq");

            modelBuilder.HasSequence("mp_ped_evaluation_history_id_seq");

            modelBuilder.HasSequence("mp_ped_symptomps_id_seq");

            modelBuilder.HasSequence("mp_pediatric_evaluation_id_seq");

            modelBuilder.HasSequence("mp_phc_health_history_id_seq");

            modelBuilder.HasSequence("mp_phc_medical_history_id_seq");

            modelBuilder.HasSequence("mp_phc_mental_status_id_seq");

            modelBuilder.HasSequence("mp_phc_social_history_id_seq");

            modelBuilder.HasSequence("mp_phc_system_review_id_seq");

            modelBuilder.HasSequence("mp_psychiatric_progress_note_id_seq");

            modelBuilder.HasSequence("mp_states_seq");

            modelBuilder.HasSequence("mp_summary_treatment_history_id_seq");

            modelBuilder.HasSequence("mp_symptoms_id_seq");

            modelBuilder.HasSequence("psychosocial_id_seq");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

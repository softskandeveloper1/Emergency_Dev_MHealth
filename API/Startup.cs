﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Helpers;
using WebApi.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using MHealth.Data;
using Microsoft.EntityFrameworkCore;
using DAL.IService;
using DAL.Service;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();


            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IApplicantService, ApplicantService>();
            services.AddScoped<IClinicianDocumentService, ClinicianDocumentService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<IApplicantDocumentService, ApplicantDocumentService>();
            services.AddScoped<IAppointmentService, AppointmentService>();

            services.AddScoped<IChildrenService, ChildrenService>();
            services.AddScoped<IEducationService, EducationService>();
            services.AddScoped<IEmergencyContactService, EmergencyContactService>();
            services.AddScoped<IEmploymentService, EmploymentService>();
            services.AddScoped<IFamilyHistoryService, FamilyHistoryService>();
            services.AddScoped<ISubstanceUseService, SubstanceUseService>();
            services.AddScoped<ISocialRelService, SocialRelService>();
            services.AddScoped<IPsychosocialService, PsychosocialService>();
            services.AddScoped<IProgressNoteService, ProgressNoteService>();
            services.AddScoped<ILookUpService, LookUpService>();
            services.AddScoped<IExpertiseService, ExpertiseService>();
            services.AddScoped<IClinicianAvailabilityService, ClinicianAvailabilityService>();
            services.AddScoped<ICreditService, CreditService>();

            services.AddScoped<IClinicianService, ClinicianService>();
            services.AddScoped<IPopulationService, PopulationService>();
            services.AddScoped<ISpecialtyService, SpecialtyService>();
            services.AddScoped<IFamilyIntakeService, FamilyIntakeService>();
            services.AddScoped<IMentalHealthPlanService, MentalHealthPlanService>();
            services.AddScoped<IAppointmentFormService, AppointmentFormService>();


            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IOtherActivitiesService, OtherActivitiesService>();
            services.AddScoped<IPracticeService, PracticeService>();
            services.AddScoped<ILanguageService, LanguageService>();

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseNpgsql(
                   Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllers();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}

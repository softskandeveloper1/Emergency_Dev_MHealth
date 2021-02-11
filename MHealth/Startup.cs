using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using MHealth.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DAL.IService;
using DAL.Service;
using MHealth.Helper;
using MHealth.Entities;
using MHealth.Hub;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.AspNetCore.Authentication;

using Microsoft.IdentityModel.Tokens;
using System.Text;
using Quartz.Spi;
using MHealth.Helper.Schedulers;
using Quartz;
using Quartz.Impl;

namespace MHealth
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Add Quartz services
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            // Add our job
            services.AddSingleton<RemindersJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(RemindersJob),
                cronExpression: "0 0/5 * 1/1 * ? *")); // run every 5 min

            services.AddSingleton<ReleaseAppoinmetJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(ReleaseAppoinmetJob),
                cronExpression: "0 0/5 * 1/1 * ? *")); // run every 5 min

            services.AddHostedService<QuartzHostedService>();

            services.AddAuthentication()
               .AddCookie(cfg => cfg.SlidingExpiration = true)
               .AddJwtBearer(cfg =>
               {
                   cfg.RequireHttpsMetadata = false;
                   cfg.SaveToken = true;

                   cfg.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidIssuer = Configuration["Tokens:Issuer"],
                       ValidAudience = Configuration["Tokens:Issuer"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                   };

               });



            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost";
                options.InstanceName = "mhealth_instance";
            });

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
            services.AddScoped<IPsychiatricProgressNoteService, PsychiatricProgressNoteService>();
            services.AddScoped<IProfileMatchService, ProfileMatchService>();
            services.AddScoped<IServiceCostService, ServiceCostService>();
            services.AddScoped<IApplicantChecklistService, ApplicantChecklistService>();

            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IOtherActivitiesService, OtherActivitiesService>();
            services.AddScoped<IPracticeService, PracticeService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IProviderCategoryService, ProviderCategoryService>();
            services.AddScoped<ICoupleScreeningService, CoupleScreeningService>();
            services.AddScoped<IPrescriptionService, PrescriptionService>();
            services.AddScoped<IClinicService, ClinicService>();
            services.AddScoped<IPharmacyService, PharmacyService>();
            services.AddScoped<IAppointmentRefundService, AppointmentRefundService>();
            services.AddScoped<IProfileBankService, ProfileBankService>();
            services.AddScoped<IReferralService, ReferralService>();
            services.AddScoped<IProfileHMOService, ProfileHMOService>();
            services.AddScoped<IAppointmentLogService, AppointmentLogService>();
            services.AddScoped<IEvaluationService, EvaluationService>();

            services.AddOptions();
            services.Configure<PayStackSettings>(Configuration.GetSection("PayStackSettings"));
            services.Configure<TwilioSettings>(Configuration.GetSection("TwilioSettings"));
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSeq();
            });

            services.AddSignalR();
            services.AddRazorPages();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            //app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

          
            app.UseAuthentication();

           // app.UseIdentityServer();

            app.UseAuthorization();

          
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages();
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
                endpoints.MapRazorPages();
                endpoints.MapHub<Chat>("/chat");
            });

            // app.UseMvc();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            //});
            var t = Task.Run(() => CreateUserRoles(services));
            t.Wait();
            //CreateUserRoles(services).Wait();
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();


            IdentityResult roleResult;
            //Adding Addmin Role   
            var roleCheck = await RoleManager.RoleExistsAsync("client");
            if (!roleCheck)
            {
                //create the roles and seed them to the database   
                roleResult = await RoleManager.CreateAsync(new IdentityRole("client"));
            }

            roleCheck = await RoleManager.RoleExistsAsync("clinician");
            if (!roleCheck)
            {
                //create the roles and seed them to the database   
                roleResult = await RoleManager.CreateAsync(new IdentityRole("clinician"));
            }

            roleCheck = await RoleManager.RoleExistsAsync("admin");
            if (!roleCheck)
            {
                //create the roles and seed them to the database   
                roleResult = await RoleManager.CreateAsync(new IdentityRole("admin"));
            }

            roleCheck = await RoleManager.RoleExistsAsync("super_admin");
            if (!roleCheck)
            {
                //create the roles and seed them to the database   
                roleResult = await RoleManager.CreateAsync(new IdentityRole("super_admin"));
            }

            //Assign Admin role to the main User here we have given our newly loregistered login id for Admin management   
            // IdentityUser user = await UserManager.FindByEmailAsync("ioxcharlex@gmail.com");
            var user = new ApplicationUser { Email = "ioxcharlex@gmail.com", UserName = "ioxcharlex@gmail.com" };
            await UserManager.CreateAsync(user, "P@ssw0rd");
            await UserManager.AddToRoleAsync(user, "super_admin");

        }
    }
}

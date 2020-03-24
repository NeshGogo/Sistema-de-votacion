using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sistema_de_votacion.Data;
using Sistema_de_votacion.Data.Candidates;
using Sistema_de_votacion.Data.Citizens;
using Sistema_de_votacion.Data.PoliticParties;
using Sistema_de_votacion.Data.Positions;
using Sistema_de_votacion.Data.Elections;
using Sistema_de_votacion.Models;
using Sistema_de_votacion.Services.Candidates;
using Sistema_de_votacion.Services.Candidates.Positions;
using Sistema_de_votacion.Services.Citizens;
using Sistema_de_votacion.Services.PoliticParties;
using Sistema_de_votacion.Services.Elections;
using AutoMapper;
using Sistema_de_votacion.Data.ElectionCandidates;
using Sistema_de_votacion.Data.ElectionPositions;
using Sistema_de_votacion.Data.ElectionCitizens;
using Sistema_de_votacion.Data.ElectionPoliticParties;

namespace Sistema_de_votacion
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
            services.AddDbContextPool<ElectionDBContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("ElectionDBConnection")));
            //Inyeccion de dependencia de Identity.
            services.AddIdentity<IdentityUser, IdentityRole>(o => 
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 5;
            })
            .AddEntityFrameworkStores<ElectionDBContext>()
            .AddDefaultTokenProviders();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<IPoliticPartyRepository, PoliticPartyRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<IPoliticPartyService, PoliticPartyService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<ICitizenRepository, CitizenRepository>();
            services.AddScoped<ICitizenService, CitizenService>();
            services.AddScoped<IElectionRepository, ElectionRepository>();
            services.AddScoped<IElectionService, ElectionService>();
            services.AddScoped<IElectionCandidateRepository, ElectionCandidateRepository>();
            services.AddScoped<IElectionPositionRepository, ElectionPositionRepository>();
            services.AddScoped<IElectionCitizenRepository, ElectionCitizenRepository>();
            services.AddScoped<IElectionPoliticPartyRepository, ElectionPoliticPartyRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Elections}/{action=Index}/{id?}");
            });
        }
    }
}

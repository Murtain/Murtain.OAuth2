using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Murtain.OAuth2.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Murtain.OAuth2
{
    public class Startup
    {
        /// <summary>
        /// ���ݿ������ַ���
        /// </summary>
        private readonly string CONNECTION_STRING = "Data Source=101.132.152.24;port=3306;Initial Catalog=vir_authorize;user id=remote;password=Mysql123!;";
        /// <summary>
        /// ����Ǩ�������ļ�����Ӧ�ó���
        /// </summary>
        private readonly string MIGRATION_ASSEMBLY_NAME = Assembly.GetAssembly(typeof(ModelsContainer)).GetName().Name;


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                // this adds the config data from DB (clients, resources, CORS)
                .AddConfigurationStore(options =>
                {
                    options.ResolveDbContextOptions = (provider, builder) =>
                    {
                        builder.UseMySql(CONNECTION_STRING,
                            sql => sql.MigrationsAssembly(MIGRATION_ASSEMBLY_NAME));
                    };
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                    {
                        builder.UseMySql(CONNECTION_STRING, sql => sql.MigrationsAssembly(MIGRATION_ASSEMBLY_NAME));
                    };
                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 10; // interval in seconds, short for testing
                });

            services.AddMvc();

            services.AddDbContext<ModelsContainer>(option =>
            {
                option.UseMySql(CONNECTION_STRING);
            });

            services.AddMurtain()
                    .AddApplicationService()
                    .AddLoggerInterception()
                    .AddUnitOfWork()
                    .AddCacheManager()
                    .AddGlobalSettingManager()
                    .AddAutoMapper()
                    .AddEntityFrameWork();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }


            app.UseIdentityServer();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using Murtain.OAuth2.Infrastructure;
using Murtain.OAuth2.Core;
using Murtain.OAuth2.Domain;
using Murtain.OAuth2.ApplicationService;
using Murtain.OAuth2.SDK;


using System.Reflection;
using Murtain.OAuth2.Core.Providers;

namespace Murtain.OAuth2
{
    public class Startup
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private readonly string CONNECTION_STRING = "Data Source=101.132.152.24;port=3306;Initial Catalog=vir_authorize;user id=remote;password=Mysql123!;";
        /// <summary>
        /// 数据迁移配置文件所在应用程序集
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
                    .AddDependency()
                    .AddLoggerInterception()
                    .AddUnitOfWork()
                    .AddCacheManager()
                    .AddGlobalSettingManager(c =>
                    {
                        c.Providers.Add<EmailSettingProvider>();
                    })
                    .AddAutoMapper()
                    .AddEntityFrameWork();


            System.Diagnostics.Debug.WriteLine("   　 へ　　　　　／|");
            System.Diagnostics.Debug.WriteLine("   　/＼7　　∠＿/");
            System.Diagnostics.Debug.WriteLine("    /　│　　 ／　／");
            System.Diagnostics.Debug.WriteLine("   │　Z ＿,＜　／　　 /`ヽ");
            System.Diagnostics.Debug.WriteLine("   │　　　　　ヽ　　 /　　〉");
            System.Diagnostics.Debug.WriteLine("    Y　　　　　 `　 /　　/");
            System.Diagnostics.Debug.WriteLine("   ｲ  ●　､　● ⊂⊃〈　　/");
            System.Diagnostics.Debug.WriteLine("   ()　 へ　　　　|　＼〈");
            System.Diagnostics.Debug.WriteLine("   　>ｰ ､_　 ィ　 │ ／／");
            System.Diagnostics.Debug.WriteLine("    / へ　　 /　ﾉ＜| ＼＼");
            System.Diagnostics.Debug.WriteLine("    ヽ_ﾉ　　(_／　 │／／");
            System.Diagnostics.Debug.WriteLine("   　7　　　　　　　|／");
            System.Diagnostics.Debug.WriteLine("   　＞―r￣￣`ｰ―＿");

            foreach (var service in services.Where(x => x.ServiceType.ToString().StartsWith("Murtain")))
            {
                System.Diagnostics.Debug.WriteLine(service.ServiceType.ToString(), "【Murtain->Setup->ConfigureServices】");
            }
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseMurtain()
               .UseAutoMapper();

            app.UseIdentityServer();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Passport}/{action=Index}/{id?}");
            });
        }
    }
}

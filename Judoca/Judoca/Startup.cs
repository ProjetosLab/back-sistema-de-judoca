using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Judoca.ModelsEF;

namespace Judoca
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
                {
                    options.AddPolicy(name: MyAllowSpecificOrigins,
                                    builder =>
                                    {
                                        builder.WithOrigins("http://web-judocakid.s3-website-us-east-1.amazonaws.com",
                                            "http://front-sistema-de-judoca.herokuapp.com");
                                        builder.AllowAnyOrigin();
                                        builder.AllowAnyMethod();
                                        builder.AllowAnyHeader();
                                        builder.AllowCredentials();
                                        
                                    });
                });

            var chave = "Server=judoca.cfk4trdj3utg.us-east-1.rds.amazonaws.com,1433;Database=Judoca;User ID=admin;Password=master123;";
            services.AddDbContext<JudocaContext>(opt => opt.UseSqlServer(chave));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            Services.Extension.AddContexts(services);

        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
 
            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action = Index}/{id?}");
            });
            
            
        }

    }
}

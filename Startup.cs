using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using ru.siliconbasement.micros.filesservice.storage;

namespace ru.siliconbasement.micros.filesservice
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
            services.AddMvc();
            
            // Add configuration as DI service
            services.Add(new ServiceDescriptor(typeof(IConfiguration), 
                     provider => new ConfigurationBuilder()
                                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", 
                                                 optional: false, 
                                                 reloadOnChange: true)
                                    .Build(), 
                     ServiceLifetime.Singleton));

            // Add application services.
            services.AddTransient<IDataStorage, LocalFileSystemStorage>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }

            app.UseMvc();
        }
    }
}

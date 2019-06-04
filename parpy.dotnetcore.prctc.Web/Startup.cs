using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using parpy.dotnetcore.prctc.Web.ms;
using Steeltoe.CircuitBreaker.Hystrix;
using Steeltoe.Common.Http.Discovery;
using Steeltoe.Discovery.Client;

namespace parpy.dotnetcore.prctc.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDiscoveryClient(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Add Steeltoe handler to container
            services.AddTransient<DiscoveryHttpMessageHandler>();
            
            services.AddHttpClient("msclient")
                .AddHttpMessageHandler<DiscoveryHttpMessageHandler>();

            services.AddHttpClient();


            services.AddTransient<IHystrixCommandOptions, HystrixCommandOptions>(m =>
                 new HystrixCommandOptions(HystrixCommandGroupKeyDefault.AsKey("micro-service"), HystrixCommandKeyDefault.AsKey("micro-service"))
            );

            services.AddHystrixCommand<IEurekaService, EurekaService>("eureka-service", Configuration);

            services.AddHystrixMetricsStream(Configuration);
            
            return IniteAutofacProvider(services);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDiscoveryClient();
            app.UseMvc();
            app.UseHystrixMetricsStream();
        }


        private IServiceProvider IniteAutofacProvider(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly).AsImplementedInterfaces();
            var Container = builder.Build();
            return new AutofacServiceProvider(Container);
        }
    }
}

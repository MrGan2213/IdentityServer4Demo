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
using Microsoft.OpenApi.Models;

namespace WebAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(s=> {
                s.SwaggerDoc("v1",new OpenApiInfo {Title="My API", Version="V1" });
            });
            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();
            services.AddAuthentication("Bearer")//是把验证服务注册到DI, 并配置了Bearer作为默认模式.
                .AddIdentityServerAuthentication(options=> {//是在DI注册了token验证的处理者是identity Server
                    options.RequireHttpsMetadata = false;//不启用https,如果是生产环境，一定要使用https
                    options.Authority = "http://localhost:5000";//指定Authorization Server（身份认证服务端）的地址
                    options.ApiName = "socialnetwork";//ApiName要和Authorization Server（身份认证服务端）里面配置ApiResource的name一样
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c=> {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","My API V1");
            });
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

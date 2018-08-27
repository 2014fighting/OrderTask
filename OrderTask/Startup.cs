using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using log4net;
using log4net.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OrderTask.Common;
using OrderTask.Common.Config;
using OrderTask.Model;
using OrderTask.Service.Service;
using OrderTask.Service.ServiceInterface;
using OrderTask.UnitOfWork;
using UEditorNetCore;

namespace OrderTask.Web
{
    public class Startup
    {
        public static ILoggerRepository Repository;
        private IServiceCollection _services;
        public Startup(IConfiguration configuration)
        {
            Repository = LogManager.CreateRepository("NETCoreRepository");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            ILogService<Startup> log = new LogService<Startup>();
            log.Info("ConfigureServices开始");

            services.AddDbContext<OrderTaskContext>(options => options.UseMySql(AppConfig.MySqlConnection));

            services.AddUnitOfWork<OrderTaskContext>();//添加UnitOfWork支持
            foreach (var item in ProjectCom.GetClassName("OrderTask.Service")) //集中注入服务
            {
                foreach (var typeArray in item.Value)
                {
                    services.AddScoped(typeArray, item.Key);
                }
            }
            services.AddScoped(typeof(ILogService<>), typeof(LogService<>));//注入泛型loger
            //添加授权支持，并添加使用Cookie的方式，配置登录页面和没有权限时的跳转页面。
            //https://www.cnblogs.com/seriawei/p/7452743.html
            services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })//传入默认授权方案
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.LoginPath = new PathString("/Account/Login");
                    o.AccessDeniedPath = new PathString("/Account/AccessDenied");
                    o.Events = new CookieAuthenticationEvents()
                    {
                        OnRedirectToLogin = (context) => context.Response.WriteAsync("<script>window.top.location.href ='/Account/Login'</script>")
                    };
                });

            services.AddAutoMapper();//配置autoapper

            //第一个参数为配置文件路径，默认为项目目录下config.json
            //第二个参数为是否缓存配置文件，默认false
            var path = Directory.GetCurrentDirectory();
            services.AddUEditorService(path+ "/wwwroot/lib/UEditor/net/config.json");

            services.AddMvc()//全局配置Json序列化处理
                .AddJsonOptions(options =>
                    {
                        //忽略循环引用
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        //不使用驼峰样式的key
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                        //设置时间格式
                        options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    }
                );
            _services = services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                ListAllRegisteredServices(app);
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    // Requires the following import:
                    // using Microsoft.AspNetCore.Http;
                    
                }
            });
            app.UseAuthentication();//使用授权  
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }


        private void ListAllRegisteredServices(IApplicationBuilder app)
        {
            app.Map("/allservices", builder => builder.Run(async context =>
            {
                var sb = new StringBuilder();
                sb.Append("<h1>All Services</h1>");
                sb.Append("<table><thead>");
                sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Instance</th></tr>");
                sb.Append("</thead><tbody>");
                foreach (var svc in _services)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                    sb.Append($"<td>{svc.Lifetime}</td>");
                    sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody></table>");
                await context.Response.WriteAsync(sb.ToString());
            }));
        }
    }
}

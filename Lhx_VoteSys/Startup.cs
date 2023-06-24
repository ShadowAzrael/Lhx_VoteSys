<<<<<<< HEAD
=======
using Lhx_VoteSys.Models.Database;
>>>>>>> 娣诲姞椤圭洰鏂囦欢銆?
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
=======
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
>>>>>>> 娣诲姞椤圭洰鏂囦欢銆?
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
=======
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Lhx_VoteSys.Filter;
using AuthorizeFilter = Lhx_VoteSys.Filter.AuthorizeFilter;
using Lhx_VoteSys.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NLog.Extensions.Logging;
using Lhx_VoteSys.service;
>>>>>>> 娣诲姞椤圭洰鏂囦欢銆?

namespace Lhx_VoteSys
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
<<<<<<< HEAD
            services.AddControllers();
=======
            //services.AddControllers();
            services.AddControllers(x =>
            {
                //全局添加过滤器
                x.Filters.Add<AuthorizeFilter>();    //添加授权过滤器
                x.Filters.Add<ResourceFilter>();     //添加资源过滤器
                x.Filters.Add<GlobalActionFilter>(); //添加方法过滤器
                x.Filters.Add<GlobalExceptionFilter>(); //添加异常过滤器
                x.Filters.Add<ResultFilter>(); //添加结果过滤器
            });

            //添加JWT
            services.Configure<JWTConfig>(Configuration.GetSection("JWTConfig"));

            var tokenConfigs = Configuration.GetSection("JWTConfig").Get<JWTConfig>();

            //Authentication
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigs.Secret)),
                    ValidIssuer = tokenConfigs.Issuer,
                    ValidAudience = tokenConfigs.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddDbContext<VoteSysContext>();

            //添加swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VoteSys", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"}
            },new string[] { }
        }
    });
            });

            services.AddLogging(logBuilder => {
                logBuilder.ClearProviders();
                logBuilder.AddNLog();
            });

            //注入JWT服务
            services.AddScoped<IJWTService, JWTService>();
>>>>>>> 娣诲姞椤圭洰鏂囦欢銆?
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

<<<<<<< HEAD
=======
            app.UseAuthentication();

>>>>>>> 娣诲姞椤圭洰鏂囦欢銆?
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
<<<<<<< HEAD
=======

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "coreMVC3.1");
                //sc.RoutePrefix = string.Empty;
                c.RoutePrefix = "swagger";     //如果是为空 访问路径就为 根域名/index.html,注意localhost:端口号/swagger是访问不到的
                                               //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件
                                               // c.RoutePrefix = "swagger"; // 如果你想换一个路径，直接写名字即可，比如直接写c.RoutePrefix = "swagger"; 则访问路径为 根域名/swagger/index.html
            });
>>>>>>> 娣诲姞椤圭洰鏂囦欢銆?
        }
    }
}

<<<<<<< HEAD
=======
using Lhx_VoteSys.Models.Database;
>>>>>>> 添加项目文件�?
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
=======
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
>>>>>>> 添加项目文件�?
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
>>>>>>> 添加项目文件�?

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
                //ȫ����ӹ�����
                x.Filters.Add<AuthorizeFilter>();    //�����Ȩ������
                x.Filters.Add<ResourceFilter>();     //�����Դ������
                x.Filters.Add<GlobalActionFilter>(); //��ӷ���������
                x.Filters.Add<GlobalExceptionFilter>(); //����쳣������
                x.Filters.Add<ResultFilter>(); //��ӽ��������
            });

            //���JWT
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

            //���swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VoteSys", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "���¿�����������ͷ����Ҫ���Jwt��ȨToken��Bearer Token",
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

            //ע��JWT����
            services.AddScoped<IJWTService, JWTService>();
>>>>>>> 添加项目文件�?
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

>>>>>>> 添加项目文件�?
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
                c.RoutePrefix = "swagger";     //�����Ϊ�� ����·����Ϊ ������/index.html,ע��localhost:�˿ں�/swagger�Ƿ��ʲ�����
                                               //·�����ã�����Ϊ�գ���ʾֱ���ڸ�������localhost:8001�����ʸ��ļ�
                                               // c.RoutePrefix = "swagger"; // ������뻻һ��·����ֱ��д���ּ��ɣ�����ֱ��дc.RoutePrefix = "swagger"; �����·��Ϊ ������/swagger/index.html
            });
>>>>>>> 添加项目文件�?
        }
    }
}

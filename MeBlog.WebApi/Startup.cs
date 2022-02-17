using MeBlog.IRepository;
using MeBlog.IService;
using MeBlog.Repository;
using MeBlog.Service;
using MeBlog.WebApi.Utility._AutoMapper;
using MeBlog.WebApi.Utility._Captcha;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeBlog.WebApi
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MeBlog.WebApi", Version = "v1" });
                #region Swaggerʹ�ü�Ȩ
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In=ParameterLocation.Header,
                    Type=SecuritySchemeType.ApiKey,
                    Description="ֱ�����¿�������Bearer {token}(ע������֮����һ���ո�)",
                    Name="Authorization",
                    BearerFormat ="JWT",
                    Scheme="Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
                #endregion
            });
            #region SqlSugarIOC
            services.AddSqlSugar(new IocConfig()
            {
                ConnectionString = this.Configuration["SqlConn"],
                DbType = IocDbType.SqlServer,
                IsAutoCloseConnection = true
            });
            #endregion
            #region IOC����ע��
            services.AddCustomIOC();
            #endregion

            #region JWT��Ȩ
            services.AddCustomJWT();
            #endregion

            #region AutoMapper
            services.AddAutoMapper(typeof(CustomAutoMapperProfile));
            #endregion

            #region ��֤��
            services.AddScoped<ICaptcha, Captcha>();
            #endregion
            #region ע��Session
            //���һ���ڴ滺��
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.HttpOnly = true;
            });
            #endregion

            #region ���ÿ���
            services.AddCors(options =>
            {
                options.AddPolicy("CorsSetup", policy =>
                {
                    policy.AllowAnyOrigin() //���淶��ʵ�������õ��õ���վ��ַ
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            #endregion


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MeBlog.WebApi v1"));
            }
            //����Session
            app.UseSession();
            //Cors�����м��
            app.UseCors("CorsSetup");

            app.UseRouting();

            //��ӵ��ܵ���
            app.UseAuthentication();//��Ȩ

            app.UseAuthorization();//��Ȩ

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

   public static class IOCExtend
    {
        //IOC
        public static IServiceCollection AddCustomIOC(this IServiceCollection services)
        {
            services.AddScoped<IBlogNewsRepository,BlogNewsRepository>();
            services.AddScoped<IBlogNewsService, BlogNewsService>();
            services.AddScoped<ITypeInfoRepository,TypeInfoRepository>();
            services.AddScoped<ITypeInfoService,TypeInfoService>();
            services.AddScoped<IWriterInfoRepository,WriterInfoRepository>();
            services.AddScoped<IWriterInfoService, WriterInfoService>();

            services.AddScoped<IStudentRepository,StudentRepository>();
            services.AddScoped<ICourseRepository,CourseRepository>();
            services.AddScoped<IScoreRepository, ScoreRepository>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ICourseService,CourseService>();
            services.AddScoped<IScoreService, ScoreService>();

            services.AddScoped<IYebMenuRepository, YebMenuRepository>();
            services.AddScoped<IYebMenuService, YebMenuService>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IJobLevelRepository, JobLevelRepository>();
            services.AddScoped<IJobLevelService, JobLevelService>();
            services.AddScoped<IPermissRepository, PermissRepository>();
            services.AddScoped<IPermissService, PermissService>();
            services.AddScoped<IYebPerMenuRepository, YebPerMenuRepository>();
            services.AddScoped<IYebPerMenuService, YebPerMenuService>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IYebStuPerRepository, YebStuPerRepository>();
            services.AddScoped<IYebStuPerService, YebStuPerService>();

            return services;
        }

        //��Ȩ
        public static IServiceCollection AddCustomJWT(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("lishanbinlishanbin")),
                        ValidateIssuer=true,
                        ValidIssuer="http://localhost:5000",
                        ValidateAudience=true,
                        ValidAudience="http://localhost:5000",
                        ValidateLifetime=true,
                        ClockSkew=TimeSpan.FromMinutes(60)
                    };
                });

            return services;
        }
    }

}

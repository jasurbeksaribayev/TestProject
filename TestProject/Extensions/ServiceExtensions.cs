using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TestProject.Data.GenericRepositories;
using TestProject.Data.IGenericRepositories;
using TestProject.Domain.Entities;
using TestProject.Service.IServices.Auth;
using TestProject.Service.IServices.Students;
using TestProject.Service.IServices.StudentSubjects;
using TestProject.Service.IServices.Subjects;
using TestProject.Service.IServices.Teachers;
using TestProject.Service.Services.Auth;
using TestProject.Service.Services.Students;
using TestProject.Service.Services.StudentSubjects;
using TestProject.Service.Services.Subjects;
using TestProject.Service.Services.Teachers;

namespace TestProject.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomerService(this IServiceCollection services)
        {
            // GenericRepositories
            services.AddScoped<IGenericRepository<Student>, GenericRepository<Student>>();
            services.AddScoped<IGenericRepository<Teacher>, GenericRepository<Teacher>>();
            services.AddScoped<IGenericRepository<Subject>, GenericRepository<Subject>>();
            services.AddScoped<IGenericRepository<StudentSubject>, GenericRepository<StudentSubject>>();

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IStudentSubjectService, StudentSubjectService>();
        }
            public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
            {
                var jwtSettings = configuration.GetSection("Jwt");

                string key = jwtSettings.GetSection("Key").Value;

                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))

                    };
                });
            }

            public static void AddSwaggerService(this IServiceCollection services)
            {
                services.AddSwaggerGen(p =>
                {
                    p.ResolveConflictingActions(ad => ad.FirstOrDefault());
                    p.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header
                    });

                    p.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
                });
            }
        }
    }

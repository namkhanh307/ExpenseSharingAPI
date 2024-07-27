using API.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.Repositories;
using Services.IServices;
using Services.Mapper;
using Services.Services;
using System.Text;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API"

                });

                // Thêm JWT Bearer Token vào Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header sử dụng scheme Bearer.",
                    Type = SecuritySchemeType.Http,
                    Name = "Authorization",
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
            //Add DBContext
            builder.Services.AddDbContext<ExpenseSharingContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //Add Scope
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IExpenseService, ExpenseService>();
            builder.Services.AddScoped<IGroupService, GroupService>();
            builder.Services.AddScoped<IPersonExpenseService, PersonExpenseService>();
            builder.Services.AddScoped<IPersonGroupService, PersonGroupService>();
            builder.Services.AddScoped<IReportService, ReportService>();
            builder.Services.AddScoped<IRecordService, RecordService>();
            builder.Services.AddScoped<IPersonService, PersonService>();
            builder.Services.AddScoped<ICalculateService, CalculateService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITokenService, TokenService>();

            //Add Automapper
            builder.Services.AddAutoMapper(typeof(GroupProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(ExpenseProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(PersonExpenseProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(PersonGroupProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(RecordProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(ReportProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(PersonProfile).Assembly);
            //Add Http Context
            builder.Services.AddHttpContextAccessor();
            //Add authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false; options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
                };
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole", policy =>
                    policy.RequireClaim("role", "admin"));
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseCors("AllowAll");
            
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            app.UseMiddleware<PermissionHandlingMiddleware>();

            app.MapControllers();
            //kkkk
            app.Run();
        }
    }
}

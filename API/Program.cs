using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.IRepositories;
using Repositories.Repositories;
using Services.IServices;
using Services.Mapper;
using Services.Services;

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
            builder.Services.AddSwaggerGen();
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

            //Add Automapper
            builder.Services.AddAutoMapper(typeof(GroupProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(ExpenseProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(PersonExpenseProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(PersonGroupProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(RecordProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(ReportProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(PersonProfile).Assembly);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

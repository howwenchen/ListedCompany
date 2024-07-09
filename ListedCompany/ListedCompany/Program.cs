using ListedCompany.Models;
using ListedCompany.Services.DatabaseHelper;
using ListedCompany.Services.Repository;
using ListedCompany.Services.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ListedCompany
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("Project");

            // Add services to the DI container.

            // 註冊 DbContext
            builder.Services.AddDbContext<db_aaa9ad_project20240703Context>(options =>
                options.UseSqlServer(connectionString));

            // 註冊 AutoMapper返回當前應用程式中加載的所有程式集
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // 註冊 DatabaseHelper
            builder.Services.AddScoped<IDatabaseHelper>(sp => new DatabaseHelper(connectionString));

            // 註冊 UnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // 註冊所有的 Repository
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // 啟用靜態檔案的存取
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

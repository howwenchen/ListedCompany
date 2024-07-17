using ListedCompany.Models;
using ListedCompany.Services;
using ListedCompany.Services.DatabaseHelper;
using ListedCompany.Services.IService;
using ListedCompany.Services.Repository;
using ListedCompany.Services.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ListedCompany
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // �]�w��Ʈw�s�u
            var connectionString = builder.Configuration.GetConnectionString("Project");

            // Add services to the DI container.

            // ���U DbContext
            builder.Services.AddDbContext<db_aaa9ad_project20240703Context>(options =>
                options.UseSqlServer(connectionString));

            // ���U AutoMapper��^��e���ε{�����[�����Ҧ��{����
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // ���U DatabaseHelper
            builder.Services.AddScoped<IDatabaseHelper>(sp => new DatabaseHelper(connectionString));

            // ���U UnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // ���U�Ҧ��� Repository
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // �ϥ��X�i��k�ʺA���U�Ҧ�Service
            builder.Services.AddAllServices(Assembly.GetExecutingAssembly());


            // ���U CORS �A��
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin",
                    builder => builder
                        .AllowAnyOrigin() 
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

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

            // �ҥ� CORS
            app.UseCors("AllowAllOrigin");

            // �ҥ��R�A�ɮת��s��
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

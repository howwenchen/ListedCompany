using ListedCompany.Services.Repository;
using ListedCompany.Services.Repository.UnitOfWork;

namespace ListedCompany;


    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var ConnectionString = builder.Configuration.GetConnectionString("Projcet");

            // Add services to the DI container.

            // ���U DbContext
            //builder.Services.AddDbContext<>(options =>
            //    options.UseSqlServer(ConnectionString));

            //AutoMapper�|��^��e���ε{���줤�[�����Ҧ��{����
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // ���U UnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // ���U�Ҧ��� Repository
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }

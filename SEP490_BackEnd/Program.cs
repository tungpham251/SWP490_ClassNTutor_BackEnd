using Microsoft.Extensions.DependencyInjection;
using SEP490_BackEnd.Mapper;
using SEP490_BackEnd.Repositories;
using SEP490_BackEnd.Repositories.Interface;
using SEP490_BackEnd.Services;
using SEP490_BackEnd.Services.Interface;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddAutoMapper(typeof(DTOToModels).Assembly);
        builder.Services.AddAutoMapper(typeof(ModelsToResponseModel).Assembly);

        builder.Services.AddScoped<IAccountServices, AccountServices>();
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();

        builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        }
        );
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
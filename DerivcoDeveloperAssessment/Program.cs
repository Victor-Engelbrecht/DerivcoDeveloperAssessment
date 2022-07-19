using DataAccess;
using Services.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISQLiteDataAccess, SQLiteDataAccess>();
//Dependancy Injection of any service inherited from IService in Services
IServiceCollection services = new ServiceCollection();
builder.Services.Scan(scan =>
                scan.FromAssemblyOf<Services.ServiceMethods.IService>()
                    .AddClasses(classes => classes.AssignableTo<Services.ServiceMethods.IService>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Demo.Application;
using WebApi.Demo.Domain;
using WebApi.Demo.Infrastructure;
using WebApi.Demo.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(Options =>
    {
        Options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Values.SelectMany(x => x.Errors);
            return new BadRequestObjectResult(new BaseException()
            {
                ErrorCode = 400,
                UserMessage = "error nhaap tu nguoi dugn",
                DevMessage = "Loi nhaaapj tu nguoi nhap",
                TraceId = "",
                MoreInfo = "",
                Errors = errors
            }.ToString() ?? "");
        };
    })
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["connectionString"];

builder.Services.AddScoped<IEmployeeRepository>( provider => new EmployeeRepository(connectionString));
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IDepartmentRepository>(provider => new DepartmentRepository(connectionString));
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

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

app.UseMiddleware<ExceptionMiddleware>();

app.Run();

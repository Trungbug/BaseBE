using Misa.demo.core.Interface.Repository;
using Misa.demo.core.Interface.Service;
using Misa.demo.core.Service;
using Misa.infrsatructure.Repository;
using Misa_FS.Middleware;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));

builder.Services.AddScoped<IShiftRepository, ShiftRepository>();
builder.Services.AddScoped<IShiftService, ShiftService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("*") // Cho phép t?t c? (ho?c "http://localhost:5173" cho an toàn h?n)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
//OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

var app = builder.Build();



app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();


app.Run();

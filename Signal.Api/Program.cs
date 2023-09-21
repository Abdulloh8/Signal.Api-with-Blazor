using Context;
using Microsoft.EntityFrameworkCore;
using Pro.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer
    ("Server=sql.bsite.net\\MSSQL2016; Database=abdulloh771_; User Id=abdulloh771_; Password=asdf1212; TrustServerCertificate=True");
});


               
               

builder.Services.AddSwaggerGen();


builder.Services.AddCors(cors =>
{
    cors.AddPolicy("any", corsPolicy =>
    {
        corsPolicy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("any");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.UseMiddleware<ErrorHanlerdMiddleware>();

app.MapControllers();

app.Run();





































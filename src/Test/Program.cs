using Microsoft.EntityFrameworkCore;
using Test.DAL;
using Test.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<RecordManiaDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);

builder.Services.AddScoped<IRecordService, RecordService>();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
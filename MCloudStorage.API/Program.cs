using MCloudStorage.API.Data;
using MCloudStorage.API.Services.Implementation;
using MCloudStorage.API.Services.ServicesInterface;
using MCloudStorage.Data.Repository.Implementation;
using MCloudStorage.Data.Repository.RepositoryInterface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();                             
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<InterfaceName, ImplementationName>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<ICloudinaryUpload, CloudinaryUpload>();
//builder.Services.AddScoped<ICloudinaryService, ClodinaryServiceExtension>();

builder.Services.AddCloudinary(ClodinaryServiceExtension.GetAccount(config));

 


//var connString = builder.Configuration.GetConnectionString("DocumentStorecontext");

//builder.Services.AddSqlServer<DocumentStoreContext>(connString);

builder.Services.AddDbContext<DocumentStoreContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DocumentStorecontext"));
});
    

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

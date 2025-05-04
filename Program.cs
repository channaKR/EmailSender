using EmailSender.Controllers;
using EmailSender.Models;
using EmailSender.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "File Upload API", Version = "v1" });
});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSetting"));
builder.Services.AddScoped<IEmailService,EmailService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseDeveloperExceptionPage(); // Enable detailed error page in development
app.UseAuthorization();

app.MapControllers();

app.Run();

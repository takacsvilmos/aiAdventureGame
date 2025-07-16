using AiAdventure.Backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddHttpClient();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddHttpClient<IGeminiService, GeminiService>();

builder.Services.AddDbContext<AiAdventureDbContext>(options => options.UseInMemoryDatabase("AiAdventureDemoDb"));

builder.Configuration.AddEnvironmentVariables();

var geminiKey = builder.Configuration["Gemini:ApiKey"];
Console.WriteLine($"[DEBUG] Gemini API Key: {geminiKey ?? "NOT FOUND"}");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();
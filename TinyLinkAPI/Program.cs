using Microsoft.EntityFrameworkCore;
using TinyLink.API.Infrastructure;
using TinyLink.API.Services;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                                        
                      });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITinyLinkService,TinyLinkService>();
builder.Services.AddScoped<IVisitService, VisitService>();
builder.Services.AddScoped<IGenericRepository<TinyLink.API.Models.TinyLink> ,GenericRepository<TinyLink.API.Models.TinyLink>>();
builder.Services.AddScoped<IGenericRepository<TinyLink.API.Models.Visit>, GenericRepository<TinyLink.API.Models.Visit>>();


var configuration = builder.Configuration;


builder.Services.AddDbContext<TinyLinkDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<TinyLinkDbContext>();
    dbContext.Database.Migrate();
}

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

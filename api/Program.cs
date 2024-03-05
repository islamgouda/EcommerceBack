using api.filter;
using api.Helper;
using api.Middelware;
using Core.Interface;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<Context>(optionbuilder =>
optionbuilder.UseSqlite(builder.Configuration.GetConnectionString("default"),
op => op.MigrationsAssembly(typeof(Context).Assembly.FullName)));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
    });
});
builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
{
    var options=ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
    return ConnectionMultiplexer.Connect(options);
});
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<LgFilter>();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseStaticFiles();
app.UseCors("CorsPolicy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddelware>();
app.UseStatusCodePagesWithReExecute("/errors/0");//use custome statuse code
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
using var scope=app.Services.CreateScope();
var services = scope.ServiceProvider;
var context =services.GetRequiredService<Context>();
//var logger= services.GetRequiredService<ILogger>();
try { 
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch {
   // logger.LogError("Error While Migrate");
}
app.Run();

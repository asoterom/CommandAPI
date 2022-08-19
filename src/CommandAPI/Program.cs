using CommandAPI.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using AutoMapper;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


var conStrBuilder = new NpgsqlConnectionStringBuilder();
conStrBuilder.ConnectionString = builder.Configuration.GetConnectionString("PostgreSqlConnection");
conStrBuilder.Username = builder.Configuration["UserID"];
conStrBuilder.Password = builder.Configuration["password"];


builder.Services.AddDbContext<CommandContext>( opt => {
    opt.UseNpgsql(
        //builder.Configuration.GetConnectionString("PostgreSqlConnection")
        conStrBuilder.ConnectionString
        );
});

//builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson( s =>
    {
        s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    });
    
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddScoped<ICommandAPIRepo,MockCommandAPIRepo>();
builder.Services.AddScoped<ICommandAPIRepo,SQLCommandAPIRepo>();

var app = builder.Build();

CommandContext dbcontext = app.Services.GetRequiredService<CommandContext>();
dbcontext.Database.Migrate();
dbcontext.Database.EnsureCreated();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

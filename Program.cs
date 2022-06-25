using G.Core.Emails;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string[] _AllowedOrigins = { "http://localhost", "http://localhost:4200" };

Config.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Config.ApiSettings = builder.Configuration.GetSection("ApiSettings").Get<ApiSettings>();
// Config.AdminUsers = builder.Configuration.GetSection("AdminUsers").Get<string>();
_AllowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.Configure<IISServerOptions>(options =>
{
    options.AutomaticAuthentication = false;
});
// builder.Services.Configure<AzureStorageConfig>(builder.Configuration.GetSection("AzureStorageConfig"));
//Config.EmailCredential = builder.Configuration.GetSection("EmailConfiguration").Get<MinvikAPI.Models.EmailCredential>();

builder.Services.AddSingleton(builder.Configuration);
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Config.ConnectionString));
builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(JsonExceptionFilter));
}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "QuestDbPOC", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuestDbPOC v1"));
    app.UseDeveloperExceptionPage();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDefaultFiles(new DefaultFilesOptions() { DefaultFileNames = new[] { "index.html" } });
app.UseCors(builder => builder.WithOrigins(_AllowedOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials());
app.UseAuthorization();
app.MapControllers();
app.Run();


using System.Text;
using System.Text.Json.Serialization;
using Builder_WASM.Server;
using Builder_WASM.Server.Data;
using Builder_WASM.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(connectionString));

//****************************************************************************************************************** //3
builder.Services.Configure<FormOptions>(o =>     
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});
//****************************************************************************************************************** 

builder.Services.AddAuthentication(aut=> 
{ 
    aut.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
    aut.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    aut.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    
    .AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidIssuer = AuthOptions.ISSUER,
        ValidAudience = AuthOptions.AUDIENCE,
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()         
    };
});
builder.Services.AddControllersWithViews()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); //  Ця штукенція дозволяє інклудити таблиці та серелізувати в жсон без помилок!
builder.Services.AddRazorPages();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();

//****************************************************************************************************************** //1
//app.UseCors("CorsPolicy");  
//****************************************************************************************************************** 

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

//****************************************************************************************************************** //2
if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")))
{
    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), @"Resources"));
}
app.UseStaticFiles(new StaticFileOptions()      
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});
//******************************************************************************************************************

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

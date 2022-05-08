using System.Text;
using Builder_WASM.Server;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddAuthorization();

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
        // ��������, ����� �� �������������� �������� ��� ��������� ������
        ValidateIssuer = false,
        // ����� �� �������������� ����������� ������
        ValidateAudience = false,
        // ������, �������������� ��������
        ValidIssuer = AuthOptions.ISSUER,
        // ��������� ����������� ������
        ValidAudience = AuthOptions.AUDIENCE,
        // ����� �� �������������� ����� �������������
        //ValidateLifetime = true,
        // ��������� ����� ������������
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.KEY)),
        // ��������� ����� ������������
        //ValidateIssuerSigningKey = true,
    };
});
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

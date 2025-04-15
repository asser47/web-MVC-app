using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication1;
using WebApplication1.Authentication;
using WebApplication1.Authorization;
using WebApplication1.Data;
using WebApplication1.Filters;
using WebApplication1.MiddleWares;
using WebApplication1.NewFolder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProductValidator>());
builder.Configuration.AddJsonFile("config.json");

//var attachmentOptions = builder.Configuration.GetSection("Attachments").Get<AttachmentOptions>();  
//builder.Services.AddSingleton(attachmentOptions);

//var attachmentOptions = new AttachmentOptions();
//builder.Configuration.GetSection("AttachmentOptions").Bind(attachmentOptions);
//builder.Services.AddSingleton(attachmentOptions);

builder.Services.Configure<AttachmentOptions>(builder.Configuration.GetSection("Attachments"));


// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<LogicActivityFilter>();
    options.Filters.Add<PermissionBasedAuthorizationFilter>();
}
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(cfg => cfg.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
//builder.Services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);
var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
builder.Services.AddSingleton(jwtOptions);

builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SingingKey))
    };
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

app.UseMiddleware<RateLimitingMiddleWare>();

app.UseMiddleware<ProfilingMiddleWare>();

app.MapControllers();

app.Run();

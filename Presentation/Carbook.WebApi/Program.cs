using Carbook.Application.Features.CQRS.Handlers.AboutHandlers;
using Carbook.Application.Features.CQRS.Handlers.BannerHandlers;
using Carbook.Application.Features.CQRS.Handlers.BrandHandlers;
using Carbook.Application.Features.CQRS.Handlers.CarHandlers;
using Carbook.Application.Features.CQRS.Handlers.CategoryHandlers;
using Carbook.Application.Features.CQRS.Handlers.ContactHandlers;
using Carbook.Application.Interfaces;
using Carbook.Application.Interfaces.CarInterfaces;
using Carbook.Persistence.Context;
using Carbook.Persistence.Repositories;
using Carbook.Persistence.Repositories.CarRepositories;
using Carbook.Application.Services;
using Carbook.Application.Interfaces.BlogInterfaces;
using Carbook.Persistence.Repositories.BlogRepositories;
using Carbook.Application.Interfaces.CarPricingInterfaces;
using Carbook.Persistence.Repositories.CarPricingRepositories;
using Carbook.Application.Interfaces.TagCloudInterfaces;
using Carbook.Persistence.Repositories.TagCloudRepositories;
using Carbook.Application.Features.RepositoryPattern;
using Carbook.Persistence.Repositories.CommentRepositories;
using Carbook.Application.Interfaces.StatisticsInterfaces;
using Carbook.Persistence.Repositories.StatisticsRepositories;
using Carbook.Application.Interfaces.RentACarInterfaces;
using Carbook.Persistence.Repositories.RentACarRepositories;
using Carbook.Application.Interfaces.CarFeatureInterfaces;
using Carbook.Persistence.Repositories.CarFeatureRepositories;
using Carbook.Application.Interfaces.CarDescriptionInterfaces;
using Carbook.Persistence.Repositories.CarDescriptionRepositories;
using Carbook.Application.Interfaces.ReviewInterfaces;
using Carbook.Persistence.Repositories.ReviewRepositories;
using FluentValidation.AspNetCore;
using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static System.Net.WebRequestMethods;
using Carbook.Application.Tools;
using Microsoft.Extensions.Hosting;
using Carbook.WebApi.Hubs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();

builder.Services.AddCors(opt =>// CORS (Cross-Origin Resource Sharing) yap�land�rmas� ekleniyor
{
    opt.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader()// T�m ba�l�klara (header) izin veriliyor
        .AllowAnyMethod() //T�m HTTP metotlar�na(GET, POST, PUT, DELETE, vs.) izin veriliyor
        .SetIsOriginAllowed((host) => true)// Herhangi bir origin (kaynak) adresine izin veriliyor
        // NOT: SetIsOriginAllowed(x => true) t�m domainlere a��k hale getirir. G�venlik a��s�ndan dikkatli kullan�lmal�
        .AllowCredentials(); // Kimlik bilgileri (cookie, authorization header vs.) g�nderilmesine izin veriliyor
    });
});
builder.Services.AddSignalR();// SignalR servisi ekleniyor
// SignalR, sunucudan istemcilere ger�ek zamanl� mesaj iletimi sa�lar (�rne�in: chat uygulamalar�, canl� bildirimler)



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters
    {

        ValidAudience = JwtTokenDefaults.ValidAudience, // Token'�n hedef kitlesi (audience) do�ru mu? Do�rula
        ValidIssuer = JwtTokenDefaults.ValidIssuer, // Token'� olu�turan (issuer) ger�ekten beklenen yer mi? Do�rula
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)), // Token'� imzalamak/do�rulamak i�in kullan�lacak gizli anahtar
        ValidateLifetime = true, // Token s�resi ge�mi� mi? Varsa ge�ersiz yap
        ValidateIssuerSigningKey = true // Token'�n imzas� do�ru anahtarla m� at�lm��? Kontrol et
    };
});



#region Registirations
// Add services to the container.
builder.Services.AddScoped<CarBookContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(ICarRepository), typeof(CarRepository));
builder.Services.AddScoped(typeof(IStatisticsRepository), typeof(StatisticsRepository));
builder.Services.AddScoped(typeof(IBlogRepository), typeof(BlogRepository));
builder.Services.AddScoped(typeof(ICarPricingRepository), typeof(CarPricingRepository));
builder.Services.AddScoped(typeof(ITagCloudRepository), typeof(TagCloudRepository));
builder.Services.AddScoped(typeof(IRentACarRepository), typeof(RentACarRepository));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(CommentRepository<>));
builder.Services.AddScoped(typeof(ICarFeatureRepository), typeof(CarFeatureRepository));
builder.Services.AddScoped(typeof(ICarDescriptionRepository), typeof(CarDescriptionRepository));
builder.Services.AddScoped(typeof(IReviewRepository), typeof(ReviewRepository));



builder.Services.AddScoped<GetAboutQueryHandler>();
builder.Services.AddScoped<GetAboutByIdQueryHandler>(); 
builder.Services.AddScoped<CreateAboutCommandHandler>();
builder.Services.AddScoped<UpdateAboutCommandHandler>();    
builder.Services.AddScoped<RemoveAboutCommandHandler>();

builder.Services.AddScoped<GetBannerQueryHandler>();
builder.Services.AddScoped<GetBannerByIdQueryHandler>();
builder.Services.AddScoped<CreateBannerCommandHandler>();
builder.Services.AddScoped<UpdateBannerCommandHandler>();
builder.Services.AddScoped<RemoveBannerCommandHandler>();

builder.Services.AddScoped<GetBrandQueryHandler>();
builder.Services.AddScoped<GetBrandByIdQueryHandler>();
builder.Services.AddScoped<CreateBrandCommandHandler>();
builder.Services.AddScoped<UpdateBrandCommandHandler>();
builder.Services.AddScoped<RemoveBrandCommandHandler>();

builder.Services.AddScoped<GetCarQueryHandler>();
builder.Services.AddScoped<GetCarByIdQueryHandler>();
builder.Services.AddScoped<CreateCarCommandHandler>();
builder.Services.AddScoped<UpdateCarCommandHandler>();
builder.Services.AddScoped<RemoveCarCommandHandler>();
builder.Services.AddScoped<GetCarWithBrandQueryHandler>();
builder.Services.AddScoped<GetLast5CarsWithBrandQueryHandler>();


builder.Services.AddScoped<GetCategoryQueryHandler>();
builder.Services.AddScoped<GetCategoryByIdQueryHandler>();
builder.Services.AddScoped<CreateCategoryCommandHandler>();
builder.Services.AddScoped<UpdateCategoryCommandHandler>();
builder.Services.AddScoped<RemoveCategoryCommandHandler>();

builder.Services.AddScoped<GetContactQueryHandler>();
builder.Services.AddScoped<GetContactByIdQueryHandler>();
builder.Services.AddScoped<CreateContactCommandHandler>();
builder.Services.AddScoped<UpdateContactCommandHandler>();
builder.Services.AddScoped<RemoveContactCommandHandler>();

#endregion

//mediatr
builder.Services.AddApplicationService(builder.Configuration); //en b�y�k avantaj kodu k�saltmaya yarar sa�lad�

//validation
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<CarHub>("/carhub");// SignalR ile ger�ek zamanl� haberle�me i�in CarHub s�n�f�n� "/carhub" endpoint'ine map eder.
// Frontend bu URL �zerinden hub'a ba�lanarak anl�k veri al��veri�i yapabilir.

app.Run();

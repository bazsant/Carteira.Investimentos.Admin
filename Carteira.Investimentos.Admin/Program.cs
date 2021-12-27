using AutoMapper;
using Carteira.Investimentos.Admin.Profiles;
using Carteira.Investimentos.Admin.Repositories;
using Carteira.Investimentos.Admin.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Carteira de Investimentos",
        Description = "Solução capaz de gerenciar uma carteira de investimentos "
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// DI
builder.Services.AddScoped<IAcaoRepository, AcaoRepository>();
builder.Services.AddScoped<IAcaoService, AcaoService>();
builder.Services.AddScoped<IOperacaoRepository, OperacaoRepository>();
builder.Services.AddScoped<IOperacaoService, OperacaoService>();

builder.Services
    .AddHttpClient<IAcaoService, AcaoService>(config =>
    {
        config.BaseAddress = new Uri(builder.Configuration.GetValue<string>("YahooFinanceApiUrl"));
        config.DefaultRequestHeaders.Add("X-API-KEY", builder.Configuration.GetValue<string>("YahooFinanceApiKey"));
    });

// AutoMapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AcaoProfile());
    mc.AddProfile(new OperacaoProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

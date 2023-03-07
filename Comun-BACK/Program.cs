/*using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}


app.UseHttpsRedirection();
app.UseCors("corsapp");

app.UseAuthorization();

app.MapControllers();

app.Run();

//######################################################################################################
//IMPORTANTE!: DE AQUÍ PARA ARRIBA ESTÁ EL CÓDIGO ANTERIOR!!! QUE HOY 28/12/2022 ESTÁ EN PRODUCCIÓN.
//######################################################################################################
*/

using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();//###
builder.Services.AddEndpointsApiExplorer();//###
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Cygnus Master API",
        Description = "Detalle de los métodos, valores y respuestas de consumo de la API"
    });
});

var appCors = "appCors";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: appCors, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var services = builder.Services;
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();
                                                       


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();

app.MapGet("/", () => "[TRUE][YES][1]");//Esto es un test
app.MapGet("/sistema/generatoken", () => "No visible!").ExcludeFromDescription();

/*[INICIO PRUEBAS]*/
app.UseCors(appCors);
app.MapControllers();

/*[FIN PRUEBAS]*/

app.UseSwaggerUI();
app.Run();



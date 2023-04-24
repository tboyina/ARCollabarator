using ARCollabator.DAL;
using ARCollabator.DALContracts;
using ARCollabator.Repo;
using ARCollabator.RepoContracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUser, User>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMarker, Marker>();
builder.Services.AddScoped<IMarkerRepository, MarkerRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using CatalogoLivros.Context;
using CatalogoLivros.Repositories.Authors;
using CatalogoLivros.Repositories.Books;
using CatalogoLivros.Services.Authors;
using CatalogoLivros.Services.Books;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBookService, BooksService>();
builder.Services.AddScoped<IBookRepository, BooksRepository>();
builder.Services.AddScoped<IAuthorsService, AuthorsService>();
builder.Services.AddScoped<IAuthorsRepository, AuthorsRepository>();

builder.Services.AddDbContext<AppDbContext>();


var app = builder.Build();

app.UseCors(options =>
{
    options.WithOrigins("http://localhost:3000");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

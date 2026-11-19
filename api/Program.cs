using System.Security.Authentication.ExtendedProtection;
using api.Data;
using api.Repositories;
using api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Register AppDbContext so it can be handed to any class that asks for it.
//UseSqlite means our database is a single file on disk (app.db)
//the connection string "DatabaseConnection" is read from appsettings.json
 builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DatabaseConnection")));

//Registering our repository for dependancy injection
//Whenever a class asks for IMovieRepository give it our MovieRepository
 builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieServices, MovieServices>();

builder.Services.AddScoped<IActorsRepository, ActorsRepository>();
builder.Services.AddScoped<IActorsServices, ActorsServices>();


//CORS stands for Cross-Origin Resources Sharing.
//A Cors policy is our API telling the browser "these origins (front ends) are allowed to call me."
builder.Services.AddCors(Options =>
{
    //Give the policy a name, "Frontend", so we can turn it on below.
    Options.AddPolicy("Frontend", PolicyEnforcement =>
    {
        // the origins that are allowed (any).
        PolicyEnforcement.AllowAnyOrigin()
        //allows any HTTP request headers like Content-Type
        .AllowAnyHeader()
        //allows requests using any Http method - CRUD functions
        .AllowAnyMethod();
    });
});

//EVerything above registers services. Build() creates the app
//Everything below sets up what happens with each request
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// send any http:// request over to https://
app.UseHttpsRedirection();

//Turn on the "Frontend" CORS policy from above.
app.UseCors("Frontend");

//checks [Authorize] attributes. we dont have any yet, but it comes baked in.
app.UseAuthorization();
// connects each controllers routes, like api/movies
app.MapControllers();

app.Run();

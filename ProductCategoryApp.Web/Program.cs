using ProductCategoryApp.Application;
using ProductCategoryApp.Infrastructure;
using ProductCategoryApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Products Categories API",
        Version = "v1",
        Description = "A Clean Architecture API for managing products and categories with CQRS and Domain Events"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Apply migrations and seed data
await EnsureDatabaseAsync(app);

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products Category API v1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at root
    });
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();

static async Task EnsureDatabaseAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await context.Database.MigrateAsync();
    await SeedDataAsync(context);
}

static async Task SeedDataAsync(ApplicationDbContext context)
{
    if (!context.Categories.Any())
    {
        // Create some sample categories
        var electronics = ProductCategoryApp.Domain.Categories.Entities.Category.Create("Electronics");
        var clothing = ProductCategoryApp.Domain.Categories.Entities.Category.Create("Clothing");
        var books = ProductCategoryApp.Domain.Categories.Entities.Category.Create("Books");

        context.Categories.AddRange(electronics, clothing, books);
        await context.SaveChangesAsync();

        // Create some sample products
        var laptop = ProductCategoryApp.Domain.Products.Entities.Product.Create(
            "Gaming Laptop",
            ProductCategoryApp.Domain.Products.ValueObjects.Price.Create(1299.99m, "USD"),
            ProductCategoryApp.Domain.Products.ValueObjects.CategoryId.Create(electronics.Id)
        );

        var tshirt = ProductCategoryApp.Domain.Products.Entities.Product.Create(
            "Cotton T-Shirt",
            ProductCategoryApp.Domain.Products.ValueObjects.Price.Create(29.99m, "USD"),
            ProductCategoryApp.Domain.Products.ValueObjects.CategoryId.Create(clothing.Id)
        );

        var novel = ProductCategoryApp.Domain.Products.Entities.Product.Create(
            "Science Fiction Novel",
            ProductCategoryApp.Domain.Products.ValueObjects.Price.Create(14.99m, "USD"),
            ProductCategoryApp.Domain.Products.ValueObjects.CategoryId.Create(books.Id)
        );

        context.Products.AddRange(laptop, tshirt, novel);
        await context.SaveChangesAsync();
    }
}
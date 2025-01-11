using System;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// use the connection string in the appsettings.json
builder.Services.AddDbContext<BookManagerContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// DI for book services
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookPriceService, BookPriceService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// DI for order services
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// DI for system services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();

//Add controllers to DI container
builder.Services.AddControllers();

// continue the build process
var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.Run();

using Microsoft.EntityFrameworkCore;
using FapSchedule.Models;
using FapSchedule.Services;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddControllersWithViews();
//builder.Services.AddMvc();  //
//builder.Services.AddControllers(); // API
builder.Services.AddRazorPages();
builder.Services.AddScoped<ICSVService, CSVService>();
builder.Services.AddDbContext<FapScheduleContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MyConStr"))
    );
// Tao ra 1 application theo config o tren
var app = builder.Build();

// Config middleware pipleline
app.MapRazorPages();

// Run application len
app.Run();

using helpDesk.Data;
using helpDesk.Repositories;
using helpDesk.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=helpdesk.db"));

builder.Services.AddControllers();
builder.Services.AddScoped<CommentsServices>();
builder.Services.AddScoped<TicketsServices>();
builder.Services.AddScoped<UsersServices>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();

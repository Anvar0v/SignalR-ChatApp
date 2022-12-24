using ChatApp_SignalR_Api.Hubs;
using ChatApp_SignalR_Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
//todo cors
builder.Services.AddCors(cors =>
{
    cors.AddDefaultPolicy(corsPolicy =>
    {
        corsPolicy.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});


//todo dbcontext
builder.Services.AddDbContext<IdentityDbContext>(options =>
{
    options.UseInMemoryDatabase("ChatApp");
});

//todo identityUser
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>();

builder.Services.AddSingleton(typeof(MessageService));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors();
app.MapControllers();

app.MapHub<ChatHub>("/chathub");

app.Run();

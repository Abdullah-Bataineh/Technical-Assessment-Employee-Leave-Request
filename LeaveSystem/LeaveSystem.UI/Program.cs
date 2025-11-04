using LeaveSystem.Appliction.Interfaces.Respositories;
using LeaveSystem.Appliction.Services;
using LeaveSystem.Domain.Entites;
using LeaveSystem.Infrastructure.Data;
using LeaveSystem.Infrastructure.MiddleWare;
using LeaveSystem.Infrastructure.Respositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveSystem.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
               
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            builder.Services.AddScoped<ILeaveRepositories,LeaveRepositories>();
            builder.Services.AddScoped<IUserRespositories,UserRepositories>();
            builder.Services.AddScoped<UserServices>();
            builder.Services.AddScoped<LeaveServices>();
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddHttpContextAccessor();      
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.UseRouting();
            app.UseMiddleware<ExcpetionAndLoggingMiddleWare>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.MapGet("/", async context =>
            {
                var userManager = context.RequestServices.GetRequiredService<UserManager<User>>();

                if (context.User.Identity?.IsAuthenticated ?? false)
                {
                    var userId = context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                    if (!string.IsNullOrEmpty(userId))
                    {
                        var user = await userManager.FindByIdAsync(userId);
                        if (user != null)
                        {
                            context.Session.SetString("FirstName", user.FirstName ?? "");
                            context.Session.SetString("LastName", user.LastName ?? "");
                            context.Session.SetString("EmployeeId", user.Id ?? "");

                            var roles = await userManager.GetRolesAsync(user);
                            context.Session.SetString("Roles", string.Join(",", roles));

                            if (roles.Contains("Employee"))
                                context.Response.Redirect("/Employee/Dashboard");
                            else if (roles.Contains("Manager"))
                                context.Response.Redirect("/Manager/Dashboard");
                            else
                                context.Response.Redirect("/Account/Login"); 
                        }
                        else
                        {
                            context.Response.Redirect("/Account/Login");
                        }
                    }
                    else
                    {
                        context.Response.Redirect("/Account/Login");
                    }
                }
                else
                {
                    context.Response.Redirect("/Account/Login");
                }

                await Task.CompletedTask;
            });

            app.MapRazorPages();

            app.Run();
        }
    }
}

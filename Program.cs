using Microsoft.EntityFrameworkCore;
using Subscription_based_marketing.DataContext;
using Subscription_based_marketing.Interface;
using Subscription_based_marketing.Models;
using Subscription_based_marketing.Models.Adminstrator;
using Subscription_based_marketing.Models.Seller;
using Subscription_based_marketing.Models.Services;
using Subscription_based_marketing.Models.Subscription;
using Subscription_based_marketing.Models.User;
using Subscription_based_marketing.Services;


using URF.Core.Abstractions;
using URF.Core.Abstractions.Trackable;
using URF.Core.EF;
using URF.Core.EF.Trackable;

namespace Subscription_based_marketing;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // Register DbContext
        builder.Services.AddDbContext<SubscriptionDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("dbcs")));

        // Register AutoMapper

        builder.Services.AddAutoMapper(cfg =>
          cfg.AddProfile<MyMappingProfile>(),
                   AppDomain.CurrentDomain.GetAssemblies());



        // Register URF Inbuild Service
        builder.Services.AddScoped<DbContext, SubscriptionDbContext>();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register URF Service
        builder.Services.AddScoped<ITrackableRepository<UserAccount>, TrackableRepository<UserAccount>>();
        builder.Services.AddScoped<ITrackableRepository<AdminstratorAccount>, TrackableRepository<AdminstratorAccount>>();
        builder.Services.AddScoped<ITrackableRepository<SellerAccount>, TrackableRepository<SellerAccount>>();
        builder.Services.AddScoped<ITrackableRepository<ServiceDetail>, TrackableRepository<ServiceDetail>>();
        builder.Services.AddScoped<ITrackableRepository<SubscriptionDetails>, TrackableRepository<SubscriptionDetails>>();

        // Register Services
        builder.Services.AddScoped<IUserAccountService, UserAccountService>();
        builder.Services.AddScoped<IAdminService, AdminService>();
        builder.Services.AddScoped<ISellerAccountService, SellerAccountService>();
        builder.Services.AddScoped<IServiceForAllAccount, ServiceForAllAccount>();
        builder.Services.AddScoped<IServiceListService, ServiceListService>();
        builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

        // Add session and memory cache
        builder.Services.AddSession();
        builder.Services.AddMemoryCache();
        var app = builder.Build();

        // Configure the HTTP request pipeline.

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseSession();

        app.UseAuthorization();


        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }


}
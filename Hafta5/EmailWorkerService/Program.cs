using EmailWorkerService;
using EmailWorkerService.Service;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using WebAPI.Context;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostcontext, services) =>
    {
        services.AddHangfire(conf =>
        {
            conf.UseSqlServerStorage(hostcontext.Configuration.GetConnectionString("Default"));
        });
        services.AddHangfireServer();
        services.AddSingleton<IMailService, MailService>();
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(hostcontext.Configuration.GetConnectionString("Default"));
        }, ServiceLifetime.Singleton);
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
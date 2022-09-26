using EG.Bot.Schedules;
using EG.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) => { })
                .ConfigureServices(services =>
                {
                    services.Configure<ConsoleLifetimeOptions>(opts => opts.SuppressStatusMessages = true);

                    new DependencyInjectionConfig().Config(services);
                    new AutoMapperConfig().Config(services);

                    services.AddScoped<IGetTransactionSchedule, GetTransactionSchedule>();

                    var serviceProvider = services.BuildServiceProvider();
                    var getTransactionScheduleService = serviceProvider.GetRequiredService<IGetTransactionSchedule>();

                    getTransactionScheduleService.Start().GetAwaiter().GetResult();
                });

var buildHost = host.Build();
buildHost.Run();
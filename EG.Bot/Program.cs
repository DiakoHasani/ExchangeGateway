using EG.Bot.Schedules;
using EG.Config;
using EG.Repository.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var connectionString = "";
var hostBuilder = new HostBuilder().ConfigureHostConfiguration(config =>
        {
            config.AddEnvironmentVariables();

            if (args != null)
            {
                config.AddCommandLine(args);
            }
        }).ConfigureAppConfiguration((context, builder) =>
        {
            var env = context.HostingEnvironment;
            builder.SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            // Override config by env, using like Logging:Level or Logging__Level
            .AddEnvironmentVariables();

            var configuration = builder.Build();
            connectionString = configuration["ConnectionStrings:defaultConnection"];
        }).ConfigureServices(services =>
        {
            services.Configure<ConsoleLifetimeOptions>(opts => opts.SuppressStatusMessages = true);

            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
            new DependencyInjectionConfig().Config(services);
            new AutoMapperConfig().Config(services);

            services.AddScoped<IGetTransactionSchedule, GetTransactionSchedule>();

            var serviceProvider = services.BuildServiceProvider();
            var getTransactionScheduleService = serviceProvider.GetRequiredService<IGetTransactionSchedule>();

            getTransactionScheduleService.Start().GetAwaiter().GetResult();
        });

var buildHost = hostBuilder.Build();
buildHost.Run();
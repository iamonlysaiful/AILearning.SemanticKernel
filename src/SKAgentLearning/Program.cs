// Load configuration from appsettings.json
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using SKAgentLearning.RoadmapSteps;

var config = new ConfigurationBuilder()
    .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"), optional: false, reloadOnChange: true)
    .Build();

// # Setup 
// Use KernelConfigurator to setup kernelBuilder
var kernelConfigurator = new HelloKernel.KernelConfigurator(config);
var kernelBuilder = kernelConfigurator.ConfigureKernelBuilder();

// Configure logging (IKernelBuilder has no WithLoggerFactory extension)
kernelBuilder.Services.AddLogging(b => b.AddConsole().SetMinimumLevel(LogLevel.Debug));
var kernel = kernelBuilder.Build();

// # [Section 1]: HelloAgent
//await HelloAgent.RunAsync(kernel);

// # [Section 2]: Plugins
//await Plugins.RunAsync(kernel);

// # [Section 3]: Plugins with AutoInvoke
await Plugins_AutoInvoke.RunAsync(kernel);
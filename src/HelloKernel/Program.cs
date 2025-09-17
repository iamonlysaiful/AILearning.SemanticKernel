using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.Extensions.Logging;
using HelloKernel.RoadmapSteps;
using Microsoft.Extensions.Configuration;


// Load configuration from appsettings.json
var config = new ConfigurationBuilder()
    .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"), optional: false, reloadOnChange: true)
    .Build();

// # Setup 
// Use KernelConfigurator to setup kernelBuilder
var kernelConfigurator = new HelloKernel.KernelConfigurator(config);
var kernelBuilder = kernelConfigurator.ConfigureKernelBuilder();

// Configure logging (IKernelBuilder has no WithLoggerFactory extension)
kernelBuilder.Services.AddLogging(b => b.AddConsole().SetMinimumLevel(LogLevel.Information));
var kernel = kernelBuilder.Build();


// # [Section 1]: HelloKernel
await HelloKernelSection.RunAsync(kernel);

// # [Section 2]: Chat completions (prompting patterns)
// prompt only
//await ChatCompletion_PromptingPattern.RunAsync(kernel);

//instruction + prompt
//await ChatCompletion_PromptingPatternWithInstruction.RunAsync(kernel);

//instruction + Prompt + options
//await ChatCompletion_PromptingPatternWithInstructionNOptions.RunAsync(kernel);

// # [Section 3]: plugins Native & Semantic
// ## native Plugin (C# method)
//await Plugins_Native.RunAsync(kernel);

// ## semantic plugin (Prompt template)
//await Plugins_Semantic.RunAsync(kernel);

// # [Section 4]: Tool / Function Calling (auto-invoke)
//await ToolRFunction_AutoInvoke.RunAsync(kernel);

// # [Section 5]: memory & embeddings (Vector Stores)
// ## In-memory vector store- Fast access to recent context, but limited capacity.
//await MemoryNEmbedding_InMemoryVectorStore.RunAsync(kernel);

// ## Persistent Memory with Postgres + pgvector
//await MemoryNEmbedding_PersistentMemoryPGVector.RunAsync(kernel);

// # [Section 6]: planning/orchestration patterns
// A plan/ orchestration is a sequence of steps (LLM + plugins + memory). Instead of writing if/else logic yourself, you can let the planner decide. Two main strategies in SK: [1]. Handcrafted orchestration (you decide how memory + functions combine). [2]. AI-generated plan (you give a goal, planner composes steps).
// ## Handcrafted orchestration
//await PlanningROrchestration_Handcrafted.RunAsync(kernel);

// ## AI-generated plan - Sequential
//await PlanningROrchestration_AIGenerated.RunAsync(kernel);

// # [Section 7]: File I/O, HTTP & Connector patterns
//await FileIO_HTTP_Connector_WithToolCalling.RunAsync(kernel);





return 0;


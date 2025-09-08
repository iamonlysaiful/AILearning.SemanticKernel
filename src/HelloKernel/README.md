# HelloKernel Project

This project demonstrates the core capabilities of Microsoft Semantic Kernel (SK) using a modular, best-practice C# solution structure. Each major feature of SK is showcased in a dedicated code section, making it easy to learn, extend, and adapt for your own AI applications.

## Project Structure
- `Program.cs`: Entry point, orchestrates all SK demonstrations.
- `Configuration/KernelConfigurator.cs`: Loads settings from `appsettings.json` and configures the kernel.
- `RoadmapSteps/`: Contains code samples for each SK feature area.
- `Plugins/`: Example plugins (native C# and semantic prompt templates).
- `Vector/`: Vector store and memory examples.
- `appsettings.json`: Centralized configuration for model endpoints, keys, etc.

## Demonstrated Features

### 1. Setup & “Hello, Kernel”
- Loads configuration from `appsettings.json`.
- Uses `KernelConfigurator` to set up the kernel with local or remote LLM endpoints (e.g., LM Studio).
- Runs a basic "Hello, Kernel" example to verify setup.

### 2. Chat Completions (Prompting Patterns)
- Shows how to use SK for chat completions with various prompting strategies:
  - Simple prompt
  - Instruction + prompt
  - Instruction + prompt + options
- See: `RoadmapSteps/ChatCompletion_PromptingPattern*.cs`

### 3. Plugins: Native & Semantic
- **Native Plugins**: C# methods exposed as callable functions in SK.
- **Semantic Plugins**: Prompt templates for LLMs, enabling flexible, reusable AI behaviors.
- See: `Plugins/` and `RoadmapSteps/Plugins_Native.cs`, `Plugins_Semantic.cs`

### 4. Tool/Function Calling (Auto-Invoke)
- Demonstrates SK's ability to automatically invoke tools/functions based on LLM output.
- See: `RoadmapSteps/ToolRFunction_AutoInvoke.cs`

### 5. Memory & Embeddings (Vector Stores)
- **In-Memory Vector Store**: Fast, ephemeral context storage for embeddings.
- **Persistent Memory (PGVector)**: Durable, scalable vector storage using Postgres.
- See: `Vector/` and `RoadmapSteps/MemoryNEmbedding_InMemoryVectorStore.cs`, `MemoryNEmbedding_PersistentMemoryPGVector.cs`

### 6. Planning/Orchestration Patterns
- **Handcrafted Orchestration**: Manually compose memory, plugins, and LLM calls.
- **AI-Generated Plans**: Let SK's planner compose steps to achieve a goal (deprecated in favor of new function calling patterns).
- See: `RoadmapSteps/PlanningROrchestration_Handcrafted.cs`, `PlanningROrchestration_AIGenerated.cs`

### 7. File I/O, HTTP, and Connector Patterns Using Function Calling
- Demonstrates how SK can interact with external systems (files, HTTP APIs, connectors) via function calling.
- See: `Plugins/Connector.cs`, `Plugins/FileIO.cs`, `Plugins/HTTP.cs`, and `RoadmapSteps/FileIO_HTTP_Connector_WithToolCalling.cs`

## Getting Started
1. Clone the repo and open in Visual Studio or VS Code.
2. Ensure `appsettings.json` is configured for your local or remote LLM endpoint.
3. Build and run the project. Each section in `Program.cs` can be enabled/disabled to test specific features.

## Requirements
- .NET 9.0+
- Microsoft.SemanticKernel and related NuGet packages
- (Optional) LM Studio or compatible OpenAI endpoint for local LLMs
- (Optional) Postgres with PGVector for persistent memory

## Customization
- Add your own plugins to the `Plugins/` folder.
- Extend or modify prompt templates for semantic plugins.
- Update `appsettings.json` to point to different models or endpoints.

## License
MIT

---

This project is a practical, hands-on guide to using Semantic Kernel for real-world AI orchestration in C#. Explore each section to learn how SK can power your next intelligent application.

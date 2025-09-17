
# SKAgentLearning Project

This project demonstrates agent-based orchestration and advanced usage patterns with Microsoft Semantic Kernel (SK), using a modular, best-practice C# solution structure. Each major feature is showcased in a dedicated code section, making it easy to learn, extend, and adapt for your own AI agent applications.

## Project Structure
- `Program.cs`: Entry point, orchestrates all agent-based SK demonstrations.
- `Configuration/KernelConfigurator.cs`: Loads settings from `appsettings.json` and configures the kernel.
- `RoadmapSteps/`: Contains code samples for each agent and plugin feature area.
- **Native Plugins:** C# plugins are referenced from `../KernelInfrastructure/Plugins/` (e.g., `MathPluginNative.cs`).
- **Semantic Plugins:** Prompt template plugins (like Summarizer) are located in `../KernelInfrastructure/Plugins/SummarizerPlugin/Summarize/`.
- `appsettings.json`: Centralized configuration for model endpoints, keys, etc.

## Demonstrated Features

### 1. Setup & “Hello, Agent”
- Loads configuration from `appsettings.json`.
- Uses `KernelConfigurator` to set up the kernel with local or remote LLM endpoints (e.g., LM Studio).
- Runs a basic "Hello, Agent" example to verify setup (`RoadmapSteps/1_HelloAgent.cs`).

### 2. Extending Agents with Tools & Plugins
- Shows how to extend agents with native and semantic plugins, including auto-invocation of tools/functions.
- See: `RoadmapSteps/2_ExtendingAgentWithToolNPlugins.cs`, `3_ExtendingAgentWithToolNPlugins_AutoInvoke.cs`

### 3. Plugins: Native & Semantic
- **Native Plugins**: C# methods exposed as callable functions in SK, implemented in `../KernelInfrastructure/Plugins/`.
- **Semantic Plugins**: Prompt templates for LLMs, enabling flexible, reusable AI behaviors. Example: Summarizer plugin in `../KernelInfrastructure/Plugins/SummarizerPlugin/Summarize/`.

### 4. Memory & Embeddings
- Demonstrates use of memory and embeddings for agent context and recall.

## Getting Started
1. Clone the repo and open in Visual Studio or VS Code.
2. Ensure `appsettings.json` is configured for your local or remote LLM endpoint.
3. Build and run the project:
   ```sh
   dotnet run --project src/SKAgentLearning/SKAgentLearning.csproj
   ```
4. Each section in `Program.cs` can be enabled/disabled to test specific features.

## Requirements
- .NET 9.0+
- Microsoft.SemanticKernel and related NuGet packages
- (Optional) LM Studio or compatible OpenAI endpoint for local LLMs
- (Optional) Postgres with PGVector for persistent memory

## Customization
- Add your own native plugins to `../KernelInfrastructure/Plugins/` and semantic plugins to a subfolder (e.g., `KernelInfrastructure/Plugins/YourPluginName/`).
- Extend or modify prompt templates for semantic plugins.
- Update `appsettings.json` to point to different models or endpoints.

## License
MIT

---

This project is a practical, hands-on guide to using Semantic Kernel for real-world AI agent orchestration in C#. Explore each section to learn how SK can power your next intelligent agent application.

**Note:**
- When referencing semantic plugins in code, use the correct relative path from your project root, e.g., `../KernelInfrastructure/Plugins/SummarizerPlugin/Summarize/` for the Summarizer plugin.

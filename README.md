# AILearning.SemanticKernel

This repository demonstrates advanced usage of Microsoft Semantic Kernel (SK) in C# for AI orchestration, plugin development, memory/embedding, and planning. It is organized into modular projects for learning and experimentation.

## Projects

- **HelloKernel**: Core SK features, plugin usage, memory, and orchestration patterns. Great starting point for learning SK basics.
- **SKAgentLearning**: Agent-based patterns and advanced orchestration using SK agents.
- **KernelInfrastructure**: Shared plugins (native and semantic), vector store, and infrastructure code for use across projects.

## Directory Structure

- `src/HelloKernel/` — Main learning project for SK features
- `src/SKAgentLearning/` — Agent-based learning and orchestration
- `src/KernelInfrastructure/` — Shared plugins and vector/memory infrastructure
- `tests/` — (If present) Unit and integration tests

## Getting Started

1. Clone the repository and open in Visual Studio or VS Code.
2. Review each project's `README.md` for setup and usage instructions.
3. Configure `appsettings.json` in each project for your LLM endpoints and API keys.
4. Build and run the desired project.

## Requirements

- .NET 9.0+
- Microsoft.SemanticKernel and related NuGet packages
- (Optional) LM Studio or compatible OpenAI endpoint for local LLMs
- (Optional) Postgres with PGVector for persistent memory

## License
MIT

---

Explore each project to learn how Semantic Kernel can power your next intelligent application!
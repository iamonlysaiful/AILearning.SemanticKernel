# KernelInfrastructure

This project contains shared infrastructure for Semantic Kernel learning projects, including:

- **Native Plugins** (C#): e.g., Math, FileIO, HTTP, Connector
- **Semantic Plugins** (Prompt templates): e.g., Summarizer (see `Plugins/SummarizerPlugin/Summarize/`)
- **Vector Store**: In-memory and persistent memory (PGVector) support

## Structure
- `Plugins/`: All native and semantic plugins for use in other projects
- `Vector/`: Vector store and memory code

## Usage
- Reference this project from `HelloKernel` or `SKAgentLearning` for shared plugins and infrastructure.
- Add new plugins or vector store implementations here for reuse.

---

See the main repo `README.md` for overall project context and setup.

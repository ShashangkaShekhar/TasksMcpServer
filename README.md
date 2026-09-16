# Tasks Mcp Server(.NET)

A minimal [Model Context Protocol (MCP)](https://modelcontextprotocol.io/) server built with **.NET 10** and `ModelContextProtocol.AspNetCore` 2.2.0. Exposes a simple in-memory task list over **streamable HTTP** at `/mcp`, plus a `/health` endpoint for hosting checks.

## Stack

- .NET 10 (`Microsoft.NET.Sdk.Web`)
- `ModelContextProtocol.AspNetCore` 2.2.0
- Stateless HTTP transport (`Stateless = true`)
- Open CORS policy (for clients like GitHub Copilot in VS Code)
- Singleton in-memory `TaskStore`, immutable `TaskItem` record with copy-on-write

## Project structure

| File | Purpose |
|---|---|
| `Program.cs` | Wire-up: `AddMcpServer().WithHttpTransport().WithToolsFromAssembly()`, CORS, `TaskStore` singleton, `MapMcp("/mcp")`, `MapGet("/health")` |
| `TasksMcpTools.cs` | Thin MCP tool adapter over `TaskStore`. All tools + params have `Description`. Never throws for missing ID |
| `TaskStore.cs` | In-memory store. `TaskItem(int Id, string Title, string Description, bool IsComplete, DateTime CreatedAt)` + `GetAll / GetById / Create / ToggleComplete / Delete` |
| `TasksMcpServer.csproj` | Web SDK, `net10.0`, MCP package reference |
| `TasksMcpServer.slnx` | Solution referencing the project |
| `Properties/launchSettings.json` | `http` → `http://localhost:5089`, `https` → `https://localhost:7228;http://localhost:5089` |
| `appsettings.json` | Base logging config |

## MCP tools

| Tool | Description | Args | Returns |
|---|---|---|---|
| `ListTasks` | Lists all tasks with ID, title, description, completion status | — | `List<TaskItem>` |
| `GetTask` | Gets a single task by ID | `id: int` | `TaskItem?` (`null` if not found) |
| `CreateTask` | Creates a new task, returns the created task | `title: string`, `description: string` | `TaskItem` |
| `ToggleTaskComplete` | Toggles completion status | `id: int` | `"Task {id} is now complete/incomplete."` or `"Task with ID {id} not found."` |
| `DeleteTask` | Deletes a task by ID | `id: int` | `"Task {id} deleted."` or `"Task with ID {id} not found."` |

Seed data on startup: `1/Buy groceries` (incomplete), `2/Write docs` (complete).

## Endpoints

- `GET /health` → `200 "healthy"` (container / readiness probe)
- `POST /mcp` → MCP streamable HTTP endpoint

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Getting started

```powershell
dotnet build
dotnet run
```

Then verify:

```powershell
curl http://localhost:5089/health
# healthy
```

Or open `http://localhost:5089/health` in a browser (the `http` launch profile uses port `5089`).

## Connect a client

### MCP Inspector

```powershell
npx @modelcontextprotocol/inspector
```

- Transport: `Streamable HTTP`
- URL: `http://localhost:5089/mcp`

### VS Code (`mcp.json`)

```json
{
  "servers": {
    "tasks": {
      "type": "http",
      "url": "http://localhost:5089/mcp"
    }
  }
}
```

## Design rules (see `AGENTS.md`)

- Keep `Stateless = true`, open CORS, `/mcp` + `/health`.
- `TasksMcpTools` stays a thin adapter; all tools + params need `Description`.
- `TaskStore` stays a singleton with immutable `TaskItem` record + copy-on-write (`with`).
- Never throw for missing ID — return `null` / `"... not found."`.
- Verify with `dotnet build`, `dotnet run` + `GET /health`, MCP Inspector.

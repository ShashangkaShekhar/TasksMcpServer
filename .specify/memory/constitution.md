# Project Constitution — TasksMcpServer

> Spec-Driven Development source of truth. All specs, plans, and tasks MUST
> comply with this document. Amend via PR + version bump only.

## 1. Identity & Mission

**TasksMcpServer** is a lightweight **Model Context Protocol (MCP) server**
exposing task management over **streamable HTTP** for AI agents.

- Stack: **C# / .NET 10 (`net10.0`)**, `Microsoft.NET.Sdk.Web`
- MCP SDK: **`ModelContextProtocol.AspNetCore` v2.2.0**
- Hosting: minimal API, `POST /mcp`, health `GET /health`
- Transport: **stateless HTTP** (`Stateless = true`)
- Tools: `AddMcpServer().WithHttpTransport().WithToolsFromAssembly()`

## 2. Non-Negotiable Principles

1. **Spec-first:** No production code without `specs/<id>-<slug>/spec.md`.
2. **Stateless server:** Keep `Stateless = true`. Only singleton allowed
   is the approved `TaskStore`.
3. **Minimal API surface:** New MCP tools need justification in spec.
4. **Type safety:** `Nullable enable`, `ImplicitUsings enable`. Return
   `TaskItem?` where not-found is possible; human-readable `string`
   confirmations for toggle/delete.
5. **Observability:** `/health` returns `healthy`. Preserve CORS default
   policy (`AllowAnyOrigin/Header/Method`) for agent clients.
6. **No secrets:** `appsettings*.json` holds logging + `AllowedHosts` only.
7. **Simplicity:** In-memory `List<TaskItem>` is intentional. DB/auth are
   out-of-scope until a spec approves them.

## 3. Architecture Constraints

```text
Client (Copilot / MCP Inspector) -> POST /mcp
Program.cs — CORS, DI, MapMcp("/mcp"), MapGet("/health")
  ├── TasksMcpTools [McpServerToolType] — 5 tools
  └── TaskStore (Singleton) — List<TaskItem>, _nextId
```

## 4. Tool Design Rules

- Naming: Verb + noun (`ListTasks`, `GetTask`, `CreateTask`, ...).
- Every class `[McpServerToolType]`; every method `[McpServerTool,
  Description("...")]`; every param `[Description("...")]`.
- Params: primitives only (`int`, `string`). Outputs may be
  `TaskItem` / `List<TaskItem>`.
- Errors: return `null` (get) or `"... not found."` (toggle/delete).
  Never throw for missing ID.
- `Description` text is user-facing: imperative, concise.

## 5. Data Rules

- `TaskItem(int Id, string Title, string Description, bool IsComplete,
  DateTime CreatedAt)` — immutable record, `CreatedAt = UtcNow`.
- Seed rows IDs 1-2, `_nextId = 3`. Preserve unless spec migrates it.
- `Create`: auto-increment ID, `IsComplete = false`.
- `ToggleComplete`: copy-on-write (`old with { ... }`).
- `Delete`: lookup then remove, return `bool`.

## 6. Workflow (normative)

```text
constitution -> specify -> clarify -> plan -> tasks -> implement
```

- `specs/<3-digit-id>-<slug>/spec.md` — WHAT + WHY.
- `plan.md` — HOW (files, risks). `tasks.md` — checklist `T001`...
- `checklists/requirements.md` — quality gate before coding.
- Link spec ID in commit/PR. No direct `main` edits for features.

```text
.specify/memory/constitution.md
.specify/templates/spec|plan|tasks|checklist-template.md
specs/001-<slug>/spec|plan|tasks.md + checklists/requirements.md
```

## 7. Quality Gates

- [ ] `dotnet build` passes; `GET /health` = `healthy`.
- [ ] MCP Inspector can call all 5 tools.
- [ ] New tool has `Description` on method + params.
- [ ] Acceptance criteria checked in `checklists/requirements.md`.
- [ ] No change to `Stateless`, CORS, `/mcp`, DI lifetime w/o rationale.

## 8. Out of Scope

Persistence (EF/SQLite), auth, multi-user isolation, paging/filtering,
validation lib, OpenTelemetry, Docker/K8s, CI. Each needs its own spec.

## 9. Versioning

- **v1.0.0** (2026-09-15) — initial adoption.
- SemVer: MAJOR = principle change, MINOR = new rule, PATCH = wording.

**Last amended:** 2026-09-15 | **Ratified by:** repo owner


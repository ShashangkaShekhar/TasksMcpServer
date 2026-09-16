# Spec 001 — Baseline TasksMcpServer

## Metadata

- **Spec ID:** `001-baseline-tasks-mcp-server`
- **Status:** Approved
- **Author:** repo owner
- **Date:** 2026-09-15
- **Constitution:** v1.0.0


## Overview

Document the as-built MCP task server so future changes have a baseline.
AI agents manage a tiny in-memory task list over streamable HTTP.

## Goals / Non-Goals

- Goals:
  - [x] G1 Describe 5 tools, hosting, data rules, gates.
- Non-Goals:
  - NG1 No behavior change in this spec.

## User Stories

### US1 — Manage tasks via agent (P1)

_As an_ agent _I want_ list/get/create/toggle/delete _so that_ I can
manage user tasks.

**Acceptance:**
- [x] Given server running When Inspector calls each tool Then
  results match Functional Requirements.

## Functional Requirements

- [x] FR-001: `ListTasks()` returns all tasks.
- [x] FR-002: `GetTask(id)` returns one task or `null`.
- [x] FR-003: `CreateTask(title, desc)` assigns ID, `IsComplete=false`.
- [x] FR-004: `ToggleTaskComplete(id)` flips flag or not-found message.
- [x] FR-005: `DeleteTask(id)` removes or not-found message.

## Non-Functional Requirements

- [x] NFR-001 Stateless HTTP, CORS open, `/health`=healthy, .NET 10.

## API / Tool Contracts

| Tool | Input | Output | Errors |
|------|-------|--------|--------|
| `ListTasks` | — | `List<TaskItem>` | — |
| `GetTask` | `id: int` | `TaskItem?` | `null` |
| `CreateTask` | `title, description` | `TaskItem` | — |
| `ToggleTaskComplete` | `id: int` | `string` | not found msg |
| `DeleteTask` | `id: int` | `string` | not found msg |

## Data & Edge Cases

- Seed: (1, Buy groceries, false), (2, Write docs, true); `_nextId=3`.
- Missing ID never throws.

## Acceptance Criteria

- [x] AC-1 `dotnet build` passes.
- [x] AC-2 `GET /health` returns `healthy`.
- [x] AC-3 All 5 tools callable via `/mcp`.

## Open Questions

- None.

# Feature Specification Template

> Copy to `specs/<3-digit-id>-<kebab-slug>/spec.md`. Fill all sections.

## Metadata

- **Spec ID:** `XXX-slug`
- **Status:** Draft | Clarifying | Approved | Implementing | Done
- **Author:** _name_
- **Date:** YYYY-MM-DD
- **Constitution:** v1.0.0 (`.specify/memory/constitution.md`)

## Overview

_1-3 sentences: problem, who benefits, outcome._

## Goals / Non-Goals

- Goals:
  - [ ] G1 ...
- Non-Goals:
  - NG1 ... (explicitly out of scope)

## User Stories

### US1 — _Title_ (P1)

_As a_ ... _I want_ ... _so that_ ...

**Acceptance:**
- [ ] Given ... When ... Then ...

## Functional Requirements

- [ ] FR-001: ...
- [ ] FR-002: ...

## Non-Functional Requirements

- [ ] NFR-001 (perf, compat .NET 10, stateless, CORS, docs): ...

## API / Tool Contracts

| Tool | Input | Output | Errors |
|------|-------|--------|--------|
| `ToolName` | `param: type — desc` | `return type` | `not-found → ...` |

Rules: `[McpServerTool, Description]` on all methods + params;
primitives in, `TaskItem` out; never throw for missing ID.

## Data & Edge Cases

- Data changes: ...
- Edge cases:
  - Missing ID → `null` / `"... not found."`
  - Empty title/desc → ...

## Acceptance Criteria (global)

- [ ] AC-1 `dotnet build` passes
- [ ] AC-2 `GET /health` = `healthy`
- [ ] AC-3 MCP Inspector exercises new/changed tools
- [ ] AC-4 Descriptions present on all new tools/params

## Open Questions

- [ ] Q1 ... (resolve in clarify phase)

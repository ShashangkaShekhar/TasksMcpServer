# AGENTS.md — Spec-Driven Development

Read `.specify/memory/constitution.md` first. It outranks this file.

## Workflow

1. **Specify:** copy `.specify/templates/spec-template.md` to
   `specs/<3-digit-id>-<slug>/spec.md`. Fill goals, stories, FRs, ACs.
2. **Clarify:** resolve `Open Questions`; mark Status `Approved`.
3. **Plan:** copy `plan-template.md` to `plan.md` in same folder.
4. **Tasks:** copy `tasks-template.md` to `tasks.md` (`T001...`).
5. **Gate:** copy `checklist-template.md` to `checklists/requirements.md`.
   All boxes checked before coding.
6. **Implement:** code against tasks; reference spec ID in commits.

## Rules for this repo

- Stack: .NET 10, `ModelContextProtocol.AspNetCore` 2.2.0.
- Keep `Stateless = true`, CORS open policy, `/mcp`, `/health`.
- `TasksMcpTools`: thin adapter, `Description` on all tools + params.
- `TaskStore`: singleton, immutable `TaskItem` record, copy-on-write.
- Never throw for missing ID; return `null` / `"... not found."`.
- Verify: `dotnet build`, `dotnet run` + `GET /health`, MCP Inspector.

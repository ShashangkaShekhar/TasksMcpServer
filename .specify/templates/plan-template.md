# Implementation Plan Template

> Copy to `specs/<id>-<slug>/plan.md`. One plan per spec.

## Metadata

- **Spec:** `specs/XXX-slug/spec.md`
- **Status:** Draft | Approved
- **Author / Date:** ...

## 1. Approach

_How the spec will be implemented in 3-6 bullets._

## 2. Architecture & Files

| File | Change | Notes |
|------|--------|-------|
| `Program.cs` | _none / ..._ | Keep CORS, `Stateless`, `/mcp`, `/health` |
| `TasksMcpTools.cs` | ... | Keep attribute rules |
| `TaskStore.cs` | ... | Keep record + copy-on-write |

## 3. Design Decisions

- D1 ... (with alternatives considered)

## 4. Risks & Mitigations

| Risk | Impact | Mitigation |
|------|--------|------------|
| ... | ... | ... |

## 5. Test / Verification

- [ ] `dotnet build`
- [ ] `dotnet run` → `GET /health`
- [ ] MCP Inspector: call ... expect ...
- [ ] Regression: existing 5 tools still work

## 6. Rollout / Rollback

- Rollout: ...
- Rollback: revert commit(s) for spec ID ...

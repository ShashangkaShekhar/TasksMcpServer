# Plan — 001 Baseline TasksMcpServer

## Metadata

- **Spec:** `spec.md`
- **Status:** Approved
- **Author / Date:** repo owner / 2026-09-15

## 1. Approach

- Baseline only; no code change. Record as-built behavior.

## 2. Architecture & Files

| File | Change | Notes |
|------|--------|-------|
| `Program.cs` | none | CORS, Stateless, `/mcp`, `/health` stay |
| `TasksMcpTools.cs` | none | 5 tools documented |
| `TaskStore.cs` | none | record + seed documented |

## 3. Design Decisions

- D1 In-memory store kept for simplicity.

## 4. Risks & Mitigations

| Risk | Impact | Mitigation |
|------|--------|------------|
| Drift | Low | Gate future specs on this baseline |

## 5. Test / Verification

- [x] `dotnet build`
- [x] `GET /health` = healthy
- [x] Inspector: 5 tools callable

## 6. Rollout / Rollback

- N/A (docs only).

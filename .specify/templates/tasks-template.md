# Tasks Template

> Copy to `specs/<id>-<slug>/tasks.md`. Keep ordered, small, testable.

## Metadata

- **Spec:** `../spec.md` | **Plan:** `../plan.md`

## Tasks

- [ ] T001 _Title_ — `path/to/File.cs`
  - Do: ...
  - Done when: ...
- [ ] T002 _Title_ — `path/to/File.cs`
  - Do: ...
  - Done when: ...
- [ ] T003 Build + verify (`dotnet build`, `/health`, Inspector)
  - Done when: all acceptance criteria pass

## Rules

- IDs sequential `T001...`; one file focus per task.
- Mark `[x]` only when "Done when" holds.
- Reference spec FR/AC numbers (e.g., closes FR-001).

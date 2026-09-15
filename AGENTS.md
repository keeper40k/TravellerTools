# RPGTools Agent Instructions

## Project purpose

TravellerTools is a intended to be a suite of .NET 10 C# tools and libraries 
providing reusable software components for role-playing game applications.

Prefer Traveller RPG- and dice-centric terminology where it makes the API clearer.
Avoid unnecessarily generic infrastructure terminology when a well-understood
Traveller RPG domain term exists.

Design public APIs as reusable library APIs rather than for a single game or
application unless explicitly instructed otherwise.

## C# and .NET coding standards

Write idiomatic modern C# compatible with .NET 10.

Code should conform to the current publicly documented Microsoft C# and .NET
coding conventions and .NET Framework Design Guidelines where applicable,
particularly for public library APIs.

Use the Microsoft C# coding conventions, .NET naming guidelines, and .NET
code-analysis/style guidance as the baseline when no repository-specific rule
overrides them.

In particular:

- Use clear, descriptive names and prefer clarity over brevity.
- Use PascalCase for types and public members.
- Prefix interface names with `I`.
- Use camelCase for parameters and local variables.
- Follow normal .NET conventions for generic type parameter naming.
- Follow established .NET conventions for exception types and argument
  validation.
- Prefer appropriate modern C# language features where they improve clarity,
  safety, or immutability.
- Do not use newer language features merely for novelty when a simpler
  implementation is clearer.
- Preserve type safety; do not fall back to `object` or `dynamic` when a
  sensible generic or strongly typed design is available.
- Prefer explicit static types when the type provides useful information to the 
  reader. Where the type is already known, prefer target-typed new() (for example,
  DiceRoller roller = new();) rather than replacing the explicit type with var. 
  Use var where it improves readability, particularly when the type is obvious 
  from context, cumbersome to express, or cannot conveniently be named (such as 
  anonymous types). Avoid var when it obscures a useful domain type or abstraction.
- Preserve type safety; do not fall back to `object` or `dynamic` when a
  sensible generic or strongly typed design is available.
- Prefer immutable public data structures where practical.
- Avoid unnecessary dependencies.

Repository `.editorconfig` rules take precedence over general style guidance.
Follow `.gitattributes` and repository line-ending conventions.

When existing code differs from a general convention, do not perform unrelated
large-scale style refactoring as part of another change. Maintain consistency
with the surrounding code unless explicitly asked to modernise it.

## XML documentation

Maintain XML documentation comments (`///`) for the public API.

When creating or modifying public classes, records, structs, interfaces,
constructors, methods, properties, enums, or other public members:

- Add appropriate XML documentation to new public APIs.
- Update existing XML documentation whenever behaviour, semantics, parameters,
  return values, exceptions, or constraints change.
- Document the purpose and contract of an API rather than merely restating its
  C# declaration.
- Add `<param>`, `<returns>`, `<typeparam>`, `<exception>`, `<value>`,
  `<remarks>`, or related XML elements where they provide useful information.
- Document validation rules and important invariants that callers need to know.
- Preserve useful existing documentation.

Non-public implementation code should also have comments even if only merely for completeness.

Add ordinary comments where they explain non-obvious algorithms, domain rules,
design decisions, constraints, or reasons for an implementation choice.

Do not add comments that simply translate self-explanatory C# into English.

## RPG domain design

Model RPG concepts explicitly where doing so improves the API.

For dice-related functionality:

- Use conventional RPG dice terminology and notation where appropriate.
- Preserve the distinction between an individual die roll, a dice expression,
  the individual rolls used to resolve an expression, and the resulting summed
  value.
- Treat dice-expression modifiers such as `2d6+1` as unsupported unless a task
  explicitly introduces that feature.
- Use integer 100 internally for a d100 result of 100. Human-facing `00`
  notation, if supported, should be translated at an input/parsing boundary
  rather than represented internally as zero.
- Preserve the existing `IRandomSource` abstraction as the randomness seam
  unless explicitly instructed to redesign it.
- Designs should remain compatible with future deterministic/seeded random
  sources.

## Testing

Production behaviour changes should have appropriate automated tests.

Use MSTest for RPGTools automated tests unless explicitly instructed otherwise.

The solution currently distinguishes between:

- unit tests, for detailed behaviour, boundaries, validation, invariants and
  implementation contracts;
- functional tests, for representative end-to-end verification that realistic
  RPG use cases work as intended.

Do not attempt exhaustive permutation testing in functional tests when the
underlying behaviour is more appropriately and efficiently verified by unit
tests.

When a functional test exposes a low-level behavioural gap, consider whether
the corresponding unit-test coverage should be strengthened.

Use deterministic random sources in automated tests. Tests must not depend on
genuinely random outcomes.

Do not introduce a mocking framework unless there is a demonstrated need and
explicit approval.

After implementation changes, run:

    dotnet build
    dotnet test

Do not report the task as complete if either command fails.

## Change discipline

Keep changes focused on the requested task.

Do not make unrelated refactors, formatting changes, API redesigns, dependency
updates, or project-wide cleanup unless explicitly requested.

If satisfying a task appears to require a significant public API change,
architectural change, new external dependency, or behaviour outside the stated
scope:

1. identify the issue;
2. explain the proposed change and why it is necessary;
3. explain meaningful alternatives or compatibility implications;
4. wait for explicit approval before proceeding.

Small implementation changes that are clearly within the requested scope do
not require separate approval unless the task explicitly says otherwise.

Preserve backwards compatibility of existing public APIs unless a breaking
change has been explicitly approved.

## Repository and source control

Respect `.gitignore`, `.gitattributes`, and `.editorconfig`.

Do not commit, push, merge, rebase, create pull requests, or otherwise modify
repository history or remote GitHub state unless explicitly instructed to do
so.

It is acceptable to inspect Git state, diffs, history, and repository metadata
when useful for completing or reviewing a task.

Do not add generated build artefacts such as `bin` or `obj` directories to
source control.

## Dependencies

Prefer the .NET Base Class Library and existing project dependencies.

Do not add a new NuGet package or other external dependency merely for
convenience when the required functionality can be implemented cleanly with
the platform.

If a new dependency provides a meaningful engineering advantage, explain the
reason and obtain approval before adding it.

## Completion report

After completing a substantive implementation task, report:

- the files created;
- the files materially modified;
- any public API added or changed;
- build status;
- unit and functional test results where applicable;
- any design decisions made where the requirement allowed meaningful choice;
- any known limitations or deliberately deferred functionality.

Do not commit or push unless explicitly instructed.

## Terminal command policy

For routine read-only inspection, issue simple commands directly wherever
possible so that they match the user's persistent Codex execution rules.

Preferred read-only commands include:

- `Get-Content`
- `Get-ChildItem`
- `Get-Item`
- `Get-Location`
- `rg`
- `git status`
- `git diff`
- `git log`
- `git show`
- `git ls-files`

Do not unnecessarily combine multiple read-only operations into a single
`pwsh -Command` invocation.

Prefer several simple inspection commands over one compound PowerShell
command when this allows the commands to match existing execution rules.

Commands that modify source files, repository contents, Git state, or Git
history should continue to use the normal approval mechanism.
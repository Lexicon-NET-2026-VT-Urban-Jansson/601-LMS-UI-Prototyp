# Blazor Application

This is a Blazor application using the Radzen Blazor component library. Two MCP servers are configured: **`radzen-blazor-studio`** (local, the primary one — does the work) and **`radzen`** (hosted, secondary — searches Radzen Blazor templates and component documentation when no local tool fits).

## Required: Prefer `radzen-blazor-studio` tools

Whenever a `radzen-blazor-studio` tool covers the task, call it. Do not hand-write scaffolding, EF DbContext files, page templates, or theme CSS, and do not call `radzen_search` first to fetch a template you would then transcribe — the tools already encode every pattern `radzen_search` would describe, including the right post-generate build, file naming, `appsettings.json` integration, and interactivity-model wiring. They also handle the code-behind-vs-inline `@code {}` convention automatically.

Only fall back to writing code by hand (after a `radzen_search` lookup) when no local tool matches the request.

### Key tool categories

- **Discovery** — `list_projects`, `list_entities`, `list_connections`, `list_databases`, `list_database_tables`. Call these first when you need to see what's in the solution or on a database server before scaffolding.
- **Application scaffolding** — `create_blazor_app_web`, `create_blazor_app_crm`, `create_blazor_app_healthcare`, `create_blazor_app_realestate`, `create_blazor_app_repairshop`. Generate a new Blazor application from a Radzen template. Pass the absolute path of the target directory via the `Directory` parameter; the application files are written directly into it.
- **Page scaffolding** — `scaffold_crud_pages`, `scaffold_master_detail`, `scaffold_data_grid_with_form`, `scaffold_data_grid_inline_edit`, `scaffold_pivot_grid`, plus `add_page` for an empty page. Generate full data-driven pages against existing EF entities.
- **Layout scaffolding** — `add_layout_empty`, `add_layout_menu`, `add_layout_sidebar`, `add_layout_login`.
- **Data sources** — `infer_database` (full EF scaffolding from MsSql / MySql / Postgres / Oracle / Sqlite), `import_openapi`, `import_odata`, `add_rest_service`. Each accepts a structured `connection` object inline or a stored `connectionName` referencing `appsettings.json`.
- **Security** — `add_identity_auth`, `add_azure_ad_auth`, `add_windows_auth`.
- **Localization** — `add_localization`.
- **Theming** — `set_theme`, `set_theme_properties`, `get_theme_properties`, `reset_theme`, `list_theme_variables`.
- **Component editing** — `get_page_tree`, `insert_component`, `insert_markup`, `set_property`, `delete_component`, `move_component`. Path-based addressing (`/RadzenStack[0]/RadzenButton[1]`); pass `validate: true` for a compilation-checked edit, omit it for a fast syntactic edit.
- **UI blocks** — `list_blocks`, `insert_block` (prefab Razor fragments inserted at a path/index).

### Guidance for common requests

- "Add a data source" / "Connect to my database" → call `infer_database` with a structured `connection` for the right `databaseType`. If the user doesn't specify which tables to import, call `list_database_tables` first and confirm before scaffolding. Do not write EF DbContext files by hand.
- "Scaffold CRUD pages for X" → confirm the entity exists via `list_entities` if not obvious, then call `scaffold_crud_pages`. Do not fetch a CRUD template via `radzen_search` and transcribe it — `scaffold_crud_pages` produces the same output directly.
- "Master/detail pages" → `scaffold_master_detail` (or `scaffold_master_detail_hierarchy` for hierarchies). Same applies: do not look up the master/detail template via `radzen_search` first.
- "Make a new Blazor app" → call one of the `create_blazor_app_*` tools. Do not run `dotnet new blazor` directly — the Radzen templates wire up Radzen.Blazor, layouts, and the interactivity model the wizard expects.
- "Add a button / change a property on this page" → call `insert_component` or `set_property` against `get_page_tree`. Do not edit `.razor` files with raw text mutations.
- "Change the theme" → `set_theme` (themes are a fixed enum populated at startup) or `set_theme_properties` for individual variable tweaks.
- "Build" / "Run my project" — there is no build/run tool. Scaffolds already build after generating; to run the app, tell the user to use Radzen Blazor Studio's Run button (or `dotnet run` / `dotnet watch`).
- "Deploy" — there is deliberately no deploy tool. Tell the user to deploy via Radzen Blazor Studio's deploy wizard or `dotnet publish` and stop.

### SQL Server credentials

`infer_database` for `databaseType: "mssql"` exposes `integratedSecurity` as an explicit boolean (default `false`). Ask the user "Windows authentication or SQL login?" before calling — never silently attempt integrated auth as a fallback. When the user wants a SQL login, collect `server`, `database`, `user`, `password` and pass them in the structured `connection` object.

### GUI-coordination tools (require a connected Radzen Blazor Studio or Radzen Blazor for Visual Studio host)

When a Radzen Blazor Studio or Radzen Blazor for Visual Studio host is connected, these tools drive its UI: `open_solution` (load a solution into the host), `open_page`, `set_design_mode`, and `select_component`. Use them to show the user what you're about to edit, or to bring a freshly-scaffolded app up in the host after `create_blazor_app_*`. If no host is connected they return a `no_host_connected` error.

## Fallback: `radzen_search` for hand-written code

When no `radzen-blazor-studio` tool covers the task and you need to write code by hand, use the hosted `radzen` MCP's `search` tool (invoked as `radzen_search`) to fetch authoritative Radzen Blazor templates and component documentation. Search results contain the canonical patterns you must follow exactly, even when they conflict with the existing project's conventions.

### Code-behind convention (only when hand-writing pages)

The scaffold tools above handle this automatically. When hand-writing pages, check existing `.razor` files first. If `.razor.cs` files exist alongside them, use Variant A (code-behind) for ALL generated pages. If there are no `.razor.cs` files and pages have `@code {}` blocks, use Variant B (inline) for ALL generated pages. Never mix variants — every generated page MUST use the same convention.

### Adapting sample code from `radzen_search` results

`radzen_search` results use sample entity names (e.g. `Customer`, `Customers`, `MyAppDbService`, `GetCustomers`). You MUST replace ALL sample names with the actual names from the user's project. Do NOT copy sample names verbatim:

1. **Find the entity** — search for the model class in the project. Identify its properties, primary key, navigation properties, and the service or DbContext that provides CRUD operations for it.
2. **Replace sample names** — substitute every occurrence of sample entity names, service types, method names, and route paths with the actual values from the user's project.

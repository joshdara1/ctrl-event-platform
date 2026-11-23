# CTRL event management platform

A university final-year project by Joshua Daramola, built for browsing and managing events and DJ profiles, registering attendance and viewing ticket confirmations.

## Technologies

C#, .NET 8, ASP.NET Core Razor Pages, Entity Framework Core, SQLite, ASP.NET Core Identity and Bootstrap.

## Implemented features

- Database-backed event and DJ profiles with create, edit and delete pages.
- Event searching across title, venue, category, description and DJ name.
- Identity registration/login and role-based policies for management pages.
- Attendee registration, cancellation and an authenticated ticket-confirmation page.

Ticket confirmation is an on-screen page; this source does not establish payment processing, emailed tickets or downloadable PDF tickets.

## Running locally

Use the .NET 8 SDK. From this folder:

```sh
dotnet restore
dotnet tool install --tool-path ./.tools dotnet-ef --version 8.0.0
./.tools/dotnet-ef database update
dotnet run
```

The configuration uses `Data Source=app.db`. The saved coursework database was removed from this portfolio copy; migrations are retained to create a fresh local database. The initial site has no seeded event data.

For a local demo, register an account, then stop and restart the app. If no administrator exists, the startup code promotes the first existing account ordered by email. This is coursework bootstrap behaviour, not suitable for an internet-facing deployment.

Microsoft's [migration guidance](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying) explains applying the included migrations.

## Validation and limits

The university report documents functional and user testing. No automated test project was found in this source. The prepared copy restored its dependencies and built successfully with the .NET 8 SDK (zero warnings and errors). Database migrations, browser workflows, roles and registration were not exercised in this review, so verify those before presenting a live demo.

Role bootstrapping and authorisation boundaries need review before production deployment. No claim of production readiness or payment security is made.

## Portfolio preparation

Application logic was preserved. Build outputs, editor caches and the original SQLite database/journals were omitted from the publication package. Existing third-party library licence files were retained. Do not add an open-source licence for material you do not own.

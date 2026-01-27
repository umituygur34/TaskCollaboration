# TaskCollaboration API - Copilot Instructions

## Project Overview

A .NET 8.0 ASP.NET Core API for collaborative task management with JWT authentication. Users can create tasks, track status/priority, leave comments, and maintain activity logs. Uses PostgreSQL via EF Core 8.0 with Npgsql driver.

## Architecture Pattern

### Service-Based DI Model

- **Controllers** ([AuthController.cs](api/Controllers/AuthController.cs)): Minimal - just route handling, delegate to services via constructor injection
- **Services** ([AuthService.cs](api/Services/AuthService.cs)): Business logic (auth, password hashing with BCrypt, JWT token generation)
- **DbContext** ([TaskCollaborationDbContext.cs](api/Data/TaskCollaborationDbContext.cs)): Single context for all entities (Users, Tasks, Comments, ActivityLogs)
- **Interfaces** ([IAuthService.cs](api/Interfaces/IAuthService.cs)): Define service contracts, registered as `AddScoped<IAuthService, AuthService>` in [Program.cs](Program.cs#L54)

### DTO Pattern for API Contracts

- Separate DTOs from Models (never return raw domain models in responses)
- **UserDto**: Contains Id, Name, Email, Token (used for login/register responses)
- **LoginDto/RegisterDto**: Request contracts in [UserDtos.cs](api/DTOs/UserDtos.cs)
- **WorkTaskDto/WorkTaskCreateDto**: Task-related DTOs for input/output

### Data Model Relationships

- **User** ↔ **WorkTask** (1:Many) - `User.Tasks` collection, `WorkTask.UserId` foreign key
- **WorkTask** ↔ **Comment** (1:Many) - `WorkTask.Comments` collection
- **WorkTask** tracks **Status** (Todo/InProgress/Done) and **Priority** (Low/Medium/High) via enums in [Enums.cs](api/Models/Enums/Enums.cs)
- **ActivityLog** model exists but not yet integrated - prepare for audit trail features

## Key Conventions

### Authentication & Security

- JWT tokens generated in `AuthService.GenerateJwtToken()` - valid for 1 hour with claims for UserId and Email
- Passwords stored as BCrypt hashes - never hash plain text on response paths
- Token key configured via `AppSettings:TokenKey` in appsettings.json (must be set, throws if missing)
- JWT bearer authentication enabled in [Program.cs](Program.cs#L13-L31)

### Namespace & File Organization

- Namespace: `TaskCollaboration.Api.api.{Feature}` (note: double "api" due to folder structure)
- Models: [api/Models/](api/Models/)
- Services: [api/Services/](api/Services/)
- Controllers: [api/Controllers/](api/Controllers/)
- DTOs: [api/DTOs/](api/DTOs/)
- Enums: [api/Models/Enums/](api/Models/Enums/)

### Configuration & Settings

- `JwtSettings` class in [Settings/JwtSettings.cs](Settings/JwtSettings.cs) bound to `AppSettings` section in config
- Development config uses PostgreSQL connection string (Host, Port, Database, Username, Password)
- Settings injected via `IOptions<JwtSettings>` pattern in services

## Critical Workflows

### Local Development

1. **Database Setup**: EF Core migrations exist ([Migrations/](Migrations/)) - run `dotnet ef database update` to apply to PostgreSQL
2. **Run API**: `dotnet run` from root - uses Swagger UI at `/swagger/ui`
3. **Build**: `dotnet build`

### Adding New Features

1. Create Model in [api/Models/](api/Models/) with relationships and enums if needed
2. Add DbSet<T> to [TaskCollaborationDbContext.cs](api/Data/TaskCollaborationDbContext.cs#L14-L17)
3. Create DTO in [api/DTOs/](api/DTOs/) (separate request/response if needed)
4. Implement Interface in [api/Interfaces/](api/Interfaces/)
5. Implement Service in [api/Services/](api/Services/) with business logic
6. Register in [Program.cs](Program.cs) using `builder.Services.AddScoped<IInterface, Implementation>()`
7. Create Controller in [api/Controllers/](api/Controllers/) with dependency injection
8. Add EF migration: `dotnet ef migrations add FeatureName`

## Error Handling Patterns

- Services throw `Exception` with descriptive messages (e.g., "User not found", "User already exists")
- Controllers catch and return appropriate HTTP status codes (401 for auth failures, 200 for success)
- No centralized exception middleware yet - consider adding global error handler for consistency

## Testing Considerations

- No test project in current structure - add `TaskCollaboration.Api.Tests` (xUnit recommended) at solution root
- Mock `TaskCollaborationDbContext` using InMemory provider for unit tests
- Mock `JwtSettings` via `Options.Create<JwtSettings>()` for service tests

## Common Pitfalls to Avoid

- Don't use raw `User` model in API responses - use `UserDto`
- JWT claims use built-in ClaimTypes (NameIdentifier, Email) - keep consistent
- `ClockSkew = TimeSpan.Zero` means tokens expire exactly at specified time
- Nullable navigation properties (`User?`) require null-checks when accessing

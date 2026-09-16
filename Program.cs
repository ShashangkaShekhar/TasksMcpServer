using TasksMcpServer;

var builder = WebApplication.CreateBuilder(args);

// 1. Register the MCP server and enable HTTP transport
// WithToolsFromAssembly() automatically scans the current assembly for all classes marked with [McpServerToolType]
builder.Services.AddMcpServer()
    .WithHttpTransport(options =>
    {
        options.Stateless = true;
    })
    .WithToolsFromAssembly();

// 2. Configure CORS (Cross-Origin Resource Sharing)
// CORS is enabled because clients like GitHub Copilot in VS Code make cross-origin requests
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 3. Register your dependency services (e.g., data storage)
builder.Services.AddSingleton<TaskStore>();

var app = builder.Build();

app.UseCors();

// 4. Add a health check endpoint (optional, but useful for deployment to container apps)
app.MapGet("/health", () => Results.Ok("healthy"));

// 5. Map the MCP endpoint
// Mounts the MCP service under the /mcp path, supporting streamable HTTP
app.MapMcp("/mcp");

app.Run();
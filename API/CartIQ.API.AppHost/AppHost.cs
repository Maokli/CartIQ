var builder = DistributedApplication.CreateBuilder(args);

var ollama =
    builder.AddOllama("ollama")
        .WithImageTag("latest")
        .WithDataVolume()
        .WithOpenWebUI();
var llm = ollama.AddModel("LLM", "qwen3.5:0.8b");

var apiService = builder.AddProject<Projects.CartIQ_WebApi>("api")
    .WithHttpHealthCheck("/health")
    .WithReference(llm)
    .WaitFor(llm);

builder.Build().Run();

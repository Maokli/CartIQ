var builder = DistributedApplication.CreateBuilder(args);

var ollama =
    builder.AddOllama("ollama")
        .WithImageTag("latest")
        .WithDataVolume()
        .WithOpenWebUI();
var llm = ollama.AddModel("LLM", "gemma3:4b");

var messageBroker = builder.AddRabbitMQ("message-broker")
    .WithDataVolume();
var workerService = builder.AddProject<Projects.CartIQ_Worker>("worker")
    .WithHttpHealthCheck()
    .WithReference(messageBroker)
    .WaitFor(messageBroker);

var apiService = builder.AddProject<Projects.CartIQ_WebApi>("api")
    .WithHttpHealthCheck("/health")
    .WithReference(llm)
    .WithReference(messageBroker)
    .WithReference(workerService)
    .WaitFor(workerService)
    .WaitFor(messageBroker)
    .WaitFor(llm);

builder.Build().Run();

var builder = DistributedApplication.CreateBuilder(args);

// var ollama =
//     builder.AddOllama("ollama")
//         .WithImageTag("latest")
//         .WithDataVolume()
//         .WithOpenWebUI();
// var llm = ollama.AddModel("LLM", "gemma3:4b");

var messageBroker = builder.AddRabbitMQ("message-broker")
    .WithDataVolume();
var ocrService = builder.AddDockerfile(
        "ocr",
        "../external/mcp-ocr-api")
    .WithHttpEndpoint(
        targetPort: 8000,
        name: "http")
    .WithHttpHealthCheck("/health");

var workerService = builder.AddProject<Projects.CartIQ_Worker>("worker")
    .WithReference(messageBroker)
    .WithReference(ocrService.GetEndpoint("http"))
    .WaitFor(messageBroker)
    .WaitFor(ocrService);

var apiService = builder.AddProject<Projects.CartIQ_WebApi>("api")
    .WithHttpHealthCheck("/health")
    //.WithReference(llm)
    .WithReference(messageBroker)
    .WithReference(workerService)
    .WaitFor(messageBroker);
    //.WaitFor(llm);

builder.Build().Run();

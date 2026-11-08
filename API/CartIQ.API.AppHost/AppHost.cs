var builder = DistributedApplication.CreateBuilder(args);

var ollama =
    builder.AddOllama("ollama")
        .WithImageTag("latest")
        .WithDataVolume()
        .WithOpenWebUI();
var llm = ollama.AddModel("llm", "gemma3:4b");

var messageBroker = builder.AddRabbitMQ("message-broker")
    .WithDataVolume();
var ocrService = builder.AddDockerfile(
        "ocr",
        "../external/mcp-ocr-api")
    .WithHttpEndpoint(
        targetPort: 8000,
        name: "http")
    .WithHttpHealthCheck("/health")
    .WithVolume("rapidOCR-models", "/home/louay/.rapidocr/models");

var workerService = builder.AddProject<Projects.CartIQ_Worker>("worker")
    .WithReference(messageBroker)
    .WithReference(ocrService.GetEndpoint("http"))
    .WithReference(llm)
    .WaitFor(messageBroker)
    .WaitFor(ocrService)
    .WaitFor(llm);

var apiService = builder.AddProject<Projects.CartIQ_WebApi>("api")
    .WithHttpHealthCheck("/health")
    .WithReference(messageBroker)
    .WithReference(workerService)
    .WaitFor(messageBroker);

builder.Build().Run();

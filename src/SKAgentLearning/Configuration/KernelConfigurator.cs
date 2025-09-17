using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using System;
using System.Net.Http;

namespace HelloKernel
{
    public class KernelConfigurator
    {
        private readonly IConfiguration _config;

        public KernelConfigurator(IConfiguration config)
        {
            _config = config;
        }

        public IKernelBuilder ConfigureKernelBuilder()
        {
            var kernelBuilder = Kernel.CreateBuilder();

            var chatModelId = _config["OpenAI:ChatCompletionModelId"];
            var embeddingModelId = _config["OpenAI:EmbeddingModelId"];
            var apiKey = _config["OpenAI:ApiKey"];
            var endpoint = _config["OpenAI:Endpoint"];

            #pragma warning disable SKEXP0010
            kernelBuilder.AddOpenAIChatCompletion(
                modelId: chatModelId,
                apiKey: apiKey,
                endpoint: new Uri(endpoint)
            );

            kernelBuilder.AddOpenAIEmbeddingGenerator(
                modelId: embeddingModelId,
                apiKey: apiKey,
                httpClient: new HttpClient { BaseAddress = new Uri(endpoint) }
            );
            #pragma warning restore SKEXP0010

            return kernelBuilder;
        }
    }
}

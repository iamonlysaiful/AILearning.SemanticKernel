using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;

namespace SKAgentLearning.RoadmapSteps
{
    public static class HelloAgent
    {
        public static async Task RunAsync(Kernel kernel)
        {
            var agent = new ChatCompletionAgent
            {
                Name = "helper",
                Instructions = "You are a concise assistant. Keep answers short.",
                Kernel = kernel
            };
            
            var session = new ChatHistory();

            while (true)
            {
                Console.Write("User: ");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) break;

                session.AddUserMessage(input);

                // Agent responds
                await foreach (var messageResponse in agent.InvokeAsync(session))
                {
                    var msg = messageResponse.Message;
                    Console.WriteLine($"{msg.Role}: {msg.Content}");
                    session.Add(msg);
                }
            }
        }
    }
}

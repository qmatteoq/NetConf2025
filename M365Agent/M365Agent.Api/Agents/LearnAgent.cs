using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;
using ModelContextProtocol.Server;

namespace M365Agent.Api.Agents
{
    public class LearnAgent(IChatClient client)
    {
        private const string AgentName = "LearnAgent"; 
        private const string AgentInstructions = """
            You are a specialized assistant that helps users with questions about Microsoft products and services.
            You have direct access to the Microsoft Learn documentation through MCP (Model Context Protocol).
            
            Your responsibilities:
            - Answer questions about Microsoft products, services, and technologies
            - Provide accurate information from official Microsoft Learn documentation
            - Offer step-by-step guidance and best practices
            - Reference relevant documentation links when appropriate
            - Stay up-to-date with the latest Microsoft technologies and features
            
            Always provide clear, accurate, and well-sourced information based on official Microsoft documentation.
            """;

        private ChatClientAgent _agent;
        private IChatClient _client = client;

        public async Task<AIAgent> InitializeAgent()
        {
            await using var learnMcpClient = await McpClient.CreateAsync(new HttpClientTransport(new()
            {
                Endpoint = new Uri("https://learn.microsoft.com/api/mcp")
            }));

            var mcpTools = await learnMcpClient.ListToolsAsync();

            _agent = new ChatClientAgent
                (_client,
                name: AgentName,
                instructions: AgentInstructions,
                tools: [.. mcpTools.Cast<AITool>()]);

            return _agent;
        }
    }
}

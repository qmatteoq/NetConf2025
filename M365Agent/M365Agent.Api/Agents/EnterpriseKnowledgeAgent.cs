using Azure.AI.Agents.Persistent;
using Azure.Identity;
using Microsoft.Agents.AI;

namespace M365Agent.Api.Agents
{
    public class EnterpriseKnowledgeAgent
    {
        private string AgentName = "EnterpriseKnowledgeAgent";
        private readonly string foundryEndpoint;

        public EnterpriseKnowledgeAgent(IConfiguration configuration)
        {
            foundryEndpoint = configuration["Azure:FoundryEndpoint"] ?? throw new ArgumentNullException("FoundryEndpoint configuration is missing");
        }

        public async Task<AIAgent> InitializeAgent()
        {
            var persistentAgentsClient = new PersistentAgentsClient(foundryEndpoint, new AzureCliCredential());
            return await persistentAgentsClient.GetAIAgentAsync("asst_bLK9gsSMArlybJYNflrIne49");
        }
    }
}

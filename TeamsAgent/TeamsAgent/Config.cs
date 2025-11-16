namespace TeamsAgent
{
    public class ConfigOptions
    {
        public TeamsConfigOptions Teams { get; set; }
        public AzureConfigOptions Azure { get; set; }
    }

    public class TeamsConfigOptions
    {
        public string BotType { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TenantId { get; set; }
    }

    /// <summary>
    /// Options for Azure OpenAI and Azure Content Safety
    /// </summary>
    public class AzureConfigOptions
    {
        public string OpenAIApiKey { get; set; }
        public string OpenAIEndpoint { get; set; }
        public string OpenAIDeploymentName { get; set; }
        public string OpenAIEmbeddingDeploymentName { get; set; }
        public string AISearchApiKey { get; set; }
        public string AISearchEndpoint { get; set; }
    }
}

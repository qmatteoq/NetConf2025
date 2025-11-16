# .NET Conf 2025 Milan - Demo Repository

Welcome to the official demo repository for .NET Conf 2025 held in Milan! This repository contains various sample applications and demonstrations showcasing the latest features and capabilities of .NET 10 and Microsoft Teams development.

## 📋 Table of Contents

- [Overview](#overview)
- [Projects](#projects)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Projects Details](#project-details)
- [Technologies Showcased](#technologies-showcased)
- [Contributing](#contributing)
- [License](#license)
- [Support](#support)

## 🎯 Overview

This repository demonstrates cutting-edge .NET development practices and Microsoft 365/Teams application development through two comprehensive solutions:

### M365Agent Solution
- **Microsoft 365 Agents Framework**: Next-generation conversational AI agents
- **Azure OpenAI Integration**: Advanced language model capabilities with function invocation
- **Model Context Protocol**: Direct access to Microsoft Learn documentation
- **M365 Agents Playground**: Compatible with Microsoft's agent development environment

### TeamsAgent Solution  
- **Teams SDK Integration**: Advanced conversational agents for Microsoft Teams
- **Feedback Management**: Complete end-to-end feedback collection and viewing system
- **Azure AI Search**: Intelligent document search and RAG capabilities
- **Microservices Architecture**: Distributed application design with .NET Aspire

### Common Features
- **Azure Integration**: Cloud-native applications with Azure services
- **Modern Web Development**: ASP.NET Core with Blazor components
- **.NET 10 Features**: Latest .NET capabilities and performance improvements
- **.NET Aspire**: Service orchestration and management for both solutions

## 🚀 Solutions

This repository contains two comprehensive solutions showcasing different aspects of modern .NET and Microsoft 365 development:

### 🤖 **M365Agent Solution**
A complete Microsoft 365 Agents solution featuring AI-powered conversational agents with Model Context Protocol (MCP) integration.

#### Projects:
- **M365Agent.Api**: Core AI agent service with Azure OpenAI integration
- **M365Agent.App**: Microsoft 365 Agents Playground compatible bot application  
- **M365Agent.AppHost**: .NET Aspire orchestration for the M365 solution
- **M365Agent.ServiceDefaults**: Shared service configurations

**Key Features:**
- Microsoft 365 Agents framework integration
- Azure OpenAI chat client with function invocation
- Model Context Protocol (MCP) for Microsoft Learn documentation access
- LearnAgent specialized for Microsoft products and services questions
- .NET Aspire orchestration with dev tunnels
- M365 Agents Playground compatibility

**Technologies:** .NET Aspire, Azure OpenAI, Microsoft Agents AI, Model Context Protocol, Microsoft 365 Agents

### 🔧 **TeamsAgent Solution** 
A comprehensive Teams application ecosystem featuring AI-powered agents, feedback management, and Azure AI Search integration.

#### Projects:
- **TeamsAgent**: Main Teams agent application with Azure AI Search
- **FeedbackApi**: RESTful API service for feedback management
- **FeedbackViewer**: Blazor Server application for viewing feedback
- **M365Agent**: Teams app integration components
- **TeamsAgent.AppHost**: .NET Aspire application host
- **TeamsAgent.ServiceDefaults**: Shared service configurations

**Key Features:**
- Teams SDK integration with AI capabilities
- Azure AI Search for document indexing and RAG
- Complete feedback management system
- Interactive Blazor components
- Microservices architecture with .NET Aspire
- Cross-service communication and orchestration

**Technologies:** ASP.NET Core, Microsoft Teams API, Azure AI Search, Blazor Server, Entity Framework Core, .NET Aspire

## 📋 Prerequisites

Before running the applications in this repository, ensure you have:

### Software Requirements
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later
- [Visual Studio 2025](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [PowerShell](https://github.com/PowerShell/PowerShell) (for scripts)

### Azure Services
- [Azure subscription](https://azure.microsoft.com/free/)
- [Azure OpenAI](https://aka.ms/oai/access) resource
- [Azure AI Search](https://azure.microsoft.com/products/ai-services/ai-search) service

### Microsoft 365 & Teams
- [Microsoft 365 Developer tenant](https://developer.microsoft.com/microsoft-365/dev-program)
- [Teams Toolkit](https://docs.microsoft.com/microsoftteams/platform/toolkit/teams-toolkit-fundamentals)
- [Microsoft 365 Agents Playground](https://aka.ms/agents-playground) (for M365Agent solution)

### Development Tools
- [AI Toolkit for VS Code](https://marketplace.visualstudio.com/items?itemName=ms-windows-ai-studio.windows-ai-studio) (optional, for enhanced AI development experience)

## 🛠️ Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/qmatteoq/NetConf2025.git
cd NetConf2025
```

### 2. Setup Azure Resources
1. Create an Azure AI Search service (for TeamsAgent solution)
2. Create an Azure OpenAI resource (for both solutions)
3. Note down the connection strings and API keys

### 3. Configure Application Settings
Update the `appsettings.Development.json` files in each solution with your Azure service configurations.

### 4. Running the M365Agent Solution
```bash
# Navigate to the M365Agent directory
cd M365Agent

# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run the AppHost (this will start the API and App services)
dotnet run --project M365Agent.AppHost
```

The M365Agent solution will:
- Start the API service with the LearnAgent
- Start the bot application compatible with M365 Agents Playground
- Create a dev tunnel for external access
- Optionally launch the M365 Agents Playground emulator

### 5. Running the TeamsAgent Solution
```bash
# Navigate to the TeamsAgent directory
cd TeamsAgent

# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run the AppHost (this will start all services)
dotnet run --project TeamsAgent.AppHost
```

### 6. Teams Agent Specific Setup
For the Teams Agent specifically:
1. Run the `Indexer.ps1` script to create your document index
2. Configure your Teams app manifest
3. Deploy to Teams for testing

## 📁 Project Details

### Directory Structure
```
M365Agent/                        # Microsoft 365 Agents Solution
├── M365Agent.Api/               # Core AI agent service
├── M365Agent.App/               # Bot application for M365 Agents Playground
├── M365Agent.AppHost/           # .NET Aspire orchestration
└── M365Agent.ServiceDefaults/   # Shared service configurations

TeamsAgent/                       # Teams Agent Solution  
├── FeedbackApi/                 # Web API for feedback management
├── FeedbackViewer/              # Blazor app for viewing feedback
├── M365Agent/                   # Teams app integration components
├── TeamsAgent/                  # Main Teams agent application
├── TeamsAgent.AppHost/          # .NET Aspire host
└── TeamsAgent.ServiceDefaults/  # Shared configurations
```

### Key Configuration Files
- `M365Agent.slnx` - M365 Agents solution file
- `TeamsAgent.slnx` - Teams Agent solution file
- `appsettings.json` - Application configurations
- `launchSettings.json` - Development launch profiles
- `manifest.json` - Teams app manifest (in TeamsAgent/M365Agent)
- `m365agents.yml` - M365 Agents configuration (in TeamsAgent/M365Agent)

## 🔧 Technologies Showcased

### .NET 10 Features
- **Performance Improvements**: Latest runtime optimizations
- **Language Features**: New C# capabilities
- **ASP.NET Core**: Enhanced web development features
- **Entity Framework Core**: Advanced ORM capabilities

### Microsoft 365 & AI Platforms
- **Microsoft 365 Agents Framework**: Next-generation conversational AI development
- **Microsoft Agents AI Library**: Advanced agent building capabilities
- **Model Context Protocol (MCP)**: Structured access to external data sources
- **Azure OpenAI**: Large language model integration with function invocation
- **M365 Agents Playground**: Development and testing environment

### Microsoft Teams Platform
- **Teams AI Library V2**: Next-generation conversational AI for Teams
- **Teams Toolkit**: Modern development experience
- **Adaptive Cards**: Rich interactive components
- **Message Extensions**: Enhanced user interactions

### Azure Services
- **Azure AI Search**: Intelligent document search and retrieval
- **Azure OpenAI**: Large language model integration
- **Azure Identity**: Secure authentication and authorization
- **Azure App Service**: Cloud hosting and deployment

### Modern Development Practices
- **Microservices Architecture**: Distributed application design
- **API-First Development**: RESTful service design
- **Cloud-Native**: Azure-ready applications
- **.NET Aspire**: Service orchestration and management for both solutions
- **Dev Tunnels**: Secure external access for development

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🆘 Support

- **General Questions**: Create an issue in this repository
- **Documentation**: Check the [SUPPORT.md](SUPPORT.md) file
- **Security Issues**: Please review our [Security Policy](SECURITY.md)

## 📚 Additional Resources

- [.NET Conf 2025 Official Site](https://www.dotnetconf.net/)
- [.NET 10 Documentation](https://docs.microsoft.com/dotnet/)
- [Microsoft Teams Platform Documentation](https://docs.microsoft.com/microsoftteams/platform/)
- [Azure AI Services Documentation](https://docs.microsoft.com/azure/ai-services/)
- [.NET Aspire Documentation](https://docs.microsoft.com/dotnet/aspire/)

## 🏷️ Tags

`dotnet` `dotnet10` `teams` `azure` `ai` `aspire` `blazor` `webapi` `milan` `netconf2025`

---

**Happy Coding! 🚀**

*This repository was created for .NET Conf 2025 in Milan to showcase the latest .NET technologies and Microsoft Teams development capabilities.*
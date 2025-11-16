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

This repository demonstrates modern .NET development practices and Microsoft Teams application development, featuring:

- **Teams Agents & AI Integration**: Advanced conversational agents using Microsoft Teams SDK
- **Feedback Management**: Complete feedback collection and viewing solutions
- **Azure Integration**: Cloud-native applications with Azure services
- **Modern Web Development**: ASP.NET Core with Blazor components
- **.NET 10 Features**: Latest .NET capabilities and performance improvements

## 🚀 Projects

### 1. **TeamsAgent** 
A comprehensive Teams application featuring AI-powered agents with Azure AI Search integration.

**Key Features:**
- Teams SDK integration
- Azure AI Search for document indexing
- Retrieval Augmented Generation (RAG) capabilities
- Custom data source integration

**Technologies:** ASP.NET Core, Microsoft Teams API, Azure AI Search, Azure Identity

### 2. **FeedbackApi**
RESTful API service for managing feedback entries with Entity Framework Core.

**Key Features:**
- RESTful API endpoints
- Entity Framework Core with SQLite
- OpenAPI/Swagger documentation
- Cross-origin resource sharing (CORS)

**Technologies:** ASP.NET Core Web API, Entity Framework Core, SQLite

### 3. **FeedbackViewer**
Modern web application for viewing and managing feedback using Blazor Server.

**Key Features:**
- Interactive Blazor components
- Real-time feedback display
- Responsive design
- Integration with FeedbackApi

**Technologies:** Blazor Server, ASP.NET Core

### 4. **M365Agent**
Microsoft 365 integrated agent demonstrating Teams app development.

**Key Features:**
- Microsoft 365 integration
- Teams app manifest
- AI Toolkit project structure
- Development tools and configuration

**Technologies:** Microsoft 365, Teams Platform, AI Toolkit

### 5. **TeamsAgent.AppHost**
.NET Aspire application host for orchestrating and managing distributed applications.

**Key Features:**
- Service orchestration
- Development dashboard
- Service discovery
- Configuration management

**Technologies:** .NET Aspire

### 6. **TeamsAgent.ServiceDefaults**
Shared service configurations and extensions for consistent application behavior.

**Key Features:**
- Common service configurations
- Shared extensions
- Consistent logging and monitoring
- Reusable components

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

## 🛠️ Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/qmatteoq/NetConf2025.git
cd NetConf2025
```

### 2. Setup Azure Resources
1. Create an Azure AI Search service
2. Create an Azure OpenAI resource
3. Note down the connection strings and API keys

### 3. Configure Application Settings
Update the `appsettings.Development.json` files in each project with your Azure service configurations.

### 4. Build and Run
```bash
# Navigate to the solution directory
cd TeamsAgent

# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run the AppHost (this will start all services)
dotnet run --project TeamsAgent.AppHost
```

### 5. Teams Agent Setup
For the Teams Agent specifically:
1. Run the `Indexer.ps1` script to create your document index
2. Configure your Teams app manifest
3. Deploy to Teams for testing

## 📁 Project Details

### Directory Structure
```
TeamsAgent/
├── FeedbackApi/          # Web API for feedback management
├── FeedbackViewer/       # Blazor app for viewing feedback
├── M365Agent/            # Microsoft 365 integration
├── TeamsAgent/           # Main Teams agent application
├── TeamsAgent.AppHost/   # .NET Aspire host
└── TeamsAgent.ServiceDefaults/  # Shared configurations
```

### Key Configuration Files
- `TeamsAgent.slnx` - Solution file
- `appsettings.json` - Application configuration
- `launchSettings.json` - Development launch profiles
- `manifest.json` - Teams app manifest (in M365Agent)

## 🔧 Technologies Showcased

### .NET 10 Features
- **Performance Improvements**: Latest runtime optimizations
- **Language Features**: New C# capabilities
- **ASP.NET Core**: Enhanced web development features
- **Entity Framework Core**: Advanced ORM capabilities

### Microsoft Teams Platform
- **Teams AI Library V2**: Next-generation conversational AI
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
- **.NET Aspire**: Service orchestration and management

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
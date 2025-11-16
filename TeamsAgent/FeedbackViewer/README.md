# Feedback Viewer

A Blazor Server application that displays feedback collected by the FeedbackApi in a table format.

## Features

- **Home Page**: Welcome page with navigation to the feedback view
- **Feedback Page**: Displays all feedback entries in a table with:
  - ID
  - Reaction (emoji)
  - Feedback text
  - Creation timestamp
- **Refresh Button**: Manually refresh the feedback data
- **Responsive Design**: Works on desktop and mobile devices

## Configuration

The application uses service discovery to automatically connect to the FeedbackApi when running through the Aspire AppHost.

## Running the Application

When running the solution through the AppHost (TeamsAgent.AppHost), the FeedbackViewer will automatically start and be accessible through the Aspire dashboard.

## Project Structure

- `Components/Pages/Home.razor` - Home page
- `Components/Pages/Feedback.razor` - Main feedback display page with table
- `Components/Layout/` - Layout components (MainLayout, NavMenu)
- `Models/FeedbackEntry.cs` - Feedback data model
- `Services/FeedbackService.cs` - Service for fetching feedback from API
- `wwwroot/` - Static assets (CSS, etc.)

## Dependencies

- .NET 10.0
- Blazor Server
- TeamsAgent.ServiceDefaults (for Aspire service discovery)

## API Integration

The application connects to the FeedbackApi's `/api/feedback` endpoint to retrieve all feedback entries. The connection is automatically configured through Aspire's service discovery mechanism.

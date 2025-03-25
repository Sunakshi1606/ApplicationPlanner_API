# ActivityPlannerAPI

## Project Overview

ActivityPlannerAPI is a .NET Core Web API application designed to provide comprehensive weather-based activity recommendations. The API aggregates and processes weather forecast data to suggest optimal outdoor activities based on temperature and weather conditions.

## Project Status

**Current Phase**: Prototype Development
- [x] Weather forecast API integration
- [x] 7-day activity recommendations
- [x] Temperature-based activity ranking
- [ ] Additional feature enhancements planned

## System Requirements

### Development Environment
- Visual Studio 2019 or later
- .NET Core SDK
- Windows 10/11 (Recommended)

### Recommended Tools
- Visual Studio 2022
- Postman (for API testing)
- SQL Server (if using database)

## Technology Stack

- **Framework**: .NET Core
- **API Documentation**: Swagger/OpenAPI
- **Development IDE**: Visual Studio
- **Primary Purpose**: Weather-based Activity Recommendations

## Installation and Setup

### Prerequisite Software
- Visual Studio 2019 or later
- .NET Core SDK

### Installation Steps

1. **Repository Cloning**
   ```bash
   git clone https://github.com/Sunakshi1606/ApplicationPlanner_API.git
   cd ActivityPlannerAPI
   ```

2. **Open Solution**
   - Launch Visual Studio
   - Open `ApplicationPlannerAPI.sln`

3. **Build the Project**
   - Select "Build" from the top menu
   - Choose "Build Solution" (Ctrl+Shift+B)

4. **Run the Application**
   ```
   Press Ctrl+F5 to launch the application
   ```

### Application Launch
- Swagger UI: `https://localhost:7004/swagger/index.html`

## API Endpoints

### Weather-based Activity Recommendations
- `GET /api/activities/forecast`
  - Retrieves 7-day activity recommendations
  - Parameters: Location, Date Range

### Example Request
```http
GET /api/activities/forecast?location=NewYork&days=7
```

## Configuration

### Swagger/OpenAPI Configuration
- Comprehensive API documentation
- Interactive API testing interface
- Detailed endpoint descriptions

### Potential Configuration Files
- `appsettings.json`
- `appsettings.Development.json`
- `appsettings.Production.json`

## Testing

### API Testing Methods
- Swagger UI
- Postman
- 
## Troubleshooting

### Common Issues
- Verify .NET Core SDK installation
- Check Visual Studio version compatibility
- Ensure all dependencies are installed
- Validate API endpoint configurations


**Disclaimer**: This API provides activity recommendations based on weather forecasts. Always verify local conditions and exercise personal judgment.

# HoopInsights API

**HoopInsights API** is a backend-focused .NET application that integrates with the NBA data API created by [balldontlie.io](https://www.balldontlie.io) to provide advanced statistical data, such as:

- Daily, weekly and monthly point differentials
- Statistical leaders in points, rebounds, assists, and fouls
- Team offensive and defensive averages
- Player performance trends against specific teams

This API is structured to support future integration with a frontend application, providing a comprehensive platform for NBA statistical analysis.

## Key Features

- **Data Aggregation**: Fetches and processes data from balldontlie.io.
- **Testing Suite**: Includes XUnit tests with Moq and FluentAssertions.
- **Modular Architecture**: Built with SOLID principles and Domain-Driven Design.
- **Advanced Analytics**: Computes metrics like net ratings and player efficiency.

<b>Technologies:</b> .NET MVC, PostgreSQL

## Getting Started

1. Clone the repository.
2. Configure your environment variables.
3. Run the application using Docker or your preferred method.
4. Use Postman to interact with the API endpoints.

## Future Enhancements

- Add Docker configuration for containerisation
- Expand analytics to include player efficiency ratings and usage
- Implement a dynamic and responsive frontend to display and query data
- Introduce caching mechanisms for common API calls to optimise performance
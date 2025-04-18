# HoopInsights API

**HoopInsights API** is a backend-focused .NET 8 application that integrates with the [balldontlie.io](https://www.balldontlie.io) NBA data API. 
Designed to provide data from the National Basketball Association (NBA), it computes advanced metrics such as:

- Daily, weekly and monthly point differentials
- Statistical leaders in points, rebounds, assists, and fouls
- Team offensive and defensive averages
- Player performance trends against specific teams

This API is structured to support future integration with a Next.js/React frontend, providing a comprehensive platform for NBA statistical analysis.

## Key Features

- **Data Aggregation**: Fetches and processes data from balldontlie.io.
- **Advanced Analytics**: Computes metrics like net ratings and player efficiency.
- **Modular Architecture**: Built with SOLID principles and Domain-Driven Design.
- **Testing Suite**: Includes XUnit tests with FakeItEasy and FluentAssertions.

<b>Technologies:</b> NET 8 MVC, LINQ, PostgreSQL, Docker

## Getting Started

1. Clone the repository.
2. Configure your environment variables.
3. Run the application using Docker or your preferred method.
4. Use Postman to interact with the API endpoints.

## Future Enhancements

- Implement a Next.js/React frontend.
- Introduce caching mechanisms for performance optimization.
- Expand analytics to include player efficiency ratings and usage rates.
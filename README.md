# FormulaOne

A REST API for accessing data scraped from the Formula 1 official website's standings tab. It includes drivers, teams and race results.

Built with ASP.NET 8, MSSQL and Docker. 

API Documentation: https://gosling7.github.io/FormulaOne/

## Features

- Code-first database approach
- Multi-container
- Clean Architecture design (with minor tweaks)
- Unit and Integration tests
- User friendly error responses
- Pagination
- Data filtering
- Bulk fetching
- Data scrapers

## How to use

### Run locally

In the root of the repository:
1. Run `docker-compose-f docker-compose.db.yml up` to start the database.
2. Run `dotnet run`.

### Run in container with HTTPS

In the root of the repository:
1. Make your `.env` file by copying the `.env.example` file (`cp .env.example .env`) and modifying it as needed.
2. Create a certificate `dotnet dev-certs https -ep "./certs/aspnetapp.pfx" -p "[YourPassword]"`. You can adjust the path on the host inside `.env`file with the `CERTIFICATE_HOST_DIRECTORY` and `CERTIFICATE_FILENAME` variables.
3. Run `docker-compose up` which will start the database and the api in separate containers.

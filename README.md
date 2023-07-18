# Sierra TakeHome Project
[![forthebadge](https://forthebadge.com/images/badges/made-with-c-sharp.svg)](https://forthebadge.com) [![forthebadge](https://forthebadge.com/images/badges/60-percent-of-the-time-works-every-time.svg)](https://forthebadge.com)  
This repo contains a small part of the Sierra TakeHome API, for testing and valdiation purposes only.

## How to run
### Database
- Create a new database on SQL Server and update the *local* connection string on Sierra.TakeHome.API project;
- From the solution folder run the follow command to start migrating the database for the most recent version;
  ```sh
  dotnet ef database update --project Sierra.TakeHome.Database.Migrations/Sierra.TakeHome.Database.Migrations.csproj --startup-project Sierra.TakeHome.API/Sierra.TakeHome.API.csproj --context Sierra.TakeHome.Data.Database.DatabaseContext --configuration Debug
  ```
- You are all set, enjoy..

### Run the unit tests
- From the solution folder run the following command from solution folder
  ```sh
  dotnet test
   ```

### Running the application
- From the solution folder run the following command to start the application
  ```sh
  dotnet run --project Sierra.TakeHome.API/Sierra.TakeHome.API.csproj
  ```
- After the bootstrap you can access the Swagger documentation using the address *http://localhosy:5183/swagger*

### Overral info

### Third-party modules

I'm using some nuget packages to help me wrap up a version of the project faster, the most important ones are listed here:

| Nuget | Url | Details |
| --- | --- | --- |
|MockQueryable.NSubstitute|https://www.nuget.org/packages/MockQueryable.NSubstitute|Used for generate Sync/Async stubs for unit tests (useful for Async operations)|
|MediatR|https://www.nuget.org/packages/MediatR|Simple implementation of the pattern Mediatr, help to reduce rependencies and keep the code more testable|
|AutoMapper|https://www.nuget.org/packages/AutoMapper|Component for mapping internal models to public one, help reducing the time to refactor all big components|


### Call samples
- This call will fetch the specified product from the database:
  ```sh
  curl --location 'http://localhost:5183/products/1020fce0-aae5-4885-8c88-38901ebc2a8f'
  ```

- This another call will generate new access token based on specified `customerId`:
  ```sh
  curl --location 'http://localhost:5183/auth' --header 'Content-Type: application/json' --data '{ "customerId": "4113ce29-dd73-4dfc-a505-9b7e86ea5f6e" }'
  ```

- Finally, this last one will create a new order and return the values
  ```sh
  curl --location 'http://localhost:5183/orders' --header 'Content-Type: application/json' --header 'Authorization: Bearer eyJhbG...qF-w' --data '{ "productId": "1020FCE0-AAE5-4885-8C88-38901EBC2A8F", "quantity": 85 }'
  ```
  _The **customerId** are being retrieved from the authentication token_


### Assumptions for this project
Some important notes regarding the start of the development and some assumption that I made working in this project
- The ids fields on the database are 100% guid (uniqueidentifier) type, that could be easily changed;
- I splitted the solution in 4 different projectos, make sense to keep the migrations and unit tests away from the application core;
- All components used by the application are `dockerizable`, it is just a matter to implement this use case;
- I'm mixing the `observer` and `repository` patterns;

**Happily coded by Andre Garcia, Hell Yeah!**

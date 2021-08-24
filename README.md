**SevenPeaks VTS Coding Challenge**


I have used Clean Architecture for this POC, and here is how it is organized.
This project is organized in such a way that there is loose coupling between layers and embraces dependency inversion principle. At the Core of the application is **Domain** where entities and Auditable Base entities are defined. The domain is independent of any feature laden tool or framework, which makes it ideal for long term use. On top of this layer is the **Application** layer where we have our Contracts(Interfaces for Repository & Services),Exceptions, CQRS implementation.Next in line of dependency is **Infrastructure** layer where frameworks and tools are used. This project uses **Entity framework core** for Commands as our ORM & **Dapper** for Queries for better mapping and performance respectively.I have used Sql Server database for storage.

**A little about the Domain Entities Design**

*We have **Vehicle** where we define differnt properties Like Name, Maker,Model and so on. So I have assumed that *Vehicle * Entity is an aggregate root. Under Vehicle  we have  **TrackingDevice** with properties like Name,Model,Imei. The Tracking Device will record the **Location** which has the Latitude, Longitude,Altitude and Vehicle Id. So we have API controllers defined for various business points.

---

## Project Structure

The project is orgainized with Clean architecture in mind, hence I have 
#### VTS Source
* Implemented **DDD, CQRS, and Clean Architecture** with Best Practices.
* Developed **CQRS with using MediatR, FluentValidation  packages**
* **SQL Server database** connection and containerization
* Using **Entity Framework Core for Commands ** and auto migrate to DB on application startup.
* Using **Dapper for Queries **.
#### PostMan Collection
* First import environment **Vehicle tracking system.postman_environment.json**
* Second import collection **VehicleTracking System API.postman_collection.json**
#### Video
* Check the attached video Under Docs

#### Docker Compose  with all dependencies on docker;
* Containerization of API,DB.
* Containerization of databases
* Override Environment variables

## Running The Project
We will need the following tools:

* [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
* [.Net Core 5 or later](https://dotnet.microsoft.com/download/dotnet-core/5)
* [Docker Desktop](https://www.docker.com/products/docker-desktop)

Follow these steps to get your development environment set up: (Before Running Start the Docker Desktop)
1. Clone the repository
. At the root directory which include **docker-compose.yml** files, run below command:
```
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```


 You can use:

* **VTS API -> http://localhost:8000/swagger/index.html**
* **SQL Server in Visual Studio Server Explorer-> localhost**   -- User Id => sa password => SwN12345678
 
Option Two
 * You can run directly from visual studio by selecting API
* **VTS API -> https://localhost:5001/index.html**
* **SQL Server in Visual Studio Server Explorer-> (localdb)\MSSQLLocalDB**   
Thank you.



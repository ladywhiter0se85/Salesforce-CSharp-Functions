# Salesforce-CSharp-Functions

## Overview

`Salesforce-CSharp-Functions` demonstrates how to integrate Salesforce with Azure Function Apps using C#. It generates C# class files based on Salesforce SObject metadata and provides a serverless solution for interacting with Salesforce data through Azure Functions. The project includes automated tests to validate function behavior and functionality.

## Technology Stack

- **Azure Function Apps**: Serverless execution of C# functions
- **.NET 8.0**
- **C#**
- **Salesforce**: CRM platform integration
- **Moq**: Mocking library for unit tests
- **xUnit**: Testing framework
- **Postman**: API testing
- **OpenAPI**: API documentation

## Project Structure

The project is organized into the following main areas:

- **OpenAPI/**  
  Generated OpenAPI specifications providing an overview of API endpoints using the `Microsoft.Azure.Functions.Worker.Extensions.OpenApi` extension.

- **Postman/**  
  Documentation assets, including the Postman collection and environment files for testing the Azure Functions.

- **Salesforce_Functions/**  
  Main application code, structured into folders:
  - `Functions`: Azure Function HTTP endpoints
  - `Services`: Request processing, orchestration, business logic and Salesforce interactions
  - `Models`: Salesforce SObject C# models (generated)
  - `Utilities`, `Configuration`, `Exceptions`: Supporting utilities and configuration classes

- **Tests/**  
  Test classes covering functions, handlers, services, and utilities.

### Example folder structure

```plaintext
Salesforce_Functions
├── Functions
├── Models
├── Services
├── Utilities
```

## Functionality Flow

- Incoming HTTP requests are received by Azure Function classes (Functions folder).
- Functions delegate request processing to SObject Services.
- SObject Services invoke the ApiService to perform Salesforce operations.
- Models represent Salesforce SObjects used for request and response serialization.
- Utilities assist with encryption, response formatting, and common tasks.
- Validation and custom exceptions standardize input validation and error handling.

## Setup

1. Clone the repository:

    ```bash
    git clone https://github.com/ladywhiter0se85/Salesforce-CSharp-Functions.git
    cd Salesforce-CSharp-Functions
    ```

2. Configure your local environment (`local.settings.json`) or Azure environment variables.

3. Generate SObject models:

    ```bash
    ./Salesforce_Functions/generate-sobject.sh
    ```

4. Build and run locally:

    ```bash
    dotnet build
    func start
    ```

5. Use the Postman collection in the `Postman/` folder to test the API endpoints locally or against deployed environments.

## SObject Generation

The `generate-sobject.sh` script is used to generate C# model classes based on Salesforce SObject metadata.

- Generated classes are placed in the `Models/` namespace under `Salesforce_Functions/`.
- If any references cannot be generated automatically, they are logged in a `missing_references.txt` file.
- For detailed instructions and troubleshooting, refer to `SObjectGenerator.md`.

## Testing

This project includes unit tests to validate application functionality.

- Unit tests verify individual components like functions, handlers, services, and utilities using mocked data with Moq.
- Integration tests (optional, if implemented) simulate request/response flows using local or mocked Salesforce environments.
# Salesforce-CSharp-Functions

## Overview

`Salesforce-CSharp-Functions` is a project that demonstrates how to integrate Salesforce with Azure Function Apps using C#. It generates C# class files based on Salesforce SObject metadata and provides a serverless solution for interacting with Salesforce data using Azure Functions. This project also includes automated tests for validating function behavior and functionality.

## Project Structure

The project is structured into the following folders:

### 1. **OpenAPI**

   - This folder contains the generated OpenAPI specification in JSON format. The OpenAPI specification is generated using the `Microsoft.Azure.Functions.Worker.Extensions.OpenApi` extension and provides an overview of the API endpoints exposed by the Azure Functions.

### 2. **Postman**

   - The `Postman` folder contains a collection and environment for testing the Azure Functions. You can import the collection into Postman to quickly test the functions and simulate Salesforce requests using the provided environment variables.

### 3. **Salesforce_Functions**

   - This folder contains the main C# Azure Function Apps for the Salesforce integration. It includes:
     - **Azure Functions**: Functions that process requests and interact with Salesforce data.
     - **Bash Script**: A script that generates C# class files for Salesforce SObjects by leveraging Salesforce metadata. This script automates the creation of strongly typed classes for working with Salesforce data in C#.

### 4. **Tests**

   - The `Tests` folder includes unit tests that use Moq and XUnit to validate the functionality of the Azure Functions and their associated services. The tests ensure that the logic works as expected and help maintain code quality.

# Salesforce Function App

## Description

This project is an Azure Function App API designed to integrate with Salesforce, with a focus on key SObjects such as **Account**, **Opportunity**, and **Contact**. It provides a structured interface to perform standard operations including:

- **Retrieving all records** (`GET`)
- **Fetching records by ID** (`GET`)
- **Filtering records dynamically** (`GET`)
- **Creating new records** (`POST`)
- **Updating existing records** (`PATCH`)
- **Upserting records using external identifiers** (`PUT`)

The goal of the app is to offer a lightweight, extendable backend service to interact with Salesforce programmatically, using clean endpoints and OpenAPI documentation.


---

## Table of Contents

- [Salesforce Function App](#salesforce-function-app)
  - [Description](#description)
  - [Table of Contents](#table-of-contents)
  - [Current Functions](#current-functions)
  - [Function Breakdown](#function-breakdown)
    - [GET - `/accounts`](#get---accounts)
      - [Output Schema](#output-schema)
    - [GET - `/accounts/id/{accountId}`](#get---accountsidaccountid)
      - [Parameters](#parameters)
      - [Output Schema](#output-schema-1)
    - [GET - `/accounts/filter/{where}`](#get---accountsfilterwhere)
      - [Parameters](#parameters-1)
      - [Output Schema](#output-schema-2)
    - [POST - `/accounts`](#post---accounts)
      - [Input Schema](#input-schema)
      - [Output Schema](#output-schema-3)
    - [PATCH - `/accounts`](#patch---accounts)
      - [Input Schema](#input-schema-1)
      - [Output Schema](#output-schema-4)
    - [PUT - `/accounts/external`](#put---accountsexternal)
      - [Parameters](#parameters-2)
      - [Input Schema](#input-schema-2)
      - [Output Schema](#output-schema-5)


## Current Functions

This Function App exposes the following endpoints:

1. **GET** `/accounts` – Returns a list of Salesforce Account objects  
2. **GET** `/accounts/id/{accountId}` – Returns a Salesforce Account object  
3. **GET** `/accounts/filter/{where}` – Returns a list of Salesforce Account objects  
4. **POST** `/accounts` – Returns a list of OperationResponse
5. **PATCH** `/accounts` – Returns a list of OperationResponse
6. **PUT** `/accounts/external` – Returns a list of OperationResponse

---

## Function Breakdown

### GET - `/accounts`

Returns a list of Salesforce `Account` objects.

#### Output Schema

```json
[
    {
        "attributes": {
            "type": "Account",
            "url": "mockedURLAccountOne"
        },
        "id": "mockedAccountOneId",
        "name": "Mocked Account One",
        "type": "Customer - Direct",
        "billingAddress": {
            "street": "312 Constitution Place\nAustin, TX 78767\nUSA",
            "city": "Austin",
            "state": "TX",
            "country": "United States",
            "stateCode": "TX",
            "countryCode": "US"
        },
        "phone": "(512) 757-6000",
        "description": "Description for Mocked Account One.",
        "active__c": "Yes"
    },
    {
        "attributes": {
            "type": "Account",
            "url": "mockedURLAccountTwo"
        },
        "id": "mockedAccountTwoId",
        "name": "Mocked Account Two",
        "type": "Customer - Direct",
        "billingAddress": {
            "street": "525 S. Lexington Ave",
            "city": "Burlington",
            "state": "NC",
            "postalCode": "27215",
            "country": "USA",
            "stateCode": "NC",
            "countryCode": "US"
        },
        "phone": "(336) 222-7000"
    }
]
```

### GET - `/accounts/id/{accountId}`

Returns a Salesforce `Account` object.

#### Parameters

| Parameter Name | Type   | Required | In    | Example            |
|----------------|--------|----------|-------|--------------------|
| accountId      | string | true     | path  | mockedAccountOneId |

#### Output Schema

```json
[
    {
        "attributes": {
            "type": "Account",
            "url": "mockedURLAccountOne"
        },
        "id": "mockedAccountOneId",
        "name": "Mocked Account One",
        "type": "Customer - Direct",
        "billingAddress": {
            "street": "312 Constitution Place\nAustin, TX 78767\nUSA",
            "city": "Austin",
            "state": "TX",
            "country": "United States",
            "stateCode": "TX",
            "countryCode": "US"
        },
        "phone": "(512) 757-6000",
        "description": "Description for Mocked Account One.",
        "active__c": "Yes"
    }
]
```

### GET - `/accounts/filter/{where}`

Returns a list of Salesforce `Account` objects.

#### Parameters

| Parameter Name | Type   | Required | In    | Example         |
|----------------|--------|----------|-------|-----------------|
| where          | string | true     | path  | BillingState:CA |
| isAnd          | bool   | true     | query | true            |

#### Output Schema

```json
[
    {
        "attributes": {
            "type": "Account",
            "url": "mockedURLAccountOne"
        },
        "id": "mockedAccountOneId",
        "name": "Mocked Account One",
        "type": "Customer - Direct",
        "billingAddress": {
            "street": "312 Constitution Place\nAustin, TX 78767\nUSA",
            "city": "Austin",
            "state": "TX",
            "country": "United States",
            "stateCode": "TX",
            "countryCode": "US"
        },
        "phone": "(512) 757-6000",
        "description": "Description for Mocked Account One.",
        "active__c": "Yes"
    },
    {
        "attributes": {
            "type": "Account",
            "url": "mockedURLAccountTwo"
        },
        "id": "mockedAccountTwoId",
        "name": "Mocked Account Two",
        "type": "Customer - Direct",
        "billingAddress": {
            "street": "525 S. Lexington Ave",
            "city": "Burlington",
            "state": "NC",
            "postalCode": "27215",
            "country": "USA",
            "stateCode": "NC",
            "countryCode": "US"
        },
        "phone": "(336) 222-7000"
    }
]
```

### POST - `/accounts`

Returns a list of `Operation Response` objects.

#### Input Schema

```json
[
    {
        "attributes": {
            "type": "Account"
        },
        "name": "Mocked Salesforce Account",
        "phone": "588-454-5857",
        "description": "Mocked Salesforce Account Creation"
    }
]
```

#### Output Schema

```json
[
    {
        "id": "mockedAccountId",
        "success": true
    }
]
```

### PATCH - `/accounts`

Returns a list of `Operation Response` objects.

#### Input Schema

```json
[
    {
        "attributes": {
            "type": "Account"
        },
        "id": "mockedAccountId",
        "description": "Mocked Salesforce Account Update"
    }
]
```

#### Output Schema

```json
[
    {
        "id": "mockedAccountId",
        "success": true
    }
]
```

### PUT - `/accounts/external`

Returns a list of `Operation Response` objects.

#### Parameters

| Parameter Name | Type   | Required | In    | Example |
|----------------|--------|----------|-------|---------|
| externalField  | string | true     | path  | Id      |

#### Input Schema

```json
[
    {
        "attributes": {
            "type": "Account"
        },
        "id": "mockedExistingAccountId",
        "description": "Mocked Salesforce Account Upsert"
    },
    {
        "attributes": {
            "type": "Account"
        },
        "name": "Mocked Salesforce Account",
        "description": "Mocked Salesforce Account Creation",
        "phone": "252-585-6896"
    }
]
```

#### Output Schema

```json
[
    {
        "id": "mockedAccountId",
        "success": true
    },
    {
        "id": "mockedAccountIdTwo",
        "success": true,
        "created": true
    }
]
```
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
    - [GET - `/opportunities`](#get---opportunities)
      - [Output Schema](#output-schema-6)
    - [GET - `/opportunities/id/{opportunityId}`](#get---opportunitiesidopportunityid)
      - [Parameters](#parameters-3)
      - [Output Schema](#output-schema-7)
    - [GET - `/opportunities/filter/{where}`](#get---opportunitiesfilterwhere)
      - [Parameters](#parameters-4)
      - [Output Schema](#output-schema-8)
    - [POST - `/opportunities`](#post---opportunities)
      - [Input Schema](#input-schema-3)
      - [Output Schema](#output-schema-9)
    - [PATCH - `/opportunities`](#patch---opportunities)
      - [Input Schema](#input-schema-4)
      - [Output Schema](#output-schema-10)
    - [PUT - `/opportunities/external`](#put---opportunitiesexternal)
      - [Parameters](#parameters-5)
      - [Input Schema](#input-schema-5)
      - [Output Schema](#output-schema-11)
    - [GET - `/contacts`](#get---contacts)
      - [Output Schema](#output-schema-12)
    - [GET - `/contacts/id/{contactId}`](#get---contactsidcontactid)
      - [Parameters](#parameters-6)
      - [Output Schema](#output-schema-13)
    - [GET - `/contacts/filter/{where}`](#get---contactsfilterwhere)
      - [Parameters](#parameters-7)
      - [Output Schema](#output-schema-14)
    - [POST - `/contacts`](#post---contacts)
      - [Input Schema](#input-schema-6)
      - [Output Schema](#output-schema-15)
    - [PATCH - `/contacts`](#patch---contacts)
      - [Input Schema](#input-schema-7)
      - [Output Schema](#output-schema-16)
    - [PUT - `/contacts/external`](#put---contactsexternal)
      - [Parameters](#parameters-8)
      - [Input Schema](#input-schema-8)
      - [Output Schema](#output-schema-17)
    - [GET - `/describes`](#get---describes)
      - [Parameters](#parameters-9)
      - [Output Schema](#output-schema-18)

## Current Functions

This Function App exposes the following endpoints:

1. **GET** `/accounts` – Returns a list of Salesforce Account objects  
2. **GET** `/accounts/id/{accountId}` – Returns a Salesforce Account object  
3. **GET** `/accounts/filter/{where}` – Returns a list of Salesforce Account objects  
4. **POST** `/accounts` – Returns a list of OperationResponse
5. **PATCH** `/accounts` – Returns a list of OperationResponse
6. **PUT** `/accounts/external` – Returns a list of OperationResponse
7. **GET** `/opportunities` – Returns a list of Salesforce Opportunity objects
8. **GET** `/opportunities/id/{opportunityId}` – Returns a Salesforce Opportunity object  
9. **GET** `/opportunities/filter/{where}` – Returns a list of Salesforce Opportunity objects  
10. **POST** `/opportunities` – Returns a list of OperationResponse
11. **PATCH** `/opportunities` – Returns a list of OperationResponse
12. **PUT** `/opportunities/external` – Returns a list of OperationResponse
13. **GET** `/contacts` – Returns a list of Salesforce Contact objects
14. **GET** `/contacts/id/{contactId}` – Returns a Salesforce Contact object  
15. **GET** `/contacts/filter/{where}` – Returns a list of Salesforce Contact objects  
16. **POST** `/contacts` – Returns a list of OperationResponse
17. **PATCH** `/contacts` – Returns a list of OperationResponse
18. **PUT** `/contacts/external` – Returns a list of OperationResponse
19. **GET** `/describes` – Returns a list of Salesforce Describe objects

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

### GET - `/opportunities`

Returns a list of Salesforce `Opportunity` objects.

#### Output Schema

```json
[
  {
    "attributes": {
      "type": "Opportunity"
    },
    "id": "mockedOpportunityIdOne",
    "accountId": "mockedAccountIdOne",
    "name": "Mocked Opportunity One",
    "stageName": "Qualification",
    "amount": 15000,
    "closeDate": "2025-02-01T00:00:00",
    "description": "Description for Mocked Opportunity One.",
    "type": "New Customer"
  },
  {
    "attributes": {
      "type": "Opportunity"
    },
    "id": "mockedOpportunityIdTwo",
    "accountId": "mockedAccountIdTwo",
    "name": "Mocked Opportunity Two",
    "stageName": "Negotiation/Review",
    "amount": 125000,
    "closeDate": "2025-01-20T00:00:00",
    "description": "Description for Mocked Opportunity Two.",
    "type": "Existing Customer - Upgrade"
  }
]
```

### GET - `/opportunities/id/{opportunityId}`

Returns a Salesforce `Opportunity` object.

#### Parameters

| Parameter Name | Type   | Required | In    | Example            |
|----------------|--------|----------|-------|--------------------|
| opportunityId      | string | true     | path  | mockedOpportunityOneId |

#### Output Schema

```json
{
  "attributes": {
    "type": "Opportunity"
  },
  "id": "mockedOpportunityIdOne",
  "accountId": "mockedAccountIdOne",
  "name": "Mocked Opportunity One",
  "stageName": "Qualification",
  "amount": 15000,
  "closeDate": "2025-02-01T00:00:00",
  "description": "Description for Mocked Opportunity One.",
  "type": "New Customer"
}
```

### GET - `/opportunities/filter/{where}`

Returns a list of Salesforce `Opportunity` objects.

#### Parameters

| Parameter Name | Type   | Required | In    | Example         |
|----------------|--------|----------|-------|-----------------|
| where          | string | true     | path  | stageName:Qualification |
| isAnd          | bool   | true     | query | true            |

#### Output Schema

```json
[
  {
    "attributes": {
      "type": "Opportunity"
    },
    "id": "mockedOpportunityIdOne",
    "accountId": "mockedAccountIdOne",
    "name": "Mocked Opportunity One",
    "stageName": "Qualification",
    "amount": 15000,
    "closeDate": "2025-02-01T00:00:00",
    "description": "Description for Mocked Opportunity One.",
    "type": "New Customer"
  },
  {
    "attributes": {
      "type": "Opportunity"
    },
    "id": "mockedOpportunityIdTwo",
    "accountId": "mockedAccountIdTwo",
    "name": "Mocked Opportunity Two",
    "stageName": "Negotiation/Review",
    "amount": 125000,
    "closeDate": "2025-01-20T00:00:00",
    "description": "Description for Mocked Opportunity Two.",
    "type": "Existing Customer - Upgrade"
  }
]
```

### POST - `/opportunities`

Returns a list of `Operation Response` objects.

#### Input Schema

```json
[
    {
        "attributes": {
            "type": "Opportunity"
        },
        "name": "Mocked Salesforce Opportunity",
        "description": "Mocked Salesforce Opportunity Creation",
        "stageName": "Qualification",
        "closeDate": "2025-04-15"
    }
]
```

#### Output Schema

```json
[
    {
        "id": "mockedOpportunityId",
        "success": true
    }
]
```

### PATCH - `/opportunities`

Returns a list of `Operation Response` objects.

#### Input Schema

```json
[
    {
        "attributes": {
            "type": "Opportunity"
        },
        "id": "mockedOpportunityId",
        "description": "Mocked Salesforce Opportunity Update"
    }
]
```

#### Output Schema

```json
[
    {
        "id": "mockedOpportunityId",
        "success": true
    }
]
```

### PUT - `/opportunities/external`

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
            "type": "Opportunity"
        },
        "id": "mockedExistingOpportunityId",
        "description": "Mocked Salesforce Opportunity Upsert"
    },
    {
        "attributes": {
            "type": "Opportunity"
        },
        "name": "Mocked Salesforce Opportunity",
        "description": "Mocked Salesforce Opportunity Creation",
        "stageName": "Needs Analysis",
        "closeDate": "2025-04-15",
    }
]
```

#### Output Schema

```json
[
    {
        "id": "mockedOpportunityId",
        "success": true
    },
    {
        "id": "mockedOpportunityIdTwo",
        "success": true,
        "created": true
    }
]
```

### GET - `/contacts`

Returns a list of Salesforce `Contact` objects.

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

### GET - `/contacts/id/{contactId}`

Returns a Salesforce `Contact` object.

#### Parameters

| Parameter Name | Type   | Required | In    | Example            |
|----------------|--------|----------|-------|--------------------|
| contactId      | string | true     | path  | mockedContactOneId |

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

### GET - `/contacts/filter/{where}`

Returns a list of Salesforce `Contact` objects.

#### Parameters

| Parameter Name | Type   | Required | In    | Example         |
|----------------|--------|----------|-------|-----------------|
| where          | string | true     | path  | MailingState:CA |
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

### POST - `/contacts`

Returns a list of `Operation Response` objects.

#### Input Schema

```json
[
    {
        "attributes": {
            "type": "Contact"
        },
        "firstName": "Mocked",
        "lastName": "Contact One",
        "phone": "588-454-5857",
        "email": "mockedContactOne@email.com",
        "description": "Mocked Contact One Creation",
        "mailingStreet": "800 Kalamalka Lake Rd",
        "mailingCity": "Vernon",
        "mailingPostalCode": "V1T 6V2",
        "mailingCountry": "Canada"
    }
]
```

#### Output Schema

```json
[
    {
        "id": "mockedContactId",
        "success": true
    }
]
```

### PATCH - `/contacts`

Returns a list of `Operation Response` objects.

#### Input Schema

```json
[
    {
      "attributes": {
        "type": "Contact"
      },
      "id": "mockedContactId",
      "description": "Mocked Contact One Update"
    }
]
```

#### Output Schema

```json
[
    {
        "id": "mockedContactId",
        "success": true
    }
]
```

### PUT - `/contacts/external`

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
      "type": "Contact"
    },
    "id": "mockedContactId",
    "description": "Mocked Contact One Upsert"
  },
  {
    "attributes": {
      "type": "Contact"
    },
    "firstName": "Mocked",
    "lastName": "Contact Two",
    "phone": "588-453-2211",
    "email": "mockedContactTwo@email.com",
    "description": "Mocked Contact Two Creation",
    "mailingStreet": "1000 K. L. O. Rd",
    "mailingCity": "Kelowna",
    "mailingPostalCode": "V1Y 4X8",
    "mailingCountry": "Canada"
  }
]
```

#### Output Schema

```json
[
    {
        "id": "mockedContactId",
        "success": true
    },
    {
        "id": "mockedContactIdTwo",
        "success": true,
        "created": true
    }
]
```

### GET - `/describes`

Returns a list of Salesforce `Describe` objects.

#### Parameters

| Parameter Name | Type   | Required | In    | Example         |
|----------------|--------|----------|-------|-----------------|
| sObjects          | string   | true     | query | Contact,Opportunity            |

#### Output Schema

```json
{
    "account": [
        {
            "name": "Id",
            "label": "Account ID",
            "type": "id",
            "referenceTo": [],
            "relationshipName": ""
        },
        {
            "name": "IsDeleted",
            "label": "Deleted",
            "type": "boolean",
            "referenceTo": [],
            "relationshipName": ""
        },
        {
            "name": "MasterRecordId",
            "label": "Master Record ID",
            "type": "reference",
            "nillable": true,
            "referenceTo": [
                "Account"
            ],
            "relationshipName": "MasterRecord"
        },
        {
            "name": "Name",
            "label": "Account Name",
            "type": "string",
            "createable": true,
            "updateable": true,
            "referenceTo": [],
            "relationshipName": ""
        },
        {
            "name": "Type",
            "label": "Account Type",
            "type": "picklist",
            "nillable": true,
            "createable": true,
            "updateable": true,
            "referenceTo": [],
            "relationshipName": ""
        }
    ],
    "opportunity": [
        {
            "name": "Id",
            "label": "Opportunity ID",
            "type": "id",
            "referenceTo": [],
            "relationshipName": ""
        },
        {
            "name": "IsDeleted",
            "label": "Deleted",
            "type": "boolean",
            "referenceTo": [],
            "relationshipName": ""
        },
        {
            "name": "AccountId",
            "label": "Account ID",
            "type": "reference",
            "nillable": true,
            "createable": true,
            "updateable": true,
            "referenceTo": [
                "Account"
            ],
            "relationshipName": "Account"
        },
        {
            "name": "IsPrivate",
            "label": "Private",
            "type": "boolean",
            "createable": true,
            "updateable": true,
            "referenceTo": [],
            "relationshipName": ""
        },
        {
            "name": "Name",
            "label": "Name",
            "type": "string",
            "createable": true,
            "updateable": true,
            "referenceTo": [],
            "relationshipName": ""
        },
        {
            "name": "Description",
            "label": "Description",
            "type": "textarea",
            "nillable": true,
            "createable": true,
            "updateable": true,
            "referenceTo": [],
            "relationshipName": ""
        },
        {
            "name": "StageName",
            "label": "Stage",
            "type": "picklist",
            "createable": true,
            "updateable": true,
            "referenceTo": [],
            "relationshipName": ""
        },
        {
            "name": "Amount",
            "label": "Amount",
            "type": "currency",
            "nillable": true,
            "createable": true,
            "updateable": true,
            "referenceTo": [],
            "relationshipName": ""
        },
        {
            "name": "CloseDate",
            "label": "Close Date",
            "type": "date",
            "createable": true,
            "updateable": true,
            "referenceTo": [],
            "relationshipName": ""
        },
        {
            "name": "Type",
            "label": "Opportunity Type",
            "type": "picklist",
            "nillable": true,
            "createable": true,
            "updateable": true,
            "referenceTo": [],
            "relationshipName": ""
        },
        {
            "name": "LeadSource",
            "label": "Lead Source",
            "type": "picklist",
            "nillable": true,
            "createable": true,
            "updateable": true,
            "referenceTo": [],
            "relationshipName": ""
        },
        {
            "name": "IsClosed",
            "label": "Closed",
            "type": "boolean",
            "referenceTo": [],
            "relationshipName": ""
        },
        {
            "name": "Pricebook2Id",
            "label": "Price Book ID",
            "type": "reference",
            "nillable": true,
            "createable": true,
            "updateable": true,
            "referenceTo": [
                "Pricebook2"
            ],
            "relationshipName": "Pricebook2"
        },
        {
            "name": "OwnerId",
            "label": "Owner ID",
            "type": "reference",
            "createable": true,
            "updateable": true,
            "referenceTo": [
                "User"
            ],
            "relationshipName": "Owner"
        },
        {
            "name": "ContactId",
            "label": "Contact ID",
            "type": "reference",
            "nillable": true,
            "createable": true,
            "referenceTo": [
                "Contact"
            ],
            "relationshipName": ""
        }
    ]
}
```

# FormulaOne Web API Documentation

## Base URL

`/api`

## Endpoints

### Teams

#### `GET /Teams`

Retrieves a list of teams.

**Query Parameters:**

* `Id`: A single GUID or comma-separated list of GUIDs for bulk fetching.
* `Name`: Filters teams by name.
* `SortField`: Specifies the field to sort by. Allowed values: `name`.
* `SortOrder`: Specifies the sort order. Allowed values: `asc`, `desc`. Defaults to `asc`.

**Example:**

* `GET /Teams?Name=Ferrari`

**Example Response:**

```json
{
  "currentPage": 1,
  "totalPages": 1,
  "pageSize": 20,
  "totalResults": 18,
  "errors": [],
  "items": [
    {
      "id": "C2C756D5-0B57-4D2D-97D6-08BB8E4CC996",
      "name": "Marussia Ferrari"
    },
    {
      "id": "FB85D1D7-0DC4-4DE8-BED7-0AC151EEF5CE",
      "name": "RBR Ferrari"
    }
  ]
}
```

#### `GET /Teams/Standings`

Retrieves team standings.

**Query Parameters:**

* `Id`: A single GUID or comma-separated list of GUIDs for bulk fetching.
* `TeamId`: A single GUID or comma-separated list of GUIDs for bulk fetching.
* `TeamName`: Filters standings by team name.
* `Year`: Filters standings by year (1958-2024).
* `SortField`: Specifies the field to sort by. Allowed values: `year`, `position`, `points`.
* `SortOrder`: Specifies the sort order. Allowed values: `asc`, `desc`. Defaults to `asc`.

**Example:**

* `GET /Teams/Standings?TeamName=mclaren&SortField=points&SortOrder=desc&PageSize=10`

**Example Response:**

```json
{
  "currentPage": 1,
  "totalPages": 7,
  "pageSize": 10,
  "totalResults": 61,
  "errors": [],
  "items": [
    {
      "id": "4199A31C-0E86-49F4-AD7C-02D1B28A26FB",
      "year": 2024,
      "position": 1,
      "teamId": "D96E8F12-5B29-4E9C-AA8E-897FEF449EC2",
      "teamName": "McLaren Mercedes",
      "points": 666
    },
    {
      "id": "E464C8AD-9F24-4E3F-9809-2EA064730A74",
      "year": 2011,
      "position": 2,
      "teamId": "D96E8F12-5B29-4E9C-AA8E-897FEF449EC2",
      "teamName": "McLaren Mercedes",
      "points": 497
    }
  ]
}
```

#### `GET /Teams/Results`

Retrieves team race results.

**Query Parameters:**

* `Id`: A single GUID or comma-separated list of GUIDs for bulk fetching.
* `TeamId`: A single GUID or comma-separated list of GUIDs for bulk fetching.
* `TeamName`: Filters results by team name.
* `Year`: Filters results by year (1950-2024).
* `SortField`: Specifies the field to sort by. Allowed values: `position`, `points`, `date`.
* `SortOrder`: Specifies the sort order. Allowed values: `asc`, `desc`. Defaults to `asc`.

**Example:**

* `GET /Teams/Results?TeamName=aston&Year=2023&SortField=date

**Example Response:**

```json
{
  "currentPage": 1,
  "totalPages": 3,
  "pageSize": 20,
  "totalResults": 43,
  "errors": [],
  "items": [
    {
      "id": "05758CA6-E398-4D58-B6D1-4885BA90F588",
      "position": 3,
      "date": "2023-03-05",
      "circuitId": "9D85AFC3-AF78-4CD6-AE07-57D22E7C1525",
      "circuitName": "Bahrain International Circuit",
      "driverId": "95AF2E07-EDAC-4860-BE00-9075D8D28A59",
      "driverName": "Fernando Alonso",
      "teamId": "C46DAFC4-5105-447C-B18B-3E8CE084141F",
      "teamName": "Aston Martin Aramco Mercedes",
      "laps": 57,
      "time": "+38.637s",
      "points": 15
    },
    {
      "id": "A054636E-30E0-4F70-87E4-5C96F745E78D",
      "position": 6,
      "date": "2023-03-05",
      "circuitId": "9D85AFC3-AF78-4CD6-AE07-57D22E7C1525",
      "circuitName": "Bahrain International Circuit",
      "driverId": "C356FF3B-059D-414F-A413-B8D9AF9E809E",
      "driverName": "Lance Stroll",
      "teamId": "C46DAFC4-5105-447C-B18B-3E8CE084141F",
      "teamName": "Aston Martin Aramco Mercedes",
      "laps": 57,
      "time": "+54.502s",
      "points": 8
    }
  ]
}
```

### Drivers

#### `GET /Drivers`

Retrieves a list of drivers.

**Query Parameters:**

* `Id`: A single GUID or comma-separated list of GUIDs for bulk fetching.
* `Name`: Filters drivers by name.
* `Nationality`: Filters drivers by nationality (three-letter code, e.g., ITA, GER, POL).
* `SortField`: Specifies the field to sort by. Allowed values: `firstname`, `lastname`, `nationality`.
* `SortOrder`: Specifies the sort order. Allowed values: `asc`, `desc`. Defaults to `asc`.

**Example:**

* `GET /Drivers?Name=jack`

**Example Response:**

```json
{
  "currentPage": 1,
  "totalPages": 1,
  "pageSize": 20,
  "totalResults": 13,
  "errors": [],
  "items": [
    {
      "id": "6EC08059-D390-4603-97F5-18339AD474E3",
      "name": "Jack Doohan",
      "nationality": "AUS"
    },
    {
      "id": "2CC291F4-C471-41C9-A0C0-3935E67B3F07",
      "name": "Jackie Stewart",
      "nationality": "GBR"
    }
  ]
}
```

#### `GET /Drivers/Standings`

Retrieves driver standings.

**Query Parameters:**

* `Id`: A single GUID or comma-separated list of GUIDs for bulk fetching.
* `DriverId`: A single GUID or comma-separated list of GUIDs for bulk fetching.
* `Year`: Filters standings by year (1950-2024).
* `SortField`: Specifies the field to sort by. Allowed values: `year`, `position`, `points`.
* `SortOrder`: Specifies the sort order. Allowed values: `asc`, `desc`. Defaults to `asc`.

**Example:**

* `GET /Drivers/Standings?DriverId=54838C10-9492-4731-9B6D-F9EC8CB97864&SortField=year&SortOrder=desc`

**Example Response:**

```json
{
  "currentPage": 1,
  "totalPages": 1,
  "pageSize": 20,
  "totalResults": 10,
  "errors": [],
  "items": [
    {
      "id": "78B77B26-CBCE-45E0-B476-F9812768550C",
      "position": 1,
      "driverId": "54838C10-9492-4731-9B6D-F9EC8CB97864",
      "driverName": "Max Verstappen",
      "nationality": "NED",
      "teamId": "14BE59E1-FC4E-48A1-9BDF-7DBBC6F6D0A6",
      "teamName": "Red Bull Racing Honda RBPT",
      "points": 437,
      "year": 2024
    },
    {
      "id": "010FFF13-D30D-42A4-9FA5-E9AC93800871",
      "position": 1,
      "driverId": "54838C10-9492-4731-9B6D-F9EC8CB97864",
      "driverName": "Max Verstappen",
      "nationality": "NED",
      "teamId": "14BE59E1-FC4E-48A1-9BDF-7DBBC6F6D0A6",
      "teamName": "Red Bull Racing Honda RBPT",
      "points": 575,
      "year": 2023
    }
  ]
}
```

#### `GET /Drivers/Results`

Retrieves driver race results.

**Query Parameters:**

* `Id`: A single GUID or comma-separated list of GUIDs for bulk fetching.
* `DriverId`: A single GUID or comma-separated list of GUIDs for bulk fetching.
* `DriverName`: Filters results by driver name.
* `Year`: Filters results by year (1950-2024).
* `SortField`: Specifies the field to sort by. Allowed values: `position`, `points`, `date`.
* `SortOrder`: Specifies the sort order. Allowed values: `asc`, `desc`. Defaults to `asc`.

**Example:**

* `GET /Drivers/Results?DriverName=Ayrton&Year=1993&SortField=points&SortOrder=desc`

**Example Response:**

```json
{
  "currentPage": 1,
  "totalPages": 1,
  "pageSize": 20,
  "totalResults": 16,
  "errors": [],
  "items": [
    {
      "id": "CA74EAA6-FDFD-4828-89A0-26E4FFFCF4F5",
      "position": 1,
      "date": "1993-05-23",
      "circuitId": "BC7E96A8-687B-4474-AB97-F5C6310C6760",
      "circuitName": "Circuit de Monaco",
      "driverId": "17B64B57-577F-4B8A-AE28-D2EDF5567C45",
      "driverName": "Ayrton Senna",
      "teamId": "0BD521C4-F93B-401D-9E41-B5159A6119FD",
      "teamName": "McLaren Ford",
      "laps": 78,
      "time": "1:52:10.947",
      "points": 10
    },
    {
      "id": "5E73456A-8FCF-4919-A20B-31A82D3B9EEF",
      "position": 1,
      "date": "1993-03-28",
      "circuitId": "887DECF4-F97F-4684-B14F-63C6A8E536DA",
      "circuitName": "Autódromo José Carlos Pace",
      "driverId": "17B64B57-577F-4B8A-AE28-D2EDF5567C45",
      "driverName": "Ayrton Senna",
      "teamId": "0BD521C4-F93B-401D-9E41-B5159A6119FD",
      "teamName": "McLaren Ford",
      "laps": 71,
      "time": "1:51:15.485",
      "points": 10
    }
  ]
}
```

### Circuits

#### `GET /Circuits`

Retrieves a list of circuits.

**Query Parameters:**

* `Id`: A single GUID or comma-separated list of GUIDs for bulk fetching.
* `Name`: Filters circuits by name.
* `Location`: Filters circuits by location.
* `SortField`: Specifies the field to sort by. Allowed values: `name`, `location`.
* `SortOrder`: Specifies the sort order. Allowed values: `asc`, `desc`. Defaults to `asc`.

**Example:**

* `GET /Circuits?Location=australia`

**Example Response:**

```json
{
  "currentPage": 1,
  "totalPages": 1,
  "pageSize": 20,
  "totalResults": 2,
  "errors": [],
  "items": [
    {
      "id": "636C18B4-C2AF-4C6E-914A-506BAE24AA28",
      "name": "Albert Park Grand Prix Circuit",
      "location": "Australia"
    },
    {
      "id": "0B1B5AFF-4D47-4726-8126-8FF6F1B5273E",
      "name": "Adelaide Street Circuit",
      "location": "Australia"
    }
  ]
}
```

#### `GET /Circuits/Results`

Retrieves circuit race results.

**Query Parameters:**

* `Id`: A single GUID or comma-separated list of GUIDs for bulk fetching.
* `CircuitId`: A single GUID.
* `CircuitName`: Filters results by circuit name.
* `CircuitLocation`: Filters results by circuit location.
* `Year`: Filters results by year (1950-2024).
* `SortField`: Specifies the field to sort by. Allowed values: `position`, `points`, `date`.
* `SortOrder`: Specifies the sort order. Allowed values: `asc`, `desc`. Defaults to `asc`.

**Example:**

* `GET /Circuits/Results?CircuitName=Monza&Year=1999&SortField=position&SortOrder=asc`

**Example Response:**

```json
{
  "currentPage": 1,
  "totalPages": 2,
  "pageSize": 20,
  "totalResults": 22,
  "errors": [],
  "items": [
    {
      "id": "F53DFB8E-79FB-451C-B8E8-77DE071BDA3C",
      "position": 1,
      "date": "1999-09-12",
      "circuitId": "A52DD76E-0463-42FB-80DA-F57EA623DC01",
      "circuitName": "Autodromo Nazionale Monza",
      "driverId": "5413D9F1-351F-4BED-A684-F8DC6461C13E",
      "driverName": "Heinz-Harald Frentzen",
      "teamId": "5881457F-9D34-4678-9812-D785C3B12E3E",
      "teamName": "Jordan Mugen Honda",
      "laps": 53,
      "time": "1:17:02.923",
      "points": 10
    },
    {
      "id": "C90FADF1-5C25-4EEF-8845-4BB967A00942",
      "position": 2,
      "date": "1999-09-12",
      "circuitId": "A52DD76E-0463-42FB-80DA-F57EA623DC01",
      "circuitName": "Autodromo Nazionale Monza",
      "driverId": "2F9054E3-BE4E-49C6-9DC8-3918E8D57A2E",
      "driverName": "Ralf Schumacher",
      "teamId": "C7E38BB4-75B8-446F-9542-6BCB2EAD2194",
      "teamName": "Williams Supertec",
      "laps": 53,
      "time": "+3.272s",
      "points": 6
    }
  ]
}
```

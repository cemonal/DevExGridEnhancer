# DevExGridEnhancer

**DevExGridEnhancer** is a .NET Standard 2.0 library designed to enhance the sorting and filtering capabilities of your data grids. It simplifies data parsing and offers support for IQueryable, making data processing more efficient and straightforward.

## Features

- **Advanced Sorting**: Easily sort data based on multiple columns with both ascending and descending orders.
- **Flexible Filtering**: Apply filters to your data using various comparison operators, allowing complex criteria.
- **IQueryable Support**: Leverage IQueryable for optimal performance and database integration.

# DevExGridEnhancer Example

This is an example of how to use the **DevExGridEnhancer** library to parse and process sorting and filtering parameters for a data grid.

## Usage

### Sorting

To sort data based on one or more columns, provide a JSON string in the following format:

```json
[
  {
    "selector": "firstName",
    "desc": false
  },
  {
    "selector": "lastName",
    "desc": false
  }
]
```

In this example, we are sorting by the `firstName` column in ascending order (A-Z) and then by the `lastName` column in ascending order as well.

To apply sorting to your data using the **DevExGridEnhancer** library, use the `OrderBy` extension method as shown below:

```csharp
// Sorting JSON string
string sortJson = "sort=[{\"selector\":\"firstName\",\"desc\":false},{\"selector\":\"lastName\",\"desc\":false}]";

// Parse sorting information
var sortModels = DataGridSortParser.ParseSorts(sortJson);

// Apply sorting to your data
var sortedData = sourceData.OrderBy(sortModels);
```

### Filtering

To filter data based on one or more criteria, provide a JSON string in the following format:

```json
[
  [
    "firstName",
    "contains",
    "Cem"
  ],
  "and",
  [
    "lastName",
    "contains",
    "Önal"
  ],
  "and",
  [
    "country.name",
    "contains",
    "Turkey"
  ]
]
```

In this example, we are filtering data where the `firstName` contains "Cem," the `lastName` contains "Önal," and the country.name contains "Turkey."

To apply filtering to your data using the **DevExGridEnhancer** library, use the `Where` extension method as shown below:

```csharp
// Filtering JSON string
string filterJson = "filter=[[\"firstName\",\"contains\",\"Cem\"],\"and\",[\"lastName\",\"contains\",\"Önal\"],\"and\",[\"country.name\",\"contains\",\"Turkey\"]]";

// Parse filtering information
var filterModels = DataGridFilterParser.ParseFilters(filterJson);

// Apply filtering to your data
var filteredData = sourceData.Where(filterModels);
```

## Contributing

Contributions are welcome! Feel free to submit issues, pull requests, or feedback in the GitHub repository.

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

## Supported Filter Comparators

DevExGridEnhancer supports a variety of filter comparators that you can use to filter your data. These comparators enable you to define different filter criteria based on your requirements. Below is a list of the supported filter comparators and their descriptions:

- **Equals (`=`)**: Filters data where the specified property is equal to the given value.
- **Not Equal (`!=` or `<>`)**: Filters data where the specified property is not equal to the given value.
- **Contains**: Filters data where the specified property contains the given value.
- **Not Contains**: Filters data where the specified property does not contain the given value.
- **Greater Than (`>`)**: Filters data where the specified property is greater than the given value.
- **Greater Than or Equal (`>=`)**: Filters data where the specified property is greater than or equal to the given value.
- **Less Than (`<`)**: Filters data where the specified property is less than the given value.
- **Less Than or Equal (`<=`)**: Filters data where the specified property is less than or equal to the given value.

You can use these filter comparators when constructing your filtering criteria in the JSON format, allowing you to create powerful and flexible filters for your data grids.

### Example

Here's an example of how to use the "Equals" filter comparator to filter data where the "status" property is equal to "Active":

```json
[
  ["status", "=", "Active"]
]
```

Feel free to mix and match these filter comparators to meet your specific filtering needs. They provide a versatile way to filter and refine your data.


## Contributing

Contributions are welcome! Feel free to submit issues, pull requests, or feedback in the GitHub repository.

## Welcome to the Musical Instrument Store backroom database!
Here you will be able to search for musical instruments by type, brand, price, past
purchase history, quantity in stock, and much more!

To test the functionality of the application, please clone the repository and run the
migrations to create the database. There are entries filled with seed data provided.

### To run this application you will need to install the following NuGet packages:
#### AutoMapper: [nuget](https://www.nuget.org/packages/AutoMapper/)
#### Entity Framework: [nuget](https://www.nuget.org/packages/EntityFramework/)
#### Entity Framework Core: [nuget](https://www.nuget.org/packages/EntityFrameworkCore/)
#### Entity Framework Core Design: [nuget](https://www.nuget.org/packages/EntityFrameworkCore.Design/)
#### Pomelo.EntityFrameworkCore.MySql: [nuget](https://www.nuget.org/packages/Pomelo.EntityFrameworkCore.MySql/)
#### Pomelo.EntityFrameworkCore.MySql.Design: [nuget](https://www.nuget.org/packages/Pomelo.EntityFrameworkCore.MySql.Design/)
#### Swashbuckle.AspNetCore: [nuget](https://www.nuget.org/packages/Swashbuckle.AspNetCore/)

### To check out our search ability, please visit the following pages:
#### localhost:7036/api/products  <-shows all of the products in the database
#### localhost:7036/api/customers  <-shows all of the customers in the database
#### localhost:7036/api/orders  <-shows all of the orders in the database

### Applicable endpoints:

#### GET /api/customers
http://localhost/api/customers
```csharp
[HttpGet] 
public Task<object> GetAll(string? search)
in class CustomerController
```

#### POST /api/customers
http://localhost/api/customers
```csharp
[HttpPost] 
public Task<object> Post([FromBody] CustomerDto customerDto)
in class CustomerController
```
#### POST /api/customers
http://localhost/api/customers
```csharp
[HttpPost] 
public Task<object> Put([FromBody] CustomerDto customerDto)
in class CustomerController
```
#### GET /api/customers/:id
http://localhost/api/customers/{{id}}
```csharp
[HttpGet] [Route("{id}")] 
public Task<object> Get(int id)
in class CustomerController
```
#### Delete /api/customers/:id
http://localhost/api/customers/{{id}}
```csharp
[HttpDelete] [Route("{id}")] 
public Task<object> Delete(int id)
in class CustomerController
```

#### GET /api/orders
http://localhost/api/orders
```csharp
[HttpGet] 
public Task<object> GetAll(string? search, string? start, string? end)
in class OrderController
```
#### POST /api/orders
http://localhost/api/orders
```csharp
[HttpPost] 
public Task<object> Post([FromBody] OrderDto orderDto)
in class OrderController
```
#### POST /api/orders
http://localhost/api/orders
```csharp
[HttpPost] 
public Task<object> Put([FromBody] OrderDto orderDto)
in class OrderController
```
#### GET /api/orders/:id
http://localhost/api/orders/{{id}}
```csharp
[HttpGet] [Route("{id}")] 
public Task<object> Get(int id)
in class OrderController
```
#### Delete /api/orders/:id
http://localhost/api/orders/{{id}}
```csharp
[HttpDelete] [Route("{id}")] 
public Task<object> Delete(int id)
in class OrderController
```

#### GET /api/products
http://localhost/api/products
```csharp
[HttpGet] 
public Task<object> GetAll(
    string? search, 
    string? startDate, 
    string? endDate, 
    int? customer)
in class ProductController
```
#### POST /api/products
http://localhost/api/products
```csharp
[HttpPost] 
public Task<object> Post([FromBody] ProductDto productDto)
in class ProductController
```
#### POST /api/products
http://localhost/api/products
```csharp
[HttpPost] 
public Task<object> Put([FromBody] ProductDto productDto)
in class ProductController
```
#### GET /api/products/:id
http://localhost/api/products/{{id}}
```csharp
[HttpGet] [Route("{id}")] 
public Task<object> Get(int id)
in class ProductController
```
#### Delete /api/products/:id
http://localhost/api/products/{{id}}
```csharp
[HttpDelete] [Route("{id}")] 
public Task<object> Delete(int id)
in class ProductController
```

## Objective for the project:
### Phase 1: Basic DB Design
The objective is to design a database for our Musical Instrument Store. The database must be able to
persist our customers, our inventory of Instruments, and all orders. The developer may store this data in
any format they wish; In other words, the three categories of data to be stored does not necessarily
indicate that there will be three tables in the db. Other tables may be required to correctly persist this
data model.

#### 1. REQUIRED data
#### Customers
    i. Name

    ii. Address

    iii. Phone

    iv. Email

    v. Contact Date

#### Instruments
    i. Model #

    ii. Name

    iii. Manufacturer

    iv. Category (Type of Instrument)

    v. Price Data (Retail / Wholesale)

    vi. # On Hand

#### Orders
    i. Order Date

    ii. Ship Date

    iii. Notes

    iv. Order Status (pending, paid, shipped, etc)

    v. Items included on Order


#### 2. Additional Requirements
#### Price Data Persistence
    i. The order shall be able to display what the price of each item was AT THE TIME OF PURCHASE

#### 3. Optional Data
    The data fields listed above are required; However, the developer may add any
    that are deemed necessary to meet our objectives

### Phase 2: Basic Select Queries
There are several queries we know up front will be required for our store.
#### 1. Required Queries
#### a. Search Customers by personal data
##### https://localhost:7175/api/products?search={substring}
    i. Search by fields such as name, email, address, etc
    SELECT * FROM Customers WHERE {field} = '{substring}';

    ii. Shall be able to search by substrings of the fields &
    iii. May be separate queries or compound, at the developer’s discretion &
    iv. Example: “Return all customers named ‘Smith’”, or “Return all
    customers with an email address at houstonisd.org”
    Solved by:
    SELECT * FROM Customers WHERE {field} LIKE '%{substring}%';



#### b. Search Order by a date range
##### https://localhost:7175/api/orders?search=date&start={startDate}&end={endDate}
    i. Example: “Return all orders taking place between January and July, 2016”
    SELECT * FROM Orders WHERE OrderDate BETWEEN '2016-01-01' AND '2016-07-01';

#### c. Get Orders falling within a specific price range
##### https://localhost:7175/api/orders?search=price&start={startPrice}&end={endPrice}

    i. Example: “Return all orders between $2,500 and $5,000”
    SELECT * FROM Orders WHERE Price BETWEEN 2500 AND 5000;

#### d. Get all Customers including the sum of their order amounts
##### https://localhost:7175/api/customers?search=spent
    i. Basically, each customer and the total they have spent
    SELECT C.Id, FirstName, LastName, Email, Phone, Address, City, State, Zip, ContactDate, SUM(O.OrderTotal) 
    FROM Customers C 
    JOIN Orders O on C.Id = O.Customer 
    GROUP BY C.Id;

#### e. Get all Instruments that have ever been purchased by a specific Customer
##### https://localhost:7175/api/products?customer={customerId}
    SELECT P.Id, P.Name, P.ModelNumber, P.RetailPrice, P.WholeSalePrice, P.Category, P.Manufacturer, P.QuantityInStock 
    FROM Products P 
    JOIN OrderProduct OP on P.Id = OP.ItemsId 
    JOIN Orders O on O.Id = OP.OrdersId 
    WHERE O.Customer = '{customerId}' 
    GROUP BY P.Id;

#### f. Get all customers that have ordered a specific type of instrument
##### https://localhost:7175/api/customers?instrument={instrumentId}
    i. Example: “Return all customers that have ordered brass instruments”
    SELECT C.Id, FirstName, LastName, Email, Phone, Address, City, State, Zip, ContactDate
    FROM Customers C
    JOIN Orders O on C.Id = O.Customer
    JOIN OrderProduct OP on O.Id = OP.OrdersId
    WHERE OP.ItemsId = '{instrumentId}'
    GROUP BY C.Id;

#### g. Get all instruments that have sold in the last 6 months, including the quantity sold of each
##### https://localhost:7175/api/products?search=recent
    SELECT P.Id, P.Name, P.ModelNumber, P.RetailPrice, P.WholeSalePrice, P.Category, P.Manufacturer, P.QuantityInStock, COUNT(*)
    FROM Products P
    JOIN OrderProduct OP on P.Id = OP.ItemsId
    RIGHT OUTER JOIN Orders O on O.Id = OP.OrdersId
    WHERE O.OrderDate <= NOW()
    GROUP BY P.Id;
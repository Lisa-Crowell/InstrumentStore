create database `music.db`;

use `music.db`;

create table if not exists Customers
(
    Id          int auto_increment
    primary key,
    FirstName   longtext    not null,
    LastName    longtext    not null,
    Email       longtext    not null,
    Phone       longtext    not null,
    Address     longtext    not null,
    City        longtext    not null,
    State       longtext    not null,
    Zip         longtext    not null,
    ContactDate datetime(6) not null
    );

create table if not exists Orders
(
    Id          int auto_increment
    primary key,
    Customer    int            not null,
    Notes       longtext       not null,
    OrderDate   datetime(6)    not null,
    ShipDate    longtext       not null,
    OrderStatus longtext       not null,
    OrderTotal  decimal(10, 2) not null
    );

create table if not exists Products
(
    Id              int auto_increment
    primary key,
    Name            longtext       not null,
    ModelNumber     varchar(25)    not null,
    RetailPrice     decimal(10, 2) not null,
    WholeSalePrice  decimal(10, 2) not null,
    Category        longtext       not null,
    Manufacturer    longtext       not null,
    QuantityInStock int            not null
    );

create table if not exists OrderProduct
(
    ItemsId  int not null,
    OrdersId int not null,
    primary key (ItemsId, OrdersId),
    constraint FK_OrderProduct_Orders_OrdersId
    foreign key (OrdersId) references Orders (Id)
    on delete cascade,
    constraint FK_OrderProduct_Products_ItemsId
    foreign key (ItemsId) references Products (Id)
    on delete cascade
    );

create index IX_OrderProduct_OrdersId
    on OrderProduct (OrdersId);


INSERT INTO `music.db`.Products (Id, Name, ModelNumber, RetailPrice, WholeSalePrice, Category, Manufacturer, QuantityInStock) VALUES (1, 'Gibson Guitar', '1234', 900.00, 750.00, 'String Instrument', 'Gibson', 6);
INSERT INTO `music.db`.Products (Id, Name, ModelNumber, RetailPrice, WholeSalePrice, Category, Manufacturer, QuantityInStock) VALUES (2, 'Fender Guitar', '1235', 1250.00, 1000.00, 'String Instrument', 'Fender', 5);
INSERT INTO `music.db`.Products (Id, Name, ModelNumber, RetailPrice, WholeSalePrice, Category, Manufacturer, QuantityInStock) VALUES (3, 'Baby Grand Piano', '1236', 35000.00, 25000.00, 'Percussion Instrument', 'Steinway', 4);
INSERT INTO `music.db`.Products (Id, Name, ModelNumber, RetailPrice, WholeSalePrice, Category, Manufacturer, QuantityInStock) VALUES (4, 'Irish Flute', '1237', 750.00, 500.00, 'Wind Instrument', 'Martin', 3);


INSERT INTO `music.db`.Orders (Id, Customer, Notes, OrderDate, ShipDate, OrderStatus, OrderTotal) VALUES (1, 1, 'This is a test order', '2022-07-23 17:12:23.0', '2022-08-01', 'Processing', 900.00);
INSERT INTO `music.db`.Orders (Id, Customer, Notes, OrderDate, ShipDate, OrderStatus, OrderTotal) VALUES (2, 2, 'This is a test order', '2022-07-23 17:12:23.0', '2022-08-01', 'Processing', 1250.00);
INSERT INTO `music.db`.Orders (Id, Customer, Notes, OrderDate, ShipDate, OrderStatus, OrderTotal) VALUES (3, 3, 'This is a test order', '2022-07-23 17:12:23.0', '2022-08-11', 'Processing', 35000.00);
INSERT INTO `music.db`.Orders (Id, Customer, Notes, OrderDate, ShipDate, OrderStatus, OrderTotal) VALUES (4, 4, 'This is a test order', '2022-07-23 17:12:23.0', '2022-08-01', 'Processing', 750.00);

INSERT INTO `music.db`.OrderProduct (ItemsId, OrdersId) VALUES (1, 1);
INSERT INTO `music.db`.OrderProduct (ItemsId, OrdersId) VALUES (2, 2);
INSERT INTO `music.db`.OrderProduct (ItemsId, OrdersId) VALUES (3, 3);
INSERT INTO `music.db`.OrderProduct (ItemsId, OrdersId) VALUES (4, 4);


INSERT INTO `music.db`.Customers (Id, FirstName, LastName, Email, Phone, Address, City, State, Zip, ContactDate) VALUES (1, 'John', 'Doe', 'johndoe@gmail.com', '421-456-7890', '987 Main St', 'Anytown', 'NJ', '12645', '2022-07-23 17:12:23.0');
INSERT INTO `music.db`.Customers (Id, FirstName, LastName, Email, Phone, Address, City, State, Zip, ContactDate) VALUES (2, 'Jane', 'Doe', 'janedoe@gmail.com', '616-456-5184', '123 Main St', 'Anytown', 'MA', '34585', '2022-07-23 17:12:23.0');
INSERT INTO `music.db`.Customers (Id, FirstName, LastName, Email, Phone, Address, City, State, Zip, ContactDate) VALUES (3, 'Rob', 'Smith', 'robertsmith@hotmail.com', '456-123-7859', '456 Main St', 'Anytown', 'DE', '04585', '2022-07-23 17:12:23.0');
INSERT INTO `music.db`.Customers (Id, FirstName, LastName, Email, Phone, Address, City, State, Zip, ContactDate) VALUES (4, 'Mary Jane', 'Wilder', 'mjwilder@yahoo.com', '321-546-4100', '321 Main St', 'Anytown', 'CA', '12345', '2022-07-23 17:12:23.0');


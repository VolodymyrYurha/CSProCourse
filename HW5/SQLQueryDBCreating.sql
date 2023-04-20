CREATE TABLE INVOICE
(
	Id uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
	RecipientAddress nvarchar(100),
	RecipientPhoneNumber nvarchar(17),
	SenderAddress nvarchar(100),
	SenderPhoneNumber nvarchar(17),
);

CREATE TABLE VEHICLETYPE
(
   Id int NOT NULL PRIMARY KEY CLUSTERED,
   VehicleType varchar(20) NOT NULL UNIQUE,
);

INSERT INTO VEHICLETYPE (Id, VehicleType)
values
  (1, 'Car'),
  (2, 'Ship'),
  (3, 'Plane'),
  (4, 'Train');

DROP TABLE IF EXISTS VEHICLE 
CREATE TABLE VEHICLE
(
	Id int NOT NULL PRIMARY KEY CLUSTERED IDENTITY(1,1),
	VehicleTypeId int NOT NULL,
    Number nvarchar(20) NOT NULL,
    MaxCargoWeightKg int NOT NULL,
    MaxCargoWeightLbs float,
    MaxCargoVolume float NOT NULL,
    CargoWeightCurrent float DEFAULT 0,
    CargoVolumeCurrent float DEFAULT 0,
);

DROP TABLE IF EXISTS WAREHOUSE 
CREATE TABLE WAREHOUSE
(
	Id int NOT NULL PRIMARY KEY CLUSTERED IDENTITY(1,1),
);

CREATE TABLE CARGO
(
	Id uniqueidentifier NOT NULL PRIMARY KEY CLUSTERED,
    [Weight] int, 
	Volume float,
	Code nvarchar(20),
	InvoiceId uniqueidentifier FOREIGN KEY(InvoiceId) REFERENCES INVOICE(Id),
	VehicleId int FOREIGN KEY(VehicleId) REFERENCES VEHICLE(Id),
	WarehouseId int FOREIGN KEY(WarehouseId) REFERENCES WAREHOUSE(Id),
);
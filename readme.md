# Cosmos DB Generic Sample, NET 5.0
Cosmos DB is a globally distributed, scale-out database service running on the MS Azure cloud. It presents highly optimized performance potentials.

I decided to write an example that went beyond the MS examples, where I found that they are simple in the sense of not implementing exactly generic; which is not easy in this case. Also, I wanted to implement something more neglectful of the Partitions.

A huge advantage for the developer of this type of application is that Cosmos DB offers an emulator. That is, you don't have to be debugging in the cloud to create your prototypes.

For this example, I use NET 5.0, going through various details. It is notable that for example Cosmo serializes with Newtonsoft.Json, while the NET 5.0 Rest API uses the modern System.Text.Json. There is no conflict, the two can coexist in the same solution; the example illustrates that.

For those who use MongoDB, an application written for MongoDB can communicate with Cosmos and use databases from it. They are compatible technologies in protocols and communication.

[Azure Cosmos DB documentation](https://docs.microsoft.com/en-us/azure/cosmos-db/)


## Requirements

  - Visual Studio 2019 16.8.x with NET 5.0
  - [Cosmos DB Emulator](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator-release-notes)

## The Application Sample
It consists of a .NET 5.0 solution, with two projects: A netstandard 2.0 component, which runs C # 9, Library.Shared, and a Web Api REST .NET Core 5.0 application, Library.Api.

It is a prototype of a Library database with two example class models, one for books, Book, and the other for students, Student.

The ICosmosService <T> service demonstrates the core of the generic implementation. To overcome the Id property within the generics, some Reflection was implemented.

The application creates the «Library» (Where are we going to consult books) database, and feeds two Containers (tables in colloquial terms), with a data seed.

Azure Cosmos DB Emulator will show the following:

![Emulador](https://github.com/harveytriana/CosmosDBMock/blob/master/cdb_1.png)

The API Library.Api shows the CRUD implementation for both models. Note that / Partition is added in the GET and DELETE parameters, being an optional field. The default is the country where the server is running.

![Emulator](https://github.com/harveytriana/CosmosDBMock/blob/master/cdb_2.png)

> About Partitions, a detailed exposition: Azure DocumentDB Elastic Scale - Partitioning (Hint. Although examples show it, perhaps for simplicity, do not use the ID of the document as a partition)
: [Azure DocumentDB Elastic Scale - Partitioning](https://azure.microsoft.com/en-us/resources/videos/azure-documentdb-elastic-scale-partitioning/)

This image shows a GET in Swagger:

![Emulator](https://github.com/harveytriana/CosmosDBMock/blob/master/cdb_3.png)

### Other Details

* Shows a correct practice to implement the connection data to Cosmos from Settings, both for Azure and for the Emulator.

#### appsettings.json (fragment)
```json
{
  "Logging": {
  },
  "CosmosDbEmulator": {
    "EndPoint": "https://localhost:8081",
    "Key": "C2y6yDjf5/R+ob0N...aGQy67XIw/Jw==",
    "DatabaseId": "Library",
    "PartitionName": "Partition"
  },
  "CosmosDbCloud": {
    "EndPoint": "<your end point>",
    "Key": "<your secret hash>",
    "DatabaseId": "Library",
    "PartitionName": "Partition"
  }
}
```

* Shows an efficient way to handle a static file from .NET Core, with global path. It was used for the data seed when creating the sample database.
___
> :warning: **Before running this application**: Remember that you has to have a Cosmos DB service. The logical and simpler recommendation is to use the Cosmos DB emulator.
___

Luis Harvey Triana Vega
Twitter ```@__harveyt__```

License
----

MIT






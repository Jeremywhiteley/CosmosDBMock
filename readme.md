# Un ejemplo genérico con Cosmos DB
Cosmos DB es un servicio de base de datos con escalado horizontal y globalmente distribuido, que ejecuta en la nube MS Azure. Presenta potenciales de rendimiento muy optimizados.

Decidí escribir un ejemplo que fuera más allá de los ejemplos de MS, en donde encontré que son simples en el sentido de no implementar precisamente genéricos; lo cual no es sencillo en este caso. Así mismo, quise implementar algo más desatendido del asunto de las particiones. 

Una ventaja descomunal para el desarrollador de este tipo de aplicaciones es que Cosmos ofrece un emulador. Es decir, no tienes que estar depurando en la nube para crear tus prototipos.

Otra ventaja del ejemplo, es que uso en NET 5.0, pasando por varios detalles. Es notable que por ejemplo, Cosmo serializa con Newtonsoft.Json, mientras que la API Rest de NET 5.0 usa el moderno System.Text.Json. No hay conflicto, los dos pueden convivir en la misma solucion; el ejemplo ilustra eso. Por cierto, Mongo también necesitaría Newtonsoft.Json para trabajar con C#.

Para quienes usan MongoDB, una aplicación escrita para MongoDB se puede comunicar con Cosmos y usar bases de datos de esta. Son tecnologías compatibles en protocolos y comunicación.

[Documentación sobre Azure Cosmos DB](https://docs.microsoft.com/es-es/azure/cosmos-db/)


## Requisitos

  - Visual Studio 2019 16.8.x con NET 5.0
  - [Emulador](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator-release-notes)

## La aplicación
Consiste en una solución de .NET 5.0, con dos proyectos: Un componente netstandard 2.0, que corre C# 9, **Library.Shared**, y una aplicación Web Api REST .NET Core 5.0, **Library.Api**.

Se trata de un prototipo de una base de datos de una Biblioteca con dos modelos clasistas de ejemplo, una para libros, **Book**, y otra para estudiantes, **Student**.

El servicio ICosmosService<T> demuestra el núcleo de la implantación con genéricos. Para superar la propiedad **Id** dentro de los genéricos, se implementó algo de Reflexión.

La aplicación crea la base de datos **Library**, y alimenta dos Contenedores (tablas en términos coloquiales), con una semilla de datos.

Azure Cosmos DB Emulator mostrará lo siguiente:

![Emulador](https://github.com/harveytriana/CosmosDBMock/blob/master/cdb_1.png)

La API **Library.Api** muestra la implementación CRUD para ambos modelos. Nótese que se agrega **/Partition** en los parámetros de GET y DELETE, siendo un campo opcional. El predeterminado es el país en donde ejecuta el servidor. 

![Emulador](https://github.com/harveytriana/CosmosDBMock/blob/master/cdb_2.png)

> Acerca de las Particiones, una exposicion detallada: [Azure DocumentDB Elastic Scale - Partitioning](https://azure.microsoft.com/en-us/resources/videos/azure-documentdb-elastic-scale-partitioning/) (Sugerencia. Aunque ejemplos lo muestran, quizás por simplicidad, no use el Id del documento como partición)

Esta imagen muestra un **GET** en Swagger:

![Emulador](https://github.com/harveytriana/CosmosDBMock/blob/master/cdb_3.png)

### Otros Detalles

* Muestro una práctica correcta para implementar los datos de conexión a Cosmos desde Settings, tanto  para Azure como para el Emulador.

#### appsettings.json (fragmento)
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

* Muestro una forma eficiente para manejar un archivo estático desde .NET Core, con ruta global. Se usó para la semilla de datos al crear la base de daos de ejemplo.

* Se resuelve de manera lógica las complejidades que maneja Cosmos en la clase **CosmosService<T>** 
___
Sigueme en Twitter ```@__harveyt__```






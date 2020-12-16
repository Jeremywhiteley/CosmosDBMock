# Cosmos DB Sample
Cosmos DB es un servicio de base de datos con escalado horizontal y globalmente distribuido, que ejecuta en la nube MS Azure. Presenta potenciales de rendimiento tecnológicamente optimizados.

Decidí escribir un ejemplo que fuera más allá de los ejemplos de MS, en donde encontré que son poco muy simples en el sentido de no implementar precisamente genéricos; lo cual no es sencillo en este caso. Así mismo, quise implementar algo más desatendido del asunto de las particiones. 

Una ventaja descomunal para el desarrollador de este tipo de aplicaciones es que Cosmo ofrece un emulador. Es decir, no tiene que estar depurando en la nube para crear tus prototipos.

Otra ventaja del ejemplo, es que la cree en NET 5.0, pasando por varios detalles. Es notable que por ejemplo, Cosmo serializa con NewtonSoft, mientras que la API Rest de NET 5.0 usa el moderno MS.JSON. No hay conflicto, los dos pueden vivir en el mismo ecosistema.
Para quienes usan MongoDB, una aplicación escrita para MongoDB se puede comunicar con Cosmos y usar bases de datos de esta. Son tecnologías compatibles en protocolos y comunicación.

[Documentación](https://docs.microsoft.com/es-es/azure/cosmos-db/)


## Requisitos

  - Visual Studio 2019 16.8.x con NET 5.0
  - [Emulador](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator-release-notes)

## La aplicación
Consiste en una solución de .NET 5.0, con dos proyectos: Un componente netstandard 2.0, que corre C# 9, Library.Shared, y una aplicación Web Api REST .NET Core 5.0, Library.Api.

Se trata de un prototipo de Biblioteca con dos modelos de ejemplo, una para libros, Book, y otra para estudiantes, Students.

El servicio ICosmosService<T> demuestra el núcleo de la implantación con genéricos. Para superar la propiedad Id dentro de los genéricos, se implementó algo de Reflexión.

La aplicación crea la base de datos Library, y alimenta dos Contenedores (tablas en términos coloquiales), con una semilla de datos. 
![Emulador](https://github.com/harveytriana/CosmosDBMock/blob/master/cdb_1.png)

La API muestra la implementación CRUD para ambos modelos,
![Emulador](https://github.com/harveytriana/CosmosDBMock/blob/master/cdb_2.png)
Probando un GET:
![Emulador](https://github.com/harveytriana/CosmosDBMock/blob/master/cdb_3.png)
Muestra cómo implementar desde Settings los datos de conexión a Cosmos, para Azure y para el Emulador.

#### appsettings.json
```json
{
  "Logging": {
...
  },
  "CosmosDbEmulator": {
    "EndPoint": "https://localhost:8081",
    "Key": "C2y6yDjf5/R+ob0N...aGQy67XIw/Jw==",
    "DatabaseId": "Library",
    "PartitionName": "Partition"
  },
  "CosmosDbCloud": {
    "EndPoint": "",
    //"Key": "<Secret hash>",
    "DatabaseId": "Library",
    "PartitionName": "Partition"
  }
}
```
___
Sigueme en Twitter @__harveyt__






# duEco

La arquitectura utilizada es:
* View
* ViewModel
* Model
* Servicio

View -> Son las interfaces con los controles UI. Consta de 2 archivos: .xaml y .xaml.cs

ViewModel -> Capa intermedia entre View y Model. Se declara con herencia del Model con las llamadas a los metodos declarados y que necesite llamar de el Servicio 

Model -> representa el modelo la Entidad en nuestra BD

Servicio -> Guarda el comportamiento del negocio

*Usando Libreria SQLite
Dentro de la carpeta del proyecto "Servicio" se encuentra la clase CoreServicio, la cual contiene un mètodo llamado ValidarTablasCreadas(). En este se iràn volcando las sentencias del codigo que proporciona SQLite para crear e insertar registros en la BD embebida como carga inicial e imprescindible para el funcionamiento de la aplicaciòn.
De este modo, se irà verificando si las tablas ya están creadas entonces no las vuelve a crear y en caso de tener ya los registros ingresados no lo vuelve a hacer. 

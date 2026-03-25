````markdown
# Simulador de la Liga BetPlay en Consola con C#

Aplicación de consola desarrollada en **C# y .NET** para simular un torneo de fútbol tipo Liga BetPlay.  
El sistema permite registrar equipos, simular partidos, actualizar estadísticas automáticamente y consultar reportes usando **LINQ**.

---

## Características

- Registro de equipos
- Validación para evitar nombres duplicados
- Simulación de partidos entre equipos existentes
- Actualización automática de estadísticas:
  - Partidos jugados
  - Partidos ganados
  - Partidos empatados
  - Partidos perdidos
  - Goles a favor
  - Goles en contra
  - Total de puntos
  - Diferencia de gol
- Tabla de posiciones ordenada por criterios deportivos
- Consultas con LINQ:
  - Líder del torneo
  - Top 3
  - Equipos invictos
  - Equipos sin victorias
  - Promedio de goles
  - Total de puntos

---

## Estructura del proyecto

```text
LigaBetPlayConsola/
│
├── Models/
│   └── Equipo.cs
│
├── Services/
│   ├── GestionTorneo.cs
│   └── Consultas.cs
│
├── Program.cs
├── LigaBetplayC#.csproj
└── LigaBetplayC#.sln
````

---

## Descripción de archivos

### `Models/Equipo.cs`

Contiene la clase `Equipo`, que representa cada equipo del torneo.

Propiedades principales:

* `Nombre`
* `PJ`
* `PG`
* `PP`
* `PE`
* `GF`
* `GC`
* `TP` (propiedad calculada)
* `DiferenciaGol` (propiedad calculada)

También incluye el método:

* `ActualizarResultado(int golesFavor, int golesContra)`

Este método protege el encapsulamiento, ya que las estadísticas no se modifican manualmente desde fuera de la clase.

---

### `Services/GestionTorneo.cs`

Clase encargada de la lógica principal del torneo.

Funciones principales:

* Registrar equipos
* Buscar equipos
* Simular partidos
* Validar que los equipos existan
* Evitar partidos entre el mismo equipo
* Actualizar automáticamente las estadísticas de ambos equipos

---

### `Services/Consultas.cs`

Clase especializada en reportes y consultas utilizando LINQ.

Incluye:

* Tabla de posiciones
* Líder del torneo
* Top 3
* Equipos invictos
* Equipos sin victorias
* Estadísticas generales del torneo

---

### `Program.cs`

Punto de entrada de la aplicación.

Incluye un menú interactivo en consola con opciones para:

* Listar equipos
* Registrar equipo
* Simular partido
* Ver tabla de posiciones
* Consultar estadísticas
* Salir

---

## Reglas de negocio

La tabla de posiciones se ordena con los siguientes criterios:

1. Mayor cantidad de puntos
2. Mejor diferencia de gol
3. Mayor cantidad de goles a favor
4. Orden alfabético por nombre

Sistema de puntos:

* Victoria = **3 puntos**
* Empate = **1 punto**
* Derrota = **0 puntos**

---

## Requisitos

* [.NET SDK](https://dotnet.microsoft.com/) instalado
* Visual Studio, VS Code o terminal compatible

---

## Cómo ejecutar el proyecto

### 1. Clonar o descargar el proyecto

```bash
git clone <url-del-repositorio>
```

O abre directamente la carpeta del proyecto en tu editor.

---

### 2. Ejecutar la aplicación

```bash
dotnet run
```

---

## Ejemplo de uso

1. Registrar equipos
2. Simular partidos ingresando nombres y goles
3. Consultar la tabla de posiciones
4. Revisar estadísticas del torneo

Ejemplo de resultado de un partido:

```text
Atlético Nacional 0 - 3 Millonarios
```

Ejemplo de tabla:

```text
Pos  Equipo                  PJ  PG  PE  PP   GF   GC   DG   TP
1    Millonarios        3   2   1   0    6    3    3    7
2     Atlético Nacional              3   2   0   1    5    4    1    6
3    Junior                   3   1   1   1    4    4    0    4
```

---

## Validaciones implementadas

* No se permiten nombres vacíos
* No se permiten equipos duplicados
* No se pueden registrar goles negativos
* No se puede simular un partido entre el mismo equipo
* No se puede simular un partido si uno o ambos equipos no existen

---

## Conceptos aplicados

* Programación Orientada a Objetos
* Encapsulamiento
* Modularidad
* Separación por responsabilidades
* LINQ
* Manejo de listas en memoria
* Validación de entradas en consola

---


## Autor

Julian Andres Santamaria Bustamante

```


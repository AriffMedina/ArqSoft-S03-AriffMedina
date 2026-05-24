# Catálogo de Gatos

> Plataforma web de catálogo felino con autenticación de usuarios, sistema de reviews con calificaciones y arquitectura limpia en ASP.NET Core.

---

## Tabla de contenidos

- [Descripción general](#descripción-general)
- [Arquitectura](#arquitectura)
- [Tecnologías](#tecnologías)
- [Estructura del proyecto](#estructura-del-proyecto)
- [Funcionalidades](#funcionalidades)
- [Instalación y uso](#instalación-y-uso)
- [Persistencia de datos](#persistencia-de-datos)
- [Cláusula de uso de IA](#cláusula-de-uso-de-ia)

---

## Descripción general

**Élite Felina** es una aplicación web MVC desarrollada en ASP.NET Core que permite explorar un catálogo de gatos con filtros por raza, pelaje y temperamento. Los usuarios registrados pueden dejar reviews con calificación de 1 a 5 estrellas en cada perfil felino, y cualquier visitante puede consultar el promedio de valoraciones de cada gato.

El proyecto fue construido siguiendo los principios de **Arquitectura Limpia (Clean Architecture)**, separando responsabilidades en capas independientes y desacopladas.

---

## Arquitectura

El proyecto sigue el patrón de **Clean Architecture** dividido en cuatro capas:

```
┌────────────────────────────────────────┐
│           Presentation                 │  Controllers, Views, wwwroot
├────────────────────────────────────────┤
│           Application                  │  Services (lógica de negocio)
├────────────────────────────────────────┤
│           Infrastructure               │  Repositorios JSON
├────────────────────────────────────────┤
│              Domain                    │  Modelos e Interfaces
└────────────────────────────────────────┘
```

La dependencia siempre fluye hacia adentro: Presentation → Application → Domain ← Infrastructure.

---

## Tecnologías

| Tecnología | Uso |
|---|---|
| ASP.NET Core MVC (.NET 8) | Framework web principal |
| C# | Lenguaje de programación |
| Razor Views (.cshtml) | Motor de plantillas HTML |
| System.Text.Json | Serialización / persistencia en JSON |
| BCrypt.Net-Next | Hash seguro de contraseñas |
| Bootstrap 5 | Componentes base de UI |
| Google Fonts (Cormorant Garamond + Outfit) | Tipografía premium |
| Session Middleware | Manejo de sesión de usuario |

---

## Estructura del proyecto

```
CatalogoApp/
├── Catalogo.Domain/
│   ├── Models/
│   │   ├── Item.cs             # Modelo de gato
│   │   ├── Usuario.cs          # Modelo de usuario
│   │   └── Review.cs           # Modelo de review
│   └── Interfaces/
│       ├── IItemRepository.cs
│       ├── IUsuarioRepository.cs
│       └── IReviewRepository.cs
│
├── Catalogo.Application/
│   └── Services/
│       ├── ItemService.cs       # Lógica de catálogo y filtros
│       ├── UsuarioService.cs    # Registro y validación de login
│       └── ReviewService.cs     # Reviews y calificación promedio
│
├── Catalogo.Infrastructure/
│   └── Repositories/
│       ├── JsonItemRepository.cs
│       ├── JsonUsuarioRepository.cs
│       └── JsonReviewRepository.cs
│
└── Catalogo.Presentation/
    ├── Controllers/
    │   ├── CatalogoController.cs
    │   ├── AuthController.cs
    │   └── ReviewController.cs
    ├── Views/
    │   ├── Home/Index.cshtml
    │   ├── Catalogo/
    │   │   ├── Index.cshtml     # Catálogo con filtros
    │   │   └── Detail.cshtml    # Detalle + reviews
    │   ├── Auth/
    │   │   ├── Login.cshtml
    │   │   └── Register.cshtml
    │   └── Shared/_Layout.cshtml
    ├── Data/
    │   ├── items.json           # Catálogo de gatos
    │   ├── users.json           # Usuarios registrados
    │   └── reviews.json         # Reviews y calificaciones
    └── Program.cs
```

---

## Funcionalidades

### Catálogo
- Listado de gatos con diseño de tarjetas premium
- Filtros combinables por **Raza**, **Pelaje** y **Temperamento**
- Página de detalle con descripción completa y calificación promedio en estrellas

### Autenticación
- Registro de nuevos usuarios con validación de campos y contraseña hasheada con **BCrypt**
- Inicio de sesión con sesión persistente (30 minutos)
- Cierre de sesión
- Botones de acceso en la barra de navegación superior derecha

### Reviews
- Cualquier visitante puede **leer** las reviews de un gato
- Solo usuarios **autenticados** pueden publicar una review
- Cada review incluye **comentario** y **calificación de 1 a 5 estrellas**
- Se muestra el **promedio de calificaciones** en el perfil de cada gato

---

## Instalación y uso

### Requisitos previos
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022 o VS Code

### Pasos

```bash
# 1. Clonar el repositorio
git clone https://github.com/tu-usuario/CatalogoApp.git
cd CatalogoApp

# 2. Instalar dependencia de BCrypt en el proyecto Application
dotnet add Catalogo.Application package BCrypt.Net-Next

# 3. Ejecutar la aplicación
cd Catalogo.Presentation
dotnet run
```

La aplicación estará disponible en `https://localhost:44325` (o el puerto asignado por IIS Express).

### Archivos de datos

Los archivos JSON se crean automáticamente en `Catalogo.Presentation/Data/` al iniciar la aplicación si no existen. No requieren configuración manual.

Si se cambia la estructura de los modelos, se recomienda borrar los archivos existentes para que se regeneren limpios:

```
Data/items.json
Data/users.json
Data/reviews.json
```

---

## Persistencia de datos

La aplicación utiliza archivos **JSON como base de datos**, siguiendo el mismo patrón en las tres entidades:

```
Repositorio → lee/escribe archivo .json
           → deserializa con System.Text.Json
           → auto-incrementa IDs
           → crea el archivo si no existe
```

Las contraseñas **nunca se almacenan en texto plano**. Se aplica hash con BCrypt antes de persistir y se verifica con `BCrypt.Verify()` en el login.

---

## Cláusula de uso de IA

Este proyecto fue desarrollado con asistencia de **Claude (Anthropic)** como herramienta de apoyo durante el proceso de desarrollo.

El uso de inteligencia artificial en este proyecto se limitó a:

- Generación de scaffolding y estructura de archivos siguiendo patrones de arquitectura indicados por el desarrollador
- Sugerencias de implementación revisadas, adaptadas e integradas manualmente
- Corrección de errores de compilación y ajuste de nombres de propiedades
- Generación de estilos CSS bajo dirección estética explícita del desarrollador

**Todo el código fue revisado, comprendido y validado por el desarrollador antes de ser integrado al proyecto.** Las decisiones de arquitectura, el diseño de la solución, la elección de tecnologías y la lógica de negocio son responsabilidad del autor.

El uso de herramientas de IA generativa se declara en cumplimiento con las políticas académicas e institucionales aplicables.

---

*Desarrollado como tarea de la semana para Arquitectura de Software — 2026*

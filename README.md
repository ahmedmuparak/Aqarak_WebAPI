# 🏡 Aqarak – Real Estate Web API

Aqarak is a RESTful Real Estate Web API built with **ASP.NET Core**, providing a scalable and maintainable backend for real estate applications. The API enables users to browse properties, manage listings, upload property images, and save favorite properties while following Clean Architecture principles.

---

## ✨ Features

- 🔐 User Authentication & Authorization
- 👤 User Registration & Login
- 🏠 Property Management (Create, Read, Update, Delete)
- 📷 Property Image Upload
- 🗂️ Store and Manage Image Paths
- ❤️ Favorites Management
- 📍 Governorate Management
- 🏷️ Category Management
- 📦 DTOs for Request & Response Models
- 🗃️ Entity Framework Core Migrations
- 🧩 Repository Pattern
- 🏛️ Clean Architecture
- 🌐 RESTful API Design

---

## 🛠️ Tech Stack

- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server
- LINQ
- Repository Pattern
- Clean Architecture
- DTOs
- RESTful APIs
- Authentication & Authorization
- File Upload & Image Handling

---

## 📂 Project Structure

```
Aqarak
│
├── Controllers
├── Services
├── Repositories
├── Interfaces
├── DTOs
├── Entities
├── Data
├── Migrations
├── Helpers
├── Middleware
├── wwwroot
│   └── Images
└── Program.cs
```

---

## 🔑 Main Modules

### Authentication

- User Registration
- User Login
- Authentication & Authorization

### Properties

- Create Property
- Update Property
- Delete Property
- Get Property Details
- Get All Properties
- Upload Property Images

### Favorites

- Add Property to Favorites
- Remove Property from Favorites
- Get User Favorite Properties



## 📷 Property Images

The API supports uploading multiple images for each property.

Features include:

- Upload images to the server
- Store image paths in the database
- Retrieve image URLs with property details

---

## 🗄️ Database

The project uses **SQL Server** with **Entity Framework Core**.


---


## 📌 API Features

- RESTful API
- Clean Architecture
- Repository Pattern
- DTO Pattern
- Entity Framework Core
- SQL Server
- LINQ
- Authentication & Authorization
- Image Upload
- Scalable Project Structure

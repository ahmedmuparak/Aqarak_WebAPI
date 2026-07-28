# 🏡 Aqarak – Real Estate Web API

Aqarak is a RESTful Real Estate Web API built with **ASP.NET Core**, providing a scalable and maintainable backend for real estate applications. The API enables users to browse properties, manage listings, upload property images, save favorite properties, and communicate directly with other users through a real-time chat system.

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
- 💬 Real-Time Chat with SignalR
- 📨 User Conversations & Messaging
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
- SignalR
- File Upload & Image Handling

---

## 📂 Project Structure

```text
Aqarak
│
├── Controllers
├── Hubs
├── Services
├── Repositories
├── Interfaces
├── DTOs
├── Entities
├── Migrations
├── Helpers
├── Middleware
├── wwwroot
│   └── Images
└── Program.cs
```

---

## 🔑 Main Modules

### 🔐 Authentication

- User Registration
- User Login
- JWT Authentication
- Authorization

---

### 🏠 Properties

- Create Property
- Update Property
- Delete Property
- Get Property Details
- Get All Properties
- Upload Multiple Property Images
- Retrieve Property Images

---

### ❤️ Favorites

- Add Property to Favorites
- Remove Property from Favorites
- Get User Favorite Properties

---

### 📍 Governorates

- Create Governorates
- Update Governorates
- Delete Governorates
- Retrieve All Governorates

---

### 🏷️ Categories

- Create Categories
- Update Categories
- Delete Categories
- Retrieve All Categories

---

## 💬 Chat & Conversations

The API provides a real-time messaging system that allows users to communicate directly regarding properties.

### Conversations

- Create or retrieve conversations between users
- Get all conversations for the authenticated user
- Retrieve conversation details
- Display the latest message in each conversation

### Messages

- Send messages
- Retrieve conversation messages
- Store message timestamps
- Track sender and receiver information

### Real-Time Communication

- SignalR integration
- Instant message delivery
- Live conversation updates

---

## 📷 Property Images

The API supports uploading multiple images for each property.

Features include:

- Upload images to the server
- Store image paths in SQL Server
- Retrieve image URLs with property details
- Multiple images per property

---

## 🗄️ Database

The project uses **SQL Server** with **Entity Framework Core**.

Database features include:

- Code First Approach
- EF Core Migrations
- Relationships & Foreign Keys
- Data Seeding
- Optimized Queries with LINQ

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
- JWT Security
- Image Upload
- SignalR Real-Time Communication
- Conversation Management
- Messaging System
- Scalable Project Structure

---



## 📖 API Endpoints

### Authentication

- Register
- Login

### Properties

- Get All Properties
- Get Property By Id
- Create Property
- Update Property
- Delete Property
- Upload Property Images

### Favorites

- Add Favorite
- Remove Favorite
- Get Favorites

### Categories

- Get Categories
- Create Category
- Update Category
- Delete Category

### Governorates

- Get Governorates
- Create Governorate
- Update Governorate
- Delete Governorate

### Chat

- Get User Conversations
- Get Conversation Messages
- Send Message
- Connect to SignalR Hub

---

## 🎯 Architecture

The project follows **Clean Architecture** principles by separating responsibilities into:

- Controllers
- Services
- Repositories
- DTOs
- Interfaces
- Entities
- Data Layer

This structure improves:

- Maintainability
- Scalability
- Testability
- Separation of Concerns

---

## 📄 License

This project is intended for educational and portfolio purposes.

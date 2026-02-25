# University Management System API

A RESTful API for managing university operations, built with **ASP.NET Web API**, **C#**, and **SQL Server**.

---

## 📋 Overview

This system allows universities to manage their core data through HTTP endpoints. It covers students, courses, enrollments, and more — all structured using a clean 3-tier architecture to keep the code organized and easy to maintain.

---

## 🚀 Features

- RESTful API endpoints (GET, POST, PUT, DELETE)
- 3-Tier Architecture: API Layer → Business Logic Layer (BLL) → Data Access Layer (DAL)
- SQL Server database with stored procedures
- Clean separation of concerns using a shared Global layer
- Database setup script included for easy deployment

---

## 🛠️ Technologies Used

| Technology | Purpose |
|------------|---------|
| ASP.NET Web API | Building RESTful endpoints |
| C# | Backend logic |
| SQL Server | Database |
| ADO.NET | Database connectivity |
| T-SQL / Stored Procedures | Data operations |

---

## 📁 Project Structure

```
University-Management-System-API/
│
├── University_Management_System_APIs/         # API Layer (Controllers & Endpoints)
├── University_Management_System_APIs_BLL/     # Business Logic Layer
├── University_Management_System_APIs_DAL/     # Data Access Layer
├── University_Management_System_APIs_Global/  # Shared models and utilities
└── DatabaseSetup.sql                          # SQL script to set up the database
```

---

## ⚙️ Getting Started

### Prerequisites

- Visual Studio 2019 or later
- SQL Server (any edition)
- SQL Server Management Studio (SSMS)
- .NET Framework 4.8

### Setup Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/Ahmed-Elhebir/University-Management-System-API.git
   ```

2. **Set up the database**
   - Open SQL Server Management Studio
   - Run the `DatabaseSetup.sql` script to create the database and all required tables/stored procedures

3. **Update the connection string**
   - Open the project in Visual Studio
   - Update the connection string in the config file to point to your SQL Server instance

4. **Run the project**
   - Press `F5` in Visual Studio or run with `IIS Express`
   - The API will be available at `http://localhost:{port}/api/`

---

## 🔗 API Endpoints (Examples)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/students` | Get all students |
| GET | `/api/students/{id}` | Get student by ID |
| POST | `/api/students` | Add a new student |
| PUT | `/api/students/{id}` | Update student info |
| DELETE | `/api/students/{id}` | Delete a student |

> Similar endpoint patterns apply for other resources (courses, enrollments, etc.)

---

## 🏗️ Architecture

This project follows a **3-Tier Architecture**:

- **API Layer** — Handles HTTP requests and sends responses (Controllers)
- **BLL (Business Logic Layer)** — Contains the business rules and logic
- **DAL (Data Access Layer)** — Handles all communication with the database using ADO.NET and stored procedures
- **Global Layer** — Shared DTOs and models used across all layers

---

## 👤 Author

**Ahmed Elhebir Ahmed Ibrahim**  
Junior .NET Framework Developer  
📧 ahmed.elheber010@gmail.com  
🔗 [LinkedIn](https://linkedin.com/in/ahmed-elhebir) | [GitHub](https://github.com/Ahmed-Elhebir)

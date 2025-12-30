# 🎓 Student Management System

## 📌 Project Overview

The **Student Management System** is a web-based application developed to manage student records efficiently. This project is designed for learning and practice purposes using **ASP.NET Core MVC** with a clean and layered architecture.

The system allows administrators and users to perform CRUD operations (Create, Read, Update, Delete) on student data, manage users with roles, and understand how MVC, Services, Repositories, and Database layers work together.

---

## 🛠️ Technologies Used

* **Backend**: ASP.NET Core MVC
* **Frontend**: HTML, CSS, Bootstrap, jQuery
* **Database**: SQL Server
* **ORM**: Entity Framework Core (Code First / Database First)
* **Architecture**: Layered Architecture (MVC + Service + Repository)
* **Tools**: Visual Studio, GitHub, SQL Server Management Studio (SSMS)

---

## 🧱 Project Architecture

The project follows a **Layered Architecture** pattern:

* **UI Layer (MVC)**

  * Controllers
  * Views (Razor Pages)
  * ViewModels

* **Service Layer**

  * Business logic
  * Validation rules

* **Repository Layer**

  * Database access logic
  * Entity Framework queries

* **Data Layer**

  * DbContext
  * Data Models (Entities)

---

## ✨ Features

* 🔐 User Login with Roles
* 👨‍🎓 Student CRUD Operations
* 🔍 Search & Filter Students
* 📄 View Student Details
* ✅ Form Validation
* 🧭 Clean MVC Navigation

---

## 📂 Project Structure

```
StudentManagementSystem
│
├── Controllers
│   └── StudentController.cs
│
├── Models
│   └── Student.cs
│
├── ViewModels<img width="958" height="437" alt="SMSloginform" src="https://github.com/user-attachments/assets/f3d9f575-fd20-460f-9867-0524bdd31a3c" />

│   └── StudentViewModel.cs
│
├── Services
│   └── StudentService.cs
│
├── Repositories
│   └── StudentRepository.cs
│
├── Data
│   └── AppDbContext.cs
│
├── Views
│   └── Student
│       ├── Index.cshtml
│       ├── Create.cshtml
│       ├── Edit.cshtml
│       └── Details.cshtml
│
└── README.md
```

---

## 🚀 How to Run the Project

1. Clone the repository

   ```bash
   git clone  https://github.com/palephyu/StudentManagementSystem.git
  
   ```

2. Open the project in **Visual Studio**

3. Update database connection string in `appsettings.json`

4. Run database migration

   ```bash
   Update-Database
   ```

5. Press **F5** to run the project

---

## 📸 Screenshots

> Add screenshots of Login Page, Student List, Create/Edit Form here

```
images/login.png
images/student-list.png
```

---

## 🎯 Learning Objectives

* Understand ASP.NET Core MVC
* Practice CRUD operations
* Learn layered architecture
* Use Entity Framework Core
* Improve UI with Bootstrap
* Understand real-world project structure

---

## 👤 Author

**Name**: Pale Phyu
**Role**: .NET Developer (Student Project)

---

## 📄 License

This project is created for **educational purposes** only.

---

⭐ If you like this project, feel free to star the repository!

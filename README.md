# 🔧 ElectronicsFix

## 📚 Project Overview

**ElectronicsFix** is a complete **service-management web application** built using **ASP.NET Core MVC** and **Entity Framework Core**.
The platform is designed to manage **electronic repair services**, connecting customers with professional engineers through a structured system that supports service requests, order tracking, and administrative control.

This project focuses on solving real-world problems faced by electronic maintenance businesses, such as:

* Managing engineers and customers
* Tracking repair requests
* Controlling permissions and roles
* Organizing categories and repair items

---

## 🎯 Project Objectives

* Provide an organized system for electronic repair services
* Simplify communication between customers and engineers
* Enable admins to manage the entire platform efficiently
* Apply clean architecture and best practices in ASP.NET Core

---

## 🚀 Technologies Used

* **ASP.NET Core MVC**
* **Entity Framework Core**
* **SQL Server**
* **ASP.NET Core Identity** (Authentication & Authorization)
* **Bootstrap 5**
* **JavaScript & jQuery**
* **HTML5 & CSS3**

---

## ✨ Core Features

### 🔐 Authentication & Authorization

* Secure user registration and login
* Role-based system (**Admin / Engineer / Customer**)
* Permission-based access control for admin features
* Password validation and secure hashing

### 🧑‍💼 Admin Panel

* Dashboard overview
* Category management (Create / Edit / Delete)
* Item management (repairable electronics)
* Engineer management (Create, Edit, Delete)
* Customer management and role control
* Website settings (logo, contact info, system configuration)

### 🧰 Service Management

* Customers can submit repair requests
* Assign engineers to service orders
* Track service status (Pending → In Progress → Completed)
* View service history

### 👨‍🔧 Engineer Features

* Engineer accounts with secure login
* View assigned repair requests
* Update service status
* Manage personal profile

### 📱 Responsive Design

* Fully responsive UI
* Optimized for desktop, tablet, and mobile devices

---

## 🧱 Project Architecture

* MVC (Model – View – Controller)
* Clean separation of concerns
* ViewModels for UI logic
* Dependency Injection
* Scalable and maintainable structure

---

## 🛠️ How to Run the Project

1. Clone the repository:

   ```bash
   git clone https://github.com/your-username/ElectronicsFix.git
   ```

2. Open the solution in **Visual Studio 2022** or later.

3. Update the database connection string in:

   ```json
   appsettings.json
   ```

4. Apply Entity Framework migrations:

   ```powershell
   Update-Database
   ```

5. Run the application.

---

## 📸 Screenshots

* Home Page
* Service Request Page
* Admin Dashboard
* Engineer Panel

> *(Add screenshots for better presentation)*

---

## 🔒 Security Considerations

* Secure authentication using ASP.NET Core Identity
* Role and permission checks on server-side
* Input validation to reduce security risks

---

## 📈 Future Enhancements

* Online payment integration
* Real-time notifications
* Chat system between customer and engineer
* Mobile application version


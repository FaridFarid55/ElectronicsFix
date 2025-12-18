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

## 🗄️ Database Design & Data Model

The **ElectronicsFix** platform relies on a well-structured **relational database** designed to ensure data integrity, scalability, and clear separation of responsibilities between system entities.

### 1️⃣ Consultation

Represents consultations between customers and engineers.

* **Fields:** StartDate, EndDate, Status, ProblemDescription
* **Relations:**

  * Many-to-One with **Engineer**
  * Many-to-One with **Customer**

### 2️⃣ Engineer

Stores information about registered engineers.

* **Fields:** FullName, PhoneNumber, Address, Email, PasswordHash
* **Relations:**

  * One-to-Many with **Consultation**

### 3️⃣ Customer

Contains customer account data.

* **Fields:** FullName, PhoneNumber, Email, PasswordHash
* **Relations:**

  * One-to-Many with **Consultation**
  * One-to-Many with **Order**

### 4️⃣ Order

Represents customer orders.

* **Fields:** Quantity, TotalPrice, OrderDate
* **Relations:**

  * Many-to-One with **Customer**
  * Many-to-One with **Item**
  * One-to-One with **Delivery**

### 5️⃣ Delivery

Tracks order delivery details.

* **Fields:** DeliveryPersonName, Address, DeliveryStatus
* **Relations:**

  * One-to-One with **Order**

### 6️⃣ Item

Represents products and repairable items.

* **Fields:** ItemName, SalePrice, PurchasePrice, Description
* **Relations:**

  * Many-to-One with **Category**
  * Many-to-One with **ItemType**
  * One-to-Many with **Image**
  * One-to-One with **ItemDetails**
  * One-to-Many with **Order**
  * One-to-Many / One-to-One with **ItemDiscount**

### 7️⃣ ItemDiscount

Stores discount information applied to items.

* **Fields:** DiscountPercentage, ExpirationDate
* **Relations:**

  * Related to **Item**

### 8️⃣ Category

Organizes items into logical groups.

* **Fields:** CategoryName, CreatedAt, UpdatedAt

### 9️⃣ ItemType

Defines the type of item.

* **Examples:** Laptop, RAM, CPU, Monitor

### 🔟 ItemDetails

Stores technical specifications for items.

* **Fields:** Processor, RAM, ScreenResolution, OperatingSystem
* **Relations:**

  * One-to-One with **Item**

### 1️⃣1️⃣ Image

Stores item images.

* **Relations:**

  * One-to-Many with **Item**

### Database Principles Applied

* Relational Modeling
* Data Normalization
* Referential Integrity
* One-to-One & One-to-Many Relationships
* Entity Framework Core (Fluent API & Data Annotations)

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

---

> Designed and developed with a focus on clean code, security, and real-world business needs 🚀

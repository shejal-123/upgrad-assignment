Employee Management System-2
Name: Shejal Natha Bhosale
Batch: Batch2-Dotnet and Python

--------------------------------------------------
PROJECT OVERVIEW
--------------------------------------------------
This is a full-stack Employee Management System Web API built using ASP.NET Core Web API and Entity Framework Core.

It is the backend extension of Mini Project 1, where all employee data is now stored in a SQL Server database instead of browser memory.

The application exposes RESTful APIs that allow frontend applications (or tools like Swagger/Postman) to manage employee data securely and efficiently.

--------------------------------------------------
FEATURES
--------------------------------------------------
User Registration & Login (JWT Authentication)
Role-Based Access (Admin / Viewer)
CRUD operation on employee (create,read,update,delete are by Admin and only Read by Viewer )
Dashboard Summary (Total, Active, Inactive)
Search Employees (Server-side)
Filter by Department & Status
Sort Employees (Name, Salary, Join Date)
Pagination (Server-side)
Data Validation (Client + Server)
Secure Password Hashing (BCrypt)
Swagger API Testing


--------------------------------------------------
Commands to run EMS.API-
--------------------------------------------------
1. open EMS.API then dotnet restore
2. dotnet restore
3. dotnet build
4. dotnet run
5. open swagger at that particular endpoint


--------------------------------------------------
Commands to run frontend-
--------------------------------------------------
1. open frontend part
2. enter in index.html
3. open it with live server or go live


--------------------------------------------------
Commands to run EMS.Tests-
--------------------------------------------------
1. open EMS.Tests folder in terminal
2. dotnet test
 


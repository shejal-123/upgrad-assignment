# upgrad-assignment
upgrad training project

Name: Shejal Natha Bhosale
Batch: Batch2 - Dotnet and Python

--------------------------------------------------
PROJECT 1: Employee Management System (Frontend)
--------------------------------------------------
Browser-based EMS built using HTML, CSS, and JavaScript. 
Manages employee data without backend (stored in JS).

Features:
- Admin Login
- Add/Edit/Delete Employee
- Search, Filter, Sort
- Dashboard Summary
- Form Validation
- Jest Testing

Tech:
HTML, CSS, JavaScript, Jest

Run:
Open index.html in browser  
Run tests: npm install && npm test

--------------------------------------------------
PROJECT 2: Employee Management System (Web API)
--------------------------------------------------
Backend API using ASP.NET Core & Entity Framework with SQL Server.

Features:
- JWT Authentication
- Role-Based Access (Admin/Viewer)
- CRUD Operations
- Search, Filter, Sort, Pagination
- BCrypt Password Hashing
- Swagger Testing

Tech:
ASP.NET Core, EF Core, SQL Server

Run:
dotnet restore  
dotnet build  
dotnet run  
(Open Swagger URL)

Tests:
dotnet test

========================================
 MINI PROJECT 3: INCIDENT TRACKER
========================================

Python CLI-based tool that auto-classifies IT incidents 
and simulates ticket creation in ServiceNow, Jira, and Azure Boards.

Generates an HTML report after processing incidents.

--------------------------------------------------
FEATURES
--------------------------------------------------
- Auto classify incidents (type & severity)
- Regex-based detection
- Mock API integration (ServiceNow, Jira, Azure)
- Batch processing
- HTML report generation
- Logging & retry mechanism

--------------------------------------------------
TECHNOLOGIES
--------------------------------------------------
Python, JSON, REST APIs (Mock)

--------------------------------------------------
PROJECT STRUCTURE
--------------------------------------------------
- main.py
- models/, services/, utils/
- data/incidents.json
- output/report.html

--------------------------------------------------
RUN PROJECT
--------------------------------------------------
python3 main.py

(Open output/report.html after execution)

--------------------------------------------------
NOTES
--------------------------------------------------
- No real API calls (mock mode)
- No database required
- CLI-based application

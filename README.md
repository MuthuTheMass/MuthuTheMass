# MuthuTheMass
Muthu final year project

# About this Project
  In this project, There are 3 types of subscribers. 
      -Users (Normal Users)
      -Dealer (Parking slot Provider)
      -Admin (All Access grant person, authorized persons only)
  The USERS can book the parking slot for car only. The DEALERS will provide there parking slot if available with some time and date allocated and it will charged. With help DEALERS providing information the parking slot can the viewed by the USERS. The USERS can search car booking slot by area wise, also in and out datetime.
The USER will book the necessary slot that will be intimated to dealer. The dealer will should confirm or cancel that booking. If the USER need the slot confirmation urjent some booking slot will pre-approved slot will available that can be booked by USER. The cost will be calulated by date with time of IN and OUT with the APP prized value. If it exceed the OUT time of the slot that exceed the additional charge will be provided by dealer if not it will also calculated by automatic with minimal amount should be paid. If the car stays more than 2 days and exceed the slot OUT timing it will be notify to USER (if reachable). Otherwise, it will be reported to Police in which it can be processed by dealer where the app will initimate him.  

# In this project
  - Angular JS
  - ASP.NET Core 8
  - MS SQL
  - Cosmos DB

# Intro to Angular js
   AngularJS provides features like two-way data binding, dependency injection, and a modular architecture, which simplifies the development and testing of complex web applications. It helps create single-page applications (SPAs) where the entire content is loaded once, and subsequent interactions happen without refreshing the page.

  ### Basic Commands needs to know:
  - ```ng new Your_Project_name``` -> To Create Project
  - ```ng generate component Component_name``` or ```ng g c Component_name```-> To create component
  - ```ng generate service Service_Name ``` or ```ng g s Service_name``` -> To Create Service
  - ```ng generate directive Directive_Name``` or ```ng g d Directive_Name``` -> To create Directives
  - ```ng serve``` or ```ng s``` or ```ng s --o``` -> To run the project
  - ```npm ci``` -> Is to the install the latest and missed packages, In our project.

# Intro to ASP.Net
  ASP.NET Core Web API is a framework for building RESTful services on the .NET platform, enabling seamless communication between client and server applications. It supports features like routing, model binding, and dependency injection, making it ideal for creating scalable, maintainable, and high-performance APIs.

  ### In this,
  The Restful API is used with the help of Entity Framework core for MSSQL database migration. AutoMapper is used to map the different model with same property.
For accessing and validating the API calls the POLICY packages is used. 

# Intro to MSSQL
Microsoft SQL Server (MSSQL) is a relational database management system developed by Microsoft, designed to store and retrieve data as requested by other software applications. It offers robust features like advanced security, in-memory performance, and comprehensive data management tools, making it ideal for enterprise-level applications.

  ### Process
  For connecting the Database the database connection string will be used. And it will only used in ASP.NET project only. All the necessary details will be stored here.

# Intro to CosmosDB
Azure Cosmos DB is a globally distributed, multi-model database service by Microsoft, designed for high availability, low latency, and scalability. It supports various data models like document, key-value, graph, and column-family, and offers features like automatic indexing, multi-region writes, and comprehensive SLAs.

  ### Process
  This DB will store the JSON files consists of JSON Objects. In this project, It is mainly used to store the history of every event.
